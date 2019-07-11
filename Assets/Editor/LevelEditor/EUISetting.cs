using UnityEditor;
using UnityEngine;

public partial class EUI
{
    public static GUILayoutOption[] titleLayout
    {
        //get {return new GUILayoutOption[] { GUILayout.Width(40), GUILayout.Height(40) }};
        get => new GUILayoutOption[] { GUILayout.Width(400) };
    }

    public static  GUILayoutOption[] contextLayout
    {
        get => new GUILayoutOption[] { GUILayout.Width(100) };
    }


    public static void TitleField(string titleName)
    {
        GUILayout.Label(titleName, EUI.titleStyle, EUI.titleLayout);
    }

    public static Rect startWindow = new Rect (0, 0, 350, 600);
}

public partial class EUI
{
    public static GUIStyle titleStyle
    {
        get => "boldLabel";
    }
    public static GUIStyle headerStyle
    {
        get => "AC BoldHeader";
    }
}
