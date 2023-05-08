using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(NodesGenerator))]
public class NodesGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        NodesGenerator myScript = (NodesGenerator)target;
        if (GUILayout.Button("Generate"))
        {
            myScript.Generate();
        }else if (GUILayout.Button("Clear"))
        {
            myScript.Clear();
        }
    }
}
