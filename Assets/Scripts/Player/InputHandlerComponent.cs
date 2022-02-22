using System;
using UnityEngine;

public class InputHandlerComponent : MonoBehaviour
{
    public event Action OnInteracted;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnInteracted?.Invoke();
        }
    }
}