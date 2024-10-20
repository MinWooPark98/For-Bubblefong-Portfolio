using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupInventory : Popup
{
    public enum SortBy
    {
        Id,
        Name,
        Grade,
        Upgrade,    // 기획상 최우선으로 정렬은 불가, 드랍다운에도 노출 X
        Count,
    }

    private SortBy sortBy = SortBy.Grade;                                   // default = Grade
    private SortBy[] sortOrder = new SortBy[(int)SortBy.Count]              // 정렬 우선도
        { SortBy.Grade, SortBy.Id, SortBy.Name, SortBy.Upgrade };

    [SerializeField]
    private TMP_Dropdown dropdownSortOrder = null;
    [SerializeField]
    private ScrollRect scrollRect = null;
    [SerializeField]
    private ItemButton prefabItem = null;

    private List<ItemButton> listItemButton = new List<ItemButton>();
    private ItemButton itemSelected = null;


    public void DropdownSort(int sortBy)
    {
        if ((SortBy)sortBy == SortBy.Upgrade)
        {
            return;
        }
        this.sortBy = (SortBy)sortBy;
        UpdateCharacterList();
        UpdateSelected();
    }

    public void UpdateItemList()
    {
        for (int i = 0; i < listItem.Count; ++i)
        {
            Destroy(listItem[i].gameObject);
        }
        listItem.Clear();

        List<Data.Item> listSorted = SortToList();
        for (int i = 0; i < listSorted.Count; i++)
        {
            ItemButton newButton = Instantiate(prefabItem, scrollRect.content);
            newButton.Set(listSorted[i]);
            newButton.SetAction(
                () =>
                {
                    Select(newButton);
                });
        }
    }

    private List<Data.Item> SortToList()
    {
        List<Data.Item> listItem = UserData.GetItems();
        IOrderedEnumerable<Data.Item> orderedList = sortBy switch
        {
            SortBy.Grade => listItem.OrderByDescending(item => item.grade),
            SortBy.Id => listItem.OrderBy(item => item.id),
            SortBy.Name => listItem.OrderByDescending(item => item.name),
            _ => null
        };

        for (int i = 0; i < (int)SortBy.Count; ++i)
        {
            if (sortOrder[i] == sortBy)
            {
                continue;
            }

            orderedList = sortOrder[i] switch
            {
                SortBy.Grade => orderedList.ThenByDescending(item => item.grade),
                SortBy.Id => orderedList.ThenBy(item => item.id),
                SortBy.Name => orderedList.ThenBy(item => item.name),
                SortBy.Upgrade => orderedList.ThenByDescending(item => item.upgrade),
                _ => orderedList
            };
        }
        List<Data.Item> result = orderedList.ToList();
        return result;
    }

    private void Select(ItemButton _button)
    {
        itemSelected = _button;
    }

    public void ButtonEquip()
    {
        if (itemSelected == null)
        {
            Debug.Log("Item not selected");
            return;
        }
        StartCoroutine(Routine_RequestExample());
    }

    private IEnumerator Routine_RequestExample()
    {
        bool isDone = false;
        
        HttpGameServer.Client.Item.Equip.Request(
            itemSelected.data.id,
            (HttpGameServer.Client.Item.Equip.Res res) =>
            {
                isDone = true;

                if (res == null)
                {
                    return;
                }

                if (res.result == 100)
                {
                    UserData.SetItems(res.items);
                }
            },
            true);

        while (!isDone)
        {
            yield return null;
        }

        UpdateItemList();

        yield break;
    }
}

