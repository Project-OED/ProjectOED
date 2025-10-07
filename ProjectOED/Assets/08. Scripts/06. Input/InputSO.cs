using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputSO", menuName = "Scriptable Objects/InputSO")]
public class InputSO : ScriptableObject
{
    public static Dictionary<string, bool> checkKeys = new Dictionary<string, bool>();

    public string ButtonName;
    public string KeyCode;

    [System.NonSerialized] public InputAction Action;
    [System.NonSerialized] public float Value;
    [System.NonSerialized] public Vector2 ValueV2;

    private bool _inputHold;

    public event Action<InputAction> RebindKey;
    public event Action<InputSO> OnPressed;
    public event Action<InputSO> OnReleased;

    public void Init(InputAction action)
    {
        Action = action;
        ButtonName = action.name;
        InitKeyCode();
    }

    public void InitKeyCode()
    {
        KeyCode = Action.GetBindingDisplayString(0);
    }

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
        OnPressed?.Invoke(this);
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
        OnReleased?.Invoke(this);
    }
}
