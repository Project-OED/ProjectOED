using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputSO", menuName = "Scriptable Objects/InputSO")]
public class InputSO : ScriptableObject
{
    public string ButtonName;

    [SerializeField] private KeyCode _keyCode;
    [System.NonSerialized] public float Value;
    [System.NonSerialized] public Vector2 ValueV2;

    private bool _inputHold;

    public event Action OnPressed;
    public event Action OnReleased;

    public bool Hold()
    {
        return _inputHold;
    }

    public void InputPerFormed(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (context.valueType == typeof(Single))
        {

        }
        else if (context.valueType == typeof(float))
        {
            Value = context.ReadValue<float>();
        }
        else if (context.valueType == typeof(Vector2))
        {
            ValueV2 = context.ReadValue<Vector2>();
        }

        _inputHold = true;
        OnPressed?.Invoke();
    }


    public void InputCanceled(InputAction.CallbackContext context)
    {
        if (!context.canceled) return;

        if (context.valueType == typeof(Single))
        {

        }
        else if (context.valueType == typeof(float))
        {
            Value = context.ReadValue<float>();
        }
        else if (context.valueType == typeof(Vector2))
        {
            ValueV2 = context.ReadValue<Vector2>();
        }

        _inputHold = false;
        OnReleased?.Invoke();
    }
}
