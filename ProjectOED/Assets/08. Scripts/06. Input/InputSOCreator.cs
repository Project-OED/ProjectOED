using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;

[CustomEditor(typeof(InputSOCreator))]
public class CubeGenerateButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        InputSOCreator generator = (InputSOCreator)target;
        if (GUILayout.Button("인풋데이터 생성"))
        {
            generator.GenerateInputData();
        }
    }
}

public class InputSOCreator : MonoBehaviour
{
    [SerializeField] private string _savePath;
    [SerializeField] private InputActionAsset _input;
    public void GenerateInputData()
    {
        AllDeleteData();
        foreach (InputActionMap actionMap in _input.actionMaps)
        {
            foreach (InputAction action in actionMap.actions)
            {
                InputSO data = CreateInputData(action.name);
                data.Init(action);
            }
        }
    }

    private void AllDeleteData()
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(InputSO).Name, new[] { _savePath });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid); //해당 guid를 가진 에셋 경로 가져옴
            AssetDatabase.DeleteAsset(path); //삭제
        }
    }

    private InputSO CreateInputData(string fileName)
    {
        InputSO asset = ScriptableObject.CreateInstance<InputSO>(); //메모리에 해당 인스턴스 생성
        string assetPath = _savePath + fileName + ".asset";
        AssetDatabase.CreateAsset(asset, assetPath); //해당 경로에 에셋 생성

        AssetDatabase.SaveAssets(); //변경사항 즉시 저장
        AssetDatabase.Refresh(); //프로젝트 창 새로 고침

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset; //새로 생성된 에셋을 프로젝트 창에서 하이라이트
        return asset;
    }
}
