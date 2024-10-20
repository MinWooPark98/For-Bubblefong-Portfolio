using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    protected System.Action actionClose = null;


    protected virtual void void OnDestroy()
    {
        actionClose?.Invoke();
    }

    public void SetAction(System.Action _actionClose)
    {
        if (actionClose != null)
        {
            actionClose = null;
        }
        actionClose = _actionClose;
    }
}
