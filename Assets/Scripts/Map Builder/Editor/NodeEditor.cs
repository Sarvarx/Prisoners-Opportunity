using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Node))]
public class NodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Node myScript = (Node)target;
        if (GUILayout.Button("Path"))
        {
            myScript.Path();
        }
    }
}
