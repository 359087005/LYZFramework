using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TransformShowEditor : MonoBehaviour
{
    [DrawGizmo(GizmoType.NonSelected)]
    static void DrawGameObjectName(Transform transform, GizmoType gizmoType)
    {
        if(ToolsCommonField.canShowInfo)
        {
            Handles.Label(transform.position, transform.gameObject.name);
        }
     
    }
    
}