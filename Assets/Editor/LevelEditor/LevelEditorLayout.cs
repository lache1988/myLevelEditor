using UnityEditor;
using UnityEngine;
using System;

public partial class LevelEditor : EditorWindow
{
    bool newmapFold = true;
    private bool editFold = false;

    [MenuItem("LevelEditor/start")]
    public static void startWindow()
    {
        EditorWindow.GetWindowWithRect<LevelEditor>(EUI.startWindow);
    }

    private void OnGUI()
    {   //selection info

        selectionContainer();

        //create a default map

        newmapContainer();

        //extend operation

        editContainer();


    }
    void selectionContainer()
    {
        GUILayout.Label("LevelEditor", EUI.titleStyle);
    }
    void newmapContainer()
    {
        GUILayout.BeginVertical(EUI.headerStyle);
        if (newmapFold = EditorGUILayout.Foldout(newmapFold, "New Map", true))
        {
            InputContent();
        }
        GUILayout.EndVertical();
    }

    void editContainer()


    {
        GUILayout.BeginVertical(EUI.headerStyle);
        if (editFold = EditorGUILayout.Foldout(editFold, "Edit", true))
        {
            EditContainer();
        }
        GUILayout.EndVertical();
    }

    

}
