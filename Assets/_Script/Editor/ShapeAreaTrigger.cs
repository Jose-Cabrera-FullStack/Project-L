using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShapeAreaTrigger))]
public class ShapeAreaTriggerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Get a reference to the script component
        ShapeAreaTrigger GUIShapeAreaTrigger = (ShapeAreaTrigger)target;
        string[] propertyToRemove = { };
        // Draw the properties using the PropertyDrawer
        switch (GUIShapeAreaTrigger.shape)
        {
            case ShapeAreaTrigger.Shape.Wedge:
                propertyToRemove = new string[] { "radiusInner", "radiusOuter", };
                break;
            case ShapeAreaTrigger.Shape.Cone:
                propertyToRemove = new string[] { "height", "radius", };
                break;
            case ShapeAreaTrigger.Shape.Circular:
                propertyToRemove = new string[] { "height", "radius", "fovDeg" };
                break;
            default: Debug.Log("gola"); break;
        }

        Undo.RecordObject(GUIShapeAreaTrigger, "changed trigger");
        serializedObject.Update();
        serializedObject.ApplyModifiedProperties();
    }
}