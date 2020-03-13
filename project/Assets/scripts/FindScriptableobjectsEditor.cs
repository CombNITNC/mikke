using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(FindScriptableobjects))]
public class FindScriptableobjectsEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        FindScriptableobjects findScriptableobjects = target as FindScriptableobjects;

        GUILayout.Space(50);

        //元のInspector部分を表示
        if (GUILayout.Button("適用"))
        {
            findScriptableobjects.find_scriptableobjects_in_folda();
        }

        //EditorApplication.playmodeStateChanged += () =>
        //{
        //    findScriptableobjects.find_scriptableobjects_in_folda();
        //};

        EditorSceneManager.sceneOpened += OnOpened;
    }
    private static void OnOpened(Scene scene, OpenSceneMode mode)
    {
        FindScriptableobjects findScriptableobjects = new FindScriptableobjects();
        findScriptableobjects.find_scriptableobjects_in_folda();
    }

}