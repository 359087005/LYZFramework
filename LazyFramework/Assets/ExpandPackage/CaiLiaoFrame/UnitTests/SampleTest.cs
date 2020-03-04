using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using System.Collections.Generic;
using DG.Tweening;


public class SampleTest : MonoBehaviour
{
    public static SampleTest instance;
    public GameObject cube, cube1;
    public GameObject playerPos;
    public GameObject btn;
    public int sequence;
    /// <summary>
    /// 动画的物体
    /// </summary>
    public List<Transform> transformMoveObj;
    /// <summary>
    /// 点击的物体
    /// </summary>
    public Dictionary<string, GameObject> clickedObj = new Dictionary<string, GameObject>();
    void Awake()
    {
        foreach (TransformMove tm in GameObject.FindObjectsOfType<TransformMove>())
        {
            foreach (Transform t in tm.equipsTrans)
            {
                if (!transformMoveObj.Contains(t))
                    transformMoveObj.Add(t);
            }
        }
    }

    void Start()
    {
        instance = this;
        cube.OneselfName("cube");
        this.BuZhouTiShi("1111111请点击cube,请点击cube");

        playerPos.StartMoves(0, 2f, 1).OnComplete(() =>
        {
            cube.OnHightligher().AddClickedObj(clickedObj).SetClick(CubeClicked);

        });
        btn.SetUIClick((ff) => { Debug.Log(ff.name); Debug.Log("点击的是世界Button"); });

        btn.OnUIHightligher();
        this.SetDelay(3, () => btn.OffUIHightligher());
    }


    void CubeClicked(GameObject go)
    {
        ((SampleDataEntity)SampleDataModel.instance.DataEntity).name = go.name;
    }
    public void CubeClicked(string name)
    {
        if (name == "Cube" && sequence==0)
        {
            sequence++;
            cube.OffHightligher();
            clickedObj["Cube"].StartMoves().OnComplete(() =>
            {
                this.SetDelay(2, () =>
                {
                    cube1.OnHightligher().AddClickedObj(clickedObj).SetClick(CubeClicked);
                    this.BuZhouTiShi("请点击cube");
                });
            });
        }
        else if (name == "Cube1" && sequence == 1)
        {
            sequence++;
            clickedObj["Cube1"].OffHightligher();

            cube1.StartMoves();
            this.SetDelay(1, () =>
            {
                cube1.SetClick(CubeClicked);
                this.BuZhouTiShi("结束演示");
            });
        }
    }
    #region 当方法无参数时触发
    public void BtnClicked()
    {
        ((SampleDataEntity)SampleDataModel.instance.DataEntity).btnClicked++;
    }
    public void BtnClicked(int id)
    {
        Debug.Log("BtnClicked");
    }
    #endregion

    #region 场景恢复用
    public void SaveStorageData()
    {
        foreach (Transform t in transformMoveObj)
        {
            TransInfo transInfo = new TransInfo();
            transInfo.positon = t.localPosition;
            transInfo.eulerAngles = t.localEulerAngles;
            transInfo.localScale = t.localScale;
            transInfo.isActive = t.gameObject.activeSelf;
            ((SampleDataEntity)SampleDataModel.instance.DataEntity).gameObjectList.Add(transInfo);
        }
    }
    public void LoadStorageData()
    {
        for (int i = 0; i < transformMoveObj.Count; i++)
        {
            transformMoveObj[i].localPosition = ((SampleDataEntity)SampleDataModel.instance.DataEntity).gameObjectList[i].positon;
            transformMoveObj[i].localEulerAngles = ((SampleDataEntity)SampleDataModel.instance.DataEntity).gameObjectList[i].eulerAngles;
            transformMoveObj[i].localScale = ((SampleDataEntity)SampleDataModel.instance.DataEntity).gameObjectList[i].localScale;
            transformMoveObj[i].gameObject.SetActive(((SampleDataEntity)SampleDataModel.instance.DataEntity).gameObjectList[i].isActive);
        }
    }
    #endregion
}

