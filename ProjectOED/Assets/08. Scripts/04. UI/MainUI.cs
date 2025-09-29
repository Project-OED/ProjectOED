using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class ButtonInfo
{
    private RectTransform _text;
    private Button _button;
    private Vector3 _textPosition;
    private float _width;

    public float Width => _width;
    public RectTransform Text => _text;

    public ButtonInfo(GameObject button)
    {
        _text         = button.GetComponentInChildren<TMP_Text>().GetComponent<RectTransform>();
        _button       = button.GetComponent<Button>();
        _textPosition = _text.anchoredPosition;
        _width        = _text.sizeDelta.x;
    }

    public void InitTextPosition()
    {
        _text.anchoredPosition = _textPosition; 
    }

    public void UpdateTextPosition(Vector2 value)
    {
        _text.anchoredPosition += value;
    }

    public void Excute()
    {
        _button.onClick.Invoke();
    }
}

public class MainUI : ToggleUI
{
    [Header("------- 버튼 모음 ---------")]
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _titleButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private GameObject[] _buttons;

    [Header("------- 선택 ---------")]
    [SerializeField] private RectTransform _selectUI;
    [SerializeField] private int test;

    private List<ButtonInfo> _buttonInfo = new List<ButtonInfo>();
    private int _curIndex = 0;

    protected override void Awake()
    {
        foreach (GameObject go in _buttons)
        {
            _buttonInfo.Add(new ButtonInfo(go));
        }

        base.Awake();

        _continueButton.onClick.AddListener(ButtonEvent_Continue);
        _settingButton .onClick.AddListener(ButtonEvent_Setting);
        _saveButton    .onClick.AddListener(ButtonEvent_Save);
        _loadButton    .onClick.AddListener(ButtonEvent_Load);
        _titleButton   .onClick.AddListener(ButtonEvent_Title);
        _exitButton    .onClick.AddListener(ButtonEvent_Exit);
    }

    private void OnEnable()
    {
        SelectButton(test);
    }

    private void Update()
    {
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        _continueButton.onClick.RemoveAllListeners();
        _settingButton .onClick.RemoveAllListeners();
        _saveButton    .onClick.RemoveAllListeners();
        _loadButton    .onClick.RemoveAllListeners();
        _titleButton   .onClick.RemoveAllListeners();
        _exitButton    .onClick.RemoveAllListeners();
    }

    public void ClickButton()
    {
        _buttonInfo[_curIndex].Excute();
    }

    public void SelectButton(int index)
    {
        //전에 선택된 버튼의 텍스트 위치 초기화
        _buttonInfo[_curIndex].InitTextPosition();

        //새로운 버튼 지정
        _curIndex = index;

        //버튼 텍스트의 너비
        float width = _buttonInfo[_curIndex].Width;

        //선택된 버튼의 텍스트 위치 조정
        _buttonInfo[_curIndex].UpdateTextPosition(new Vector2(width / 5, 0));

        //선택 UI 위치 설정
        _selectUI.position = _buttonInfo[_curIndex].Text.position;
        _selectUI.anchoredPosition -= new Vector2(width / 2 + 25, 0);
    }

    private void ButtonEvent_Continue()
    {
        Debug.Log("응애");
    }

    private void ButtonEvent_Setting()
    {

    }

    private void ButtonEvent_Save()
    {

    }

    private void ButtonEvent_Load()
    {

    }

    private void ButtonEvent_Title()
    {

    }

    private void ButtonEvent_Exit()
    {

    }
}
