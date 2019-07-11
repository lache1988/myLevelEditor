using UnityEditor;
using UnityEngine;
using System;

public partial class LevelEditor : EditorWindow
{
    SceneView view;

    public bool edit = false;

    Event e;

    Ray ray;

    Vector3 mousePosition;

    Camera cam;
    Plane plane;
    float distance;
    Vector3 mouse;
    Vector3 mouseRegular;
    int height = 0;

    bool drawblockwire= true;
    Color height0 = new Color(0,0.55f,0);
    Color height1 = new Color(0.55f, 0.31f, 0);
    Color height2 = new Color(0.5f, 0, 0);
    Color height3 = new Color(0.4f,0.85f, 0.74f);
    Color height4 = new Color(0.9f, 0.9f, 0.9f);
    Color height5 = new Color(1, 0, 0);



    enum direction
    {
        forward = 0,
        right = 1,
        back  = 2,
        left = 3
    };

    direction dir = 0;


    void OnSUI(SceneView view)
    {
        e = Event.current;
        //int controlId = GUIUtility.GetControlID(FocusType.Passive);
        //if (e.type == EventType.MouseDown && e.button ==0)
        //{
        //    GUIUtility.hotControl = controlId;
        //    Event.current.Use();
        //}

        cam = view.camera;
        Tools.viewTool = ViewTool.None;
        mouseRegular = mouseIntposition();

        //mouse operation
        if (map)
        {
            if (e.type == EventType.MouseDown && e.button == 0 && edit == true && mode == toolMode.Tile)
            {
                createTile(mouseRegular, map);
            }

            if (e.type == EventType.MouseUp && e.button == 1 && edit == true && mode == toolMode.Tile)
            {
                deleteTile(mouseRegular);
            }

            if (e.type == EventType.MouseDown && e.button == 0 && edit == true && mode == toolMode.Block)
            {
                createBlock(mouseRegular, map, BlockSize);
            }

            if (e.type == EventType.MouseUp && e.button == 1 && edit == true && mode == toolMode.Block)
            {
                deleteBlock(mouseRegular, BlockSize);
            }

            if (e.type == EventType.MouseDown && e.button == 0 && edit == true && mode == toolMode.Stair)
            {
                createStair(mouseRegular, dir,map);
            }
            if (e.type == EventType.MouseUp && e.button == 1 && edit == true && mode == toolMode.Stair)
            {
                deleteTile(mouseRegular);
            }

            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.E && edit == true && mode == toolMode.Stair)
            {
                dirToNext();
            }

            if(drawblockwire)
            {
                drawblockWire(x,y,BlockSize);
            }


            //draw wire


            Handles.color = Color.black;
            if (edit && mode == toolMode.Tile)
                drawCube(mouseRegular,height);
            if (edit && mode == toolMode.Block)
                drawBlock(mouseRegular, BlockSize,height);
            if (edit && mode ==toolMode.Stair)
                drawWireArc(mouseRegular,dir,height);
            if (edit)
                drawheightLable(mouseRegular,height);
            
        }

        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

        SceneView.RepaintAll();

    }

    Vector3 mouseIntposition()
    {
        mouse = e.mousePosition;

        mouse.y = cam.pixelHeight - mouse.y;

        ray = cam.ScreenPointToRay(mouse);

        plane = new Plane(Vector3.up, new Vector3(0, height, 0));

        plane.Raycast(ray, out distance);

        mousePosition = ray.GetPoint(distance);

        mouseRegular = Vector3Int.RoundToInt(mousePosition);
        return mouseRegular;
    }




    private void OnDisable()
    {
        Tools.hidden = false;
        SceneView.onSceneGUIDelegate -= OnSUI;
    }

    private void OnEnable()
    {

        Tools.hidden = true;
        SceneView.onSceneGUIDelegate += OnSUI;
    }

    private void OnSelectionChange()
    {

    }

    void dirToNext()
    {
        if (dir == direction.left)
            dir = direction.forward;
        else
            dir++;
            }

}
