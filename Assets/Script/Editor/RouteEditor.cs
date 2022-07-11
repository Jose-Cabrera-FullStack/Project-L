using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RouteCreator)), ExecuteAlways]
public class RouteEditor : Editor
{
    public RouteCreator creator;
    public Route route;
    public PlayerController player;

    void OnSceneGUI()
    {
        if (creator.route != null)
        {
            Input();
            Draw();
        }
    }

    void Input()
    {
        Event guiEvent = Event.current;
        Vector3 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;

        bool shiftLeftClick = guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift;

        if (shiftLeftClick)
        {
            Undo.RecordObject(creator, "Add segment");
            route.AddSegment(mousePos);
        }
    }

    /// <summary> Draw all the gizmo in the scene view <summary>
    void Draw()
    {
        // Render Curve
        for (int i = 0; i < route.segmentsLen; i++)
        {
            var segment = route.getSegmentFromIndex(i);
            Handles.DrawBezier(segment[0], segment[3], segment[1], segment[2], Color.green, null, 2);

            Handles.color = Color.red;
            Handles.DrawAAPolyLine(segment[3], segment[2]);
            Handles.DrawAAPolyLine(segment[0], segment[1]);
        }

        // Render Handles
        for (int i = 0; i < route.len; i++)
        {
            Handles.color = i % 3 == 0 ? Color.green : Color.red;
            Vector3 current = route[i];
            Vector3 pos = Handles.PositionHandle(current, Quaternion.identity);
            if (pos != current)
            {
                Undo.RecordObject(creator, "Move object");
                route.movePoint(i, pos);
            }
        }
    }

    /// <summary>Render GUI for custom methods </summary>
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        RouteCreator creator = (RouteCreator)target;

        if (GUILayout.Button("Align"))
        {
            creator.alignHorizontal();
        }
        if (GUILayout.Button("Save"))
        {
            creator.Save();
        }
    }

    /// It loads the data when selecting the object for the first time
    async void OnEnable()
    {
        creator = (RouteCreator)target;
        Debug.Log("Awake");
        if (creator.route == null)
        {
            Debug.Log("route loaded");
            await creator.Load();
        }
        route = creator.route;
    }
}

