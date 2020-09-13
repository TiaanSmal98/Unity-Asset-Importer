using System.IO;
using UnityEditor;
using UnityEngine;

public class TestEnvironment : EditorWindow
{
    public string baseName { get; set; }

    public int myInt;

    public float myFloatSlider;

    public GameObject myGameObject;

    [MenuItem("Asset Importer/Configure/Asset Importer Tool")]
    public static void ShowWindow()
    {
        GetWindow(typeof(TestEnvironment), true, "My title");
    }

    private void OnGUI()
    {
        GUILayout.Label("Import Assets", EditorStyles.boldLabel);

        baseName = EditorGUILayout.TextField("Base Name", baseName);

        myInt = EditorGUILayout.IntField("My Int", 0);

        myFloatSlider = EditorGUILayout.Slider("My Slider", 0f, 0f, 100f);

        myGameObject = EditorGUILayout.ObjectField("My GO", myGameObject, typeof(GameObject), false) as GameObject;

        if (GUILayout.Button("My Button"))
        {
            MyButtonClick();
        }
    }

    

    private void MyButtonClick()
    {
        //DirectoryInitializer.CreateAssetConfigFile("Assets");

        Debug.Log("Hello world");

        DirectoryInitializer.Start();
    }

}
