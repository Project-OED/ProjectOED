using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public enum ActionMap
{
    Player,
    UI
}

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private List<InputSO> _inputData;
    private Dictionary<string, InputSO> _inputs;

    private void Awake()
    {
        _inputs = new Dictionary<string, InputSO>();

        for (int i = 0; i < _inputData.Count; i++)
        {
            _inputs.Add(_inputData[i].ButtonName, _inputData[i]);
        }
    }

    public void DisableInput()
    {
        _playerInput.enabled = false;
    }
    public void EnableInput()
    {
        _playerInput.enabled = true;
    }

    private void SwitvhActionMap(ActionMap action)
    {
        _playerInput.SwitchCurrentActionMap(action.ToString());
    }
}
