using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using DG.Tweening;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class PlayerTransInfo
{
    [Header("位移")]
    public Vector3 playerPos;
    [Header("旋转")]
    public Vector3 playerRot;
    [Header("位移")]
    public Vector3 cameraPos;
    [Header("旋转")]
    public Vector3 cameraRot;
    [Header("fieldOfView")]
    public float fov;
}

public class CameraTransformMove : MonoBehaviour {

    public Transform playerTrans,cameraTrans;
    private PlayerTransInfo playerTransInfo = new PlayerTransInfo();

    /// <summary>
    /// 第一个变量不要，只是记录原始位置
    /// </summary>
    public List<PlayerTransInfo> playerTransInfos = new List<PlayerTransInfo>();
   
    [Header("位置索引")]
    public int index = 0;


    //[ContextMenu("记录该位置")]
    public void RecordTrans()
    {
        if (playerTrans == null)
        {
            playerTrans = GameObject.Find("Player").transform;
            cameraTrans = playerTrans.Find("Main Camera");
        }
        playerTransInfo = new PlayerTransInfo();
        playerTransInfo.playerPos = playerTrans.localPosition;
        playerTransInfo.playerRot = playerTrans.localEulerAngles;
        playerTransInfo.cameraPos = cameraTrans.localPosition;
        playerTransInfo.cameraRot = cameraTrans.localEulerAngles;
        playerTransInfo.fov = cameraTrans.GetComponent<Camera>().fieldOfView;

        playerTransInfos.Add(playerTransInfo);
    }
    //[ContextMenu("增加该索引处位置")]
    public void AddTrans()
    {
        playerTransInfo = new PlayerTransInfo();
        playerTransInfo.playerPos = playerTrans.localPosition;
        playerTransInfo.playerRot = playerTrans.localEulerAngles;
        playerTransInfo.cameraPos = cameraTrans.localPosition;
        playerTransInfo.cameraRot = cameraTrans.localEulerAngles;
        playerTransInfo.fov = cameraTrans.GetComponent<Camera>().fieldOfView;

        playerTransInfos.Insert(index, playerTransInfo);
    }
    //[ContextMenu("删除该索引处位置")]
    public void DeleteTrans()
    {
        playerTransInfos.RemoveAt(index);

    }
    //[ContextMenu("修改该索引处位置")]
    public void ModifyTrans()
    {
        playerTransInfo = new PlayerTransInfo();
        playerTransInfo.playerPos = playerTrans.localPosition;
        playerTransInfo.playerRot = playerTrans.localEulerAngles;
        playerTransInfo.cameraPos = cameraTrans.localPosition;
        playerTransInfo.cameraRot = cameraTrans.localEulerAngles;
        playerTransInfo.fov = cameraTrans.GetComponent<Camera>().fieldOfView;

        playerTransInfos[index] = playerTransInfo;
    }
    //[ContextMenu("查看该索引处位置")]
    public void ReviewTrans()
    {
        playerTrans.localPosition = playerTransInfos[index].playerPos;
        playerTrans.localEulerAngles = playerTransInfos[index].playerRot;
        cameraTrans.localPosition = playerTransInfos[index].cameraPos;
        cameraTrans.localEulerAngles = playerTransInfos[index].cameraRot;
        cameraTrans.GetComponent<Camera>().fieldOfView = playerTransInfos[index].fov;
    }
    //#endregion
    //#endif

    int num;
    public void StartMoves(float delay, float duration, int index = 1, bool useRecord = false)
    {
        FirstViewControl.instance.MoveToDestination(playerTransInfos[index].playerPos,
            playerTransInfos[index].playerRot,
            playerTransInfos[index].cameraPos,
            playerTransInfos[index].cameraRot,
            delay,duration,  playerTransInfos[index].fov,useRecord
            );
    }
    public void DelayAction(float time, UnityAction action)
    {
       
        StartCoroutine(DelayActionIEnum(time, action));
    }
    IEnumerator DelayActionIEnum(float time, UnityAction action)
    {
        yield return new WaitForSeconds(time);
        if(action !=null)
        action.Invoke();
    }

    public void StopMoves()
    {
        StopAllCoroutines();
        playerTrans.gameObject.transform.DOPause();
        cameraTrans.gameObject.transform.DOPause();
    }
    
}
#if UNITY_EDITOR

[CustomEditor(typeof(CameraTransformMove))]
public class CameraTransformEditor : Editor
{
    private SerializedProperty cameraTransInfos;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
        DrawDefaultInspector();

        CameraTransformMove myScript = (CameraTransformMove)target;

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("记录"))
        {
            myScript.RecordTrans();
        }
        if (GUILayout.Button("增加"))
        {
            myScript.AddTrans();
        }
        if (GUILayout.Button("删除"))
        {
            myScript.DeleteTrans();
        }
        if (GUILayout.Button("修改"))
        {
            myScript.ModifyTrans();
        }
        if (GUILayout.Button("查看"))
        {
            myScript.ReviewTrans();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(myScript.playerTrans);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }
}
#endif
