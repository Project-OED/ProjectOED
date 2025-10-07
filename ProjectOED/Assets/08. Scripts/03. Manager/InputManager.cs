using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using static UnityEngine.InputSystem.InputActionRebindingExtensions;

public enum ActionMap
{
    Player,
    UI
}

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputActionAsset;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private List<InputSO> _inputData;
    
    private RebindingOperation _rebindingOperation;
    private Dictionary<string, InputSO> _inputs;

    private void Awake()
    {
        _inputs = new Dictionary<string, InputSO>();

        for (int i = 0; i < _inputData.Count; i++)
        {
            _inputs.Add(_inputData[i].ButtonName, _inputData[i]);
            _inputData[i].RebindKey += StartRebinding;
        }
    }

    public void OnEnable()
    {
        string rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
            _inputActionAsset.LoadBindingOverridesFromJson(rebinds);
    }

    public void OnDisable()
    {
        string rebinds = _inputActionAsset.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
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

    public void resetAllBindings()
    {
        foreach (InputActionMap map in _inputActionAsset.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }
        
        foreach (InputSO data in _inputData)
        {
            data.InitKeyCode();
        }

        PlayerPrefs.DeleteKey("rebinds");
    }

    public void StartRebinding(InputAction action)
    {
        DisableInput();

        _rebindingOperation = action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/rightButton")
            .WithCancelingThrough("<Mouse>/leftButton")
            .OnApplyBinding(CheckDuplicatedKey) //바인딩 적용 직전 적용되는 체인
            .OnCancel(operation => RebindCancel())
            .OnComplete(operation => RebindComplete())
            .Start();
    }

    public void CheckDuplicatedKey(RebindingOperation operation, string newKey)
    {
        InputAction currentAction = operation.action;

        foreach(InputAction action in currentAction.actionMap)
        {
            if (action == currentAction) continue;

            string curPath = currentAction.bindings[0].effectivePath;
            if (curPath == newKey)
            {
                operation.Cancel();
                return; // 검사를 즉시 종료합니다.
            }
        }
        operation.Complete();
    }

    private void RebindCancel()
    {
        _rebindingOperation.Dispose();
        EnableInput();
    }

    private void RebindComplete()
    {
        _rebindingOperation.Dispose();
        EnableInput();
    }
}
