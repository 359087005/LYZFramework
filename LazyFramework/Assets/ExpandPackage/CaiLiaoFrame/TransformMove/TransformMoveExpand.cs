using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using DG.Tweening;
using Com.Rainier.Buskit3D;


public static class TransformMoveExpand
{
    public class TransformExpand
    {
       public GameObject go;
       public bool isCamMove;//标记是否是摄像机动画。
       public  float delay;//记录完成运动的时间。
    }
    public static TransformExpand _te = new TransformExpand();

    //物体动画
    public static TransformExpand StartMoves(this GameObject go, bool useRecord = false)
    {
        if (go.GetComponent<TransformMove>())
        {
            go.GetComponent<TransformMove>().StartMoves(useRecord);
            float time = 0;
            for (int i = 0; i < go.GetComponent<TransformMove>().equipsTransInfos.Count; i++)
            {
                if(i!=0)
                    time += go.GetComponent<TransformMove>().equipsTransInfos[i].equipsTransInfo[0].delay + go.GetComponent<TransformMove>().equipsTransInfos[i].equipsTransInfo[0].time;
            }
            _te.go = go;
            _te.isCamMove = false;
            _te.delay = time;
        }
        return _te;
    }

    //摄像机动画
    public static TransformExpand StartMoves(this GameObject go,float delay,float duration,int index = 1, bool useRecord = false)
    {
        go.GetComponent<CameraTransformMove>().StartMoves(delay, duration, index,useRecord);
        _te.go = go;
        _te.isCamMove = true;
        _te.delay = delay + duration;
        return _te;
    }

    public static void OnComplete(this TransformExpand te, UnityAction action = null)
    {
        if (te.isCamMove && te.go.GetComponent<CameraTransformMove>())
        {
            te.go.GetComponent<CameraTransformMove>().DelayAction(te.delay, action);
        }
        else if (!te.isCamMove && te.go.GetComponent<TransformMove>())
        {
            te.go.GetComponent<TransformMove>().DelayAction(te.delay, action);
        }
    }


    public static Tweener Move(this GameObject go, EquipTransInfo equipTransInfo, Ease ease, bool useRecord = false)
    {
        Tweener t;
        if(useRecord)
        {
            TransInfo transInfo = new TransInfo();
            t = go.transform.DOLocalMove(equipTransInfo.pos, equipTransInfo.time).SetDelay(equipTransInfo.delay).SetEase(ease);

            go.transform.DOLocalRotate(equipTransInfo.rot, equipTransInfo.time).SetDelay(equipTransInfo.delay).SetEase(ease);
            go.transform.DOScale(equipTransInfo.scale, equipTransInfo.time).SetDelay(equipTransInfo.delay).SetEase(ease);

            DOTween.To(() => go.transform.localScale, x =>
            {
                transInfo.positon = go.transform.localPosition;
                transInfo.eulerAngles = go.transform.localEulerAngles;
                transInfo.localScale = go.transform.localScale;
                ((TransformMoveDataEntity)go.GetComponent<TransformMoveDataModel>().DataEntity).transInfo = transInfo;
            }, Vector3.one, equipTransInfo.time).SetDelay(equipTransInfo.delay).SetEase(ease);

        }
        else
        {
            t = go.transform.DOLocalMove(equipTransInfo.pos, equipTransInfo.time).SetDelay(equipTransInfo.delay).SetEase(ease);

            go.transform.DOLocalRotate(equipTransInfo.rot, equipTransInfo.time).SetDelay(equipTransInfo.delay).SetEase(ease);
            go.transform.DOScale(equipTransInfo.scale, equipTransInfo.time).SetDelay(equipTransInfo.delay).SetEase(ease);
        }

      
        return t;
    }

    public static void StopMoves(this GameObject go)
    {
        if (go.GetComponent<TransformMove>())
        {
            go.GetComponent<TransformMove>().StopMoves();
        }
        if(go.GetComponent<CameraTransformMove>())
        {
            go.GetComponent<CameraTransformMove>().StopMoves();
        }
    }

}

