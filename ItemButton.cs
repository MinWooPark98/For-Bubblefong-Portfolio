using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    private Data.Item data = null;

    private System.Action onClick = null;


    public void SetAction(System.Action _action)
    {
        onClick = _action;
    }

    public void OnClick()
    {
        onClick?.Invoke();
    }

    public Data.Item GetData()
    {
        return data;
    }
}
