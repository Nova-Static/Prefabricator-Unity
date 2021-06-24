using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR 
[CustomEditor(typeof(PrefabCollection))]
public class PrefabCollectionEditor : Editor
{
    public override void OnInspectorGUI()
    {
       // base.OnInspectorGUI();
        DrawDefaultInspector();

        PrefabCollection script = (PrefabCollection)target;
        if (GUILayout.Button("Randomize"))
        {
            script.Generate();
        }
    }
}
#endif
