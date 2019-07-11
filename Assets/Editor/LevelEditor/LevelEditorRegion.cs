using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public partial class LevelEditor : EditorWindow
{
    public int x = 8;
    public int y = 8;

    public int ChapterId = 0;
    public int LevelId = 0;
    public int BlockSize = 4;

    GameObject map = null;
    public enum toolMode
    {
        Tile = 0,
        Block = 2,
        Stair = 3
    }
    public toolMode mode;



    public void InputContent()
    {

        GUILayout.BeginHorizontal();
        GUILayout.Label("BlockSize", EUI.contextLayout);
        GUILayout.Label(BlockSize.ToString(), EUI.contextLayout);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("X", EUI.contextLayout);
        x = EditorGUILayout.IntField(x);
        GUILayout.Label("Y", EUI.contextLayout);
        y = EditorGUILayout.IntField(y);
        GUILayout.EndHorizontal();

        createMap();

        if (map)
        {
            newmapFold = false;
            editFold = true;
        }

    }

    void EditContainer()
    {
        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Edit Mode", EUI.contextLayout);
        edit = GUILayout.Toggle(edit, GUIContent.none, (GUILayout.Width(200)));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Draw Wire", EUI.contextLayout);
        drawblockwire = GUILayout.Toggle(drawblockwire, GUIContent.none, (GUILayout.Width(200)));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Brush", EUI.contextLayout);
        mode = (toolMode)EditorGUILayout.EnumPopup(mode, EUI.contextLayout);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Height", EUI.contextLayout);
        GUILayout.Label(height.ToString(), EUI.contextLayout);
        if (GUILayout.Button("+")) { height++; };
        if (GUILayout.Button("-")) { height--; };
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

    }

    void drawCube(Vector3 position,int height)
    {
       
        Handles.DrawWireCube(position + new Vector3(0.5f, 0.5f, 0.5f), Vector3.one);
    }

    void drawBlock(Vector3 position, int blocksize,int height)
    {
        for (int i = 0; i < blocksize; i++)
            for (int j = 0; j < blocksize; j++)
            {
                Vector3 newVector3 = new Vector3(position.x + i, position.y, position.z + j);
                Handles.DrawWireCube(newVector3 + new Vector3(0.5f, 0.5f, 0.5f), Vector3.one);
            }
    }

    

    void createMap()
    {
        if (GUILayout.Button("CreateMap") && !map)
        {
            //LevelEditorMap.createMap(ChapterId, LevelId, x, y, BlockSize, istransparent);
            GameObject newmap = new GameObject();
            newmap.name = "new map";
            map = newmap;
            createMapTile(x, y, BlockSize, map);
            createReferrence(x, y, BlockSize, map);
        }
    }

    void createReferrence(int x, int y, int BlockSize, GameObject map)
    {
        createTile(new Vector3(x * BlockSize + 1, 0, y * BlockSize + 1), map);
        createTile(new Vector3(x * BlockSize + 1, 1, y * BlockSize + 1), map);
        createTile(new Vector3(x * BlockSize + 1, 2, y * BlockSize + 1), map);
        createTile(new Vector3(x * BlockSize + 1, 3, y * BlockSize + 1), map);
        createTile(new Vector3(x * BlockSize + 1, 4, y * BlockSize + 1), map);
    }


    void createTile(Vector3 position, GameObject parent)
    {
        GameObject tile = Instantiate(Resources.Load<GameObject>("Tile[]"), position, Quaternion.identity, parent.transform);
        tile.name = "Tile[" + position.x.ToString() + "][" + position.z.ToString() + "][" + position.y.ToString() + "]";
    }

    void deleteTile(Vector3 position)
    {
        string name = "Tile[" + position.x.ToString() + "][" + position.z.ToString() + "][" + position.y.ToString() + "]";
        

        foreach (GameObject target in GameObject.FindGameObjectsWithTag("tile"))
        {
            if (target.name == name)
            {
                DestroyImmediate(target);
            }
        }


    }

    void createMapTile(int x, int y, int blocksize, GameObject map)
    {
        for (int i = 0; i < x * blocksize; i++)
        {
            for (int j = 0; j < y * blocksize; j++)
            {
                createTile(new Vector3(i, 0, j), map);
            }
        }
    }

    void createBlock(Vector3 position, GameObject parent, int blocksize)
    {
        for (int i = 0; i < blocksize; i++)
            for (int j = 0; j < blocksize; j++)
            {
                Vector3 newVector3 = new Vector3(position.x + i, position.y, position.z + j);
                createTile(newVector3, map);
            }
    }

    void deleteBlock(Vector3 position, int blocksize)
    {
        {
            for (int i = 0; i < blocksize; i++)
                for (int j = 0; j < blocksize; j++)
                {
                    Vector3 newVector3 = new Vector3(position.x + i, position.y, position.z + j);
                    deleteTile(newVector3);
                }
        }
    }

    void createStair(Vector3 position,direction dir,GameObject parent)
    {
        string stairname = "StairBack";
        switch (dir)
        {
            case (direction.back):
                stairname = "StairBack";
                break;
            case (direction.forward):
                stairname = "StairForward";
                break;
            case (direction.left):
                stairname = "StairLeft";
                break;
            case (direction.right):
                stairname = "StairRight";
                break;
        }
        
        GameObject tile = Instantiate(Resources.Load<GameObject>(stairname), position, Quaternion.identity, parent.transform);
        tile.name = "Tile[" + position.x.ToString() + "][" + position.z.ToString() + "][" + position.y.ToString() + "]";
    }

    Color heightColor(int height)
    {
        Color heightColor = new Color();
        switch (height)
        {
            case (0):
                heightColor = height0;
                break;
            case (1):
                heightColor = height1;
                break;
            case (2):
                heightColor = height2;
                break;
            case (3):
                heightColor = height3;
                break;
            case (4):
                heightColor = height4;
                break;
            default:
                heightColor = height5;
                break;

        }
        
        return heightColor;
    }

    void drawheightLable(Vector3 position,int height)
    {
        Handles.Label(position, "Height=" + height.ToString(), "sv_label_1");
        
    }
    


    void drawWireArc(Vector3 position,direction dir,int height)
    {
        
        drawCube(position,height);
        switch (dir)
        {
            case (direction.back):
                Handles.DrawWireArc(position + new Vector3(0.5f, 0.5f, 0.5f), Vector3.up, Vector3.back, 180f, 1.5f);
                break;
            case (direction.left):
                Handles.DrawWireArc(position + new Vector3(0.5f, 0.5f, 0.5f), Vector3.up, Vector3.left, 180f, 1.5f);
                break;
            case (direction.forward):
                Handles.DrawWireArc(position + new Vector3(0.5f, 0.5f, 0.5f), Vector3.up, Vector3.forward, 180f, 1.5f);
                break;
            case (direction.right):
                Handles.DrawWireArc(position + new Vector3(0.5f, 0.5f, 0.5f), Vector3.up, Vector3.right, 180f, 1.5f);
                break;
        }
    }

    void drawblockWire(int x,int y,int blocksize)
    {
        Handles.color = Color.black;
        Vector3 p1;
        Vector3 p2;

        for (int i = 0; i <= x*blocksize; i += blocksize)
        {
            p1 = new Vector3(i, 1, 0);
            p2 = new Vector3(i, 1, blocksize * y);
            Handles.DrawLine(p1, p2);
        }

        for (int j = 0; j <= y * blocksize; j += blocksize)
        {
            p1 = new Vector3(0, 1, j);
            p2 = new Vector3(blocksize * x, 1, j);
            Handles.DrawLine(p1, p2);
        }

    }
}
