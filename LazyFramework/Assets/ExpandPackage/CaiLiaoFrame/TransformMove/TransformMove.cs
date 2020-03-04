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
public class EquipTransInfo
{
	[Header("位移")]
    public Vector3 pos;
	[Header("旋转")]
	public Vector3 rot;
	[Header("缩放")]
	public Vector3 scale;
	[Header("延时")]
	public float  delay;
	[Header("时间")]
	public float time;
}

[System.Serializable]
public class EquipsTransInfo
{
    public List<EquipTransInfo> equipsTransInfo = new List<EquipTransInfo>();
}

//[RequireComponent(typeof(TransformMoveDataModel)), RequireComponent(typeof(TransformMoveLogic)), DisallowMultipleComponent]
public class TransformMove : MonoBehaviour {

    public List<Transform> equipsTrans;

    EquipsTransInfo equipsTransInfo = new EquipsTransInfo();
    EquipTransInfo equipTransInfo = new EquipTransInfo();

    public List<EquipsTransInfo> equipsTransInfos = new List<EquipsTransInfo>();
    [Header("该点处的延迟时间")]
    public float delay = 0;
    [Header("该点处的运动时间")]
    public float time = 1;
    [Header("索引")]
    public int index = 0;

    public Ease ease;

	//#if UNITY_EDITOR
	//#region 添加控制方法
	//[ContextMenu("记录该位置")]
	public void RecordTrans()
	{
        if (equipsTrans.Count == 0)
            equipsTrans.Add(transform);

        equipsTransInfo = new EquipsTransInfo();
        foreach (Transform ts in equipsTrans)
        {
            equipTransInfo = new EquipTransInfo();
            equipTransInfo.pos = ts.localPosition;
            equipTransInfo.rot = ts.localEulerAngles;
            equipTransInfo.scale = ts.localScale;
            equipTransInfo.delay = delay;
            equipTransInfo.time = time;

            equipsTransInfo.equipsTransInfo.Add(equipTransInfo);
        }
        equipsTransInfos.Add(equipsTransInfo);
	}
	//[ContextMenu("增加该索引处位置")]
	public  void AddTrans()
	{
        equipsTransInfo = new EquipsTransInfo();
        foreach (Transform ts in equipsTrans)
        {
            equipTransInfo = new EquipTransInfo();
            equipTransInfo.pos = ts.localPosition;
            equipTransInfo.rot = ts.localEulerAngles;
            equipTransInfo.scale = ts.localScale;
            equipTransInfo.delay = delay;
            equipTransInfo.time = time;

            equipsTransInfo.equipsTransInfo.Add(equipTransInfo);
        }

        equipsTransInfos.Insert(index, equipsTransInfo);

       
	}
	//[ContextMenu("删除该索引处位置")]
	public  void DeleteTrans()
	{
        equipsTransInfos.RemoveAt(index);
	}
	//[ContextMenu("修改该索引处位置")]
	public  void ModifyTrans()
	{
        equipsTransInfo = new EquipsTransInfo();
        foreach (Transform ts in equipsTrans)
        {
            equipTransInfo = new EquipTransInfo();
            equipTransInfo.pos = ts.localPosition;
            equipTransInfo.rot = ts.localEulerAngles;
            equipTransInfo.scale = ts.localScale;
            equipTransInfo.delay = delay;
            equipTransInfo.time = time;

            equipsTransInfo.equipsTransInfo.Add(equipTransInfo);
        }
        equipsTransInfos[index] = equipsTransInfo;

       
	}
	//[ContextMenu("查看该索引处位置")]
	public  void ReviewTrans()
	{
        int i = 0;
        foreach (Transform ts in equipsTrans)
        {
            equipTransInfo = equipsTransInfos[index].equipsTransInfo[i];
             ts.localPosition =equipTransInfo.pos;
             ts.localEulerAngles=equipTransInfo.rot;
             ts.localScale=equipTransInfo.scale;

             delay = equipTransInfo.delay;
             time = equipTransInfo.time;
            i++;
        }
	}

    [ContextMenu("开始动画")]
    public void StartTransformMove()
    {
        gameObject.StartMoves();
    }
	//#endregion
	//#endif

    int num;
	public TransformMove StartMoves( bool useRecord = false)
	{
        num = 0;
        if (equipsTransInfos.Count > 1)
        {
            _Move(equipsTransInfos, useRecord);
        }
       
        return transform.GetComponent<TransformMove>();
	}

    void _Move(List<EquipsTransInfo> equipsTransInfos, bool useRecord = false)
    {
        num++;
        if (num < equipsTransInfos.Count - 1)
        {
            for (int i = 0; i < equipsTrans.Count;i++ )
            {
                equipsTrans[i].gameObject.Move(equipsTransInfos[num].equipsTransInfo[i],ease,useRecord).OnComplete(() => { });
                if(i == equipsTrans.Count-1)
                {
                    equipsTrans[i].gameObject.Move(equipsTransInfos[num].equipsTransInfo[i], ease, useRecord).OnComplete(() => { _Move(equipsTransInfos,useRecord); });
                }
            }
        }
        else
        {
            for (int i = 0; i < equipsTrans.Count; i++)
            {
                equipsTrans[i].gameObject.Move(equipsTransInfos[num].equipsTransInfo[i], ease, useRecord).OnComplete(() => { });
            }
        }
    }
    
    public void DelayAction(float time, UnityAction action)
    {
        StartCoroutine(DelayActionIEnum(time, action));
    }
    IEnumerator DelayActionIEnum(float time, UnityAction action)
    {
        yield return new WaitForSeconds(time);
        if (action != null)
            action.Invoke();
    }
    
    public  void  OnComplete(UnityAction action = null)
    {
        float time = 0;
        for (int i = 0; i < equipsTransInfos.Count; i++)
        {
            time += equipsTransInfos[i].equipsTransInfo[0].delay + equipsTransInfos[i].equipsTransInfo[0].time;
        }

        DelayAction(time, () =>
        {
            if (action != null)
                action();
        });
    }

    public void StopMoves()
    {

        StopAllCoroutines();
        for (int i = 0; i < equipsTrans.Count; i++)
            equipsTrans[i].gameObject.transform.DOPause();
    }
}
#if UNITY_EDITOR

[CustomEditor(typeof(TransformMove))]
public class TransformEditor : Editor
{
    private SerializedProperty cameraTransInfos;

	public override void OnInspectorGUI()
	{
        EditorGUILayout.BeginVertical();
		DrawDefaultInspector();

        TransformMove myScript = (TransformMove)target;

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
            EditorUtility.SetDirty(target);
		}
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
	}
}
#endif
