using System;
using UnityEngine;
using UnityEngine.Events;

public class ShopController : MonoBehaviour,Iinteractable
{
    public UnityEvent Event;
    public void Interact()
    {
        Event?.Invoke();
    }

    public void SetCursorState(bool isOpen)
    {
        if (isOpen)
        {

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
