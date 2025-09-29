using UnityEngine;
using VContainer;

public class ToggleUI : MonoBehaviour
{
    [Header("------ UI ------")]
    [SerializeField] private string _name;
    [SerializeField] private GameObject _ui;

    [Inject] private UIManager _uiManager;

    public string Name => _name;
    public GameObject UI => _ui;
    protected virtual void Awake()
    {
        _ui.SetActive(false);
        _uiManager.AddUIDictionary(_name, _ui);
    }

    protected virtual void OnDestroy()
    {
        _uiManager.RemoveUIDictionary(_name);
    }

    protected virtual void UIEvent_ToggleUI()
    {
        _uiManager.ToggleUI(name, true);
    }
}
