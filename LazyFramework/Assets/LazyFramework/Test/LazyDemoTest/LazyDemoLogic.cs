using Lazy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LazyDemoLogic : ProcessMgr
{
    public GameObject 小球;
    public GameObject 方块;
    protected override void Init()
    {
        BoxManager.Instance.ShowBox("步骤提示");
        this.SetDelay(1, () => { NextStep(); });
        AddStep("第1模块", "装配方块", () =>
        {
            this.BuZhouTiShi("将方块放入方块容器中");
            AssembleManager.Instance.OnMatch_OneToMany("方块",new List<string>() { "方块容器 (2)", "方块容器 (1)" }, () =>{ NextStep();});
        });
        AddStep("第1模块", "装配胶囊", () =>
        {
            this.BuZhouTiShi("将红色胶囊放入最右侧胶囊容器中");
            this.StartAssemble("红色胶囊", "胶囊容器 (2)");
            AssembleManager.Instance.OnMatch_ContainerFull(new List<string>() { "胶囊容器 (2)" },()=>{NextStep();});
        });
        AddStep("第1模块", "挂胶囊", () =>
        {
            this.BuZhouTiShi("将粉色胶囊挂在红色胶囊下");
            this.StartAssemble("粉色胶囊", "红色胶囊");
            AssembleManager.Instance.OnMatch_OneToOne("粉色胶囊", "红色胶囊", () =>{NextStep();});
        });
        AddStep("第2模块", "组装小人儿", () =>
        {
            this.BuZhouTiShi("将地上的头、胳膊、腿 组装起来");
            AssembleManager.Instance.OnMatch_ContainerFull(new List<string>() { "腿容器", "腿容器1", "胳膊容器", "胳膊容器1", "头容器" }, () =>{ NextStep();});
        });
        AddStep("第3模块", "移动小球", () =>
        {
            this.BuZhouTiShi("点击小球");
            小球.OnHightligher();
            小球.OnMouseClick_Once((a) =>
            {
                小球.StartMoves().OnComplete(() =>
                {
                    小球.OffHightligher();
                    NextStep();
                });
            });
        });
        AddStep("第3模块", "拖动方块",() =>
        {
            this.BuZhouTiShi("拖动方块到小球上后触发答题");
            方块.OnHightligher();
            方块.GetComponent<DragObject>().canMove = true;
            方块.OnTriggerEnter(OnTriggerFunc);
        });
        AddStep("第3模块", "答题", () =>
        {
            方块.OffTriggerEnter(OnTriggerFunc);
            this.BuZhouTiShi("开始答题");
            BoxManager.Instance.ShowBox("答题");
            ExamPanelManager.Instance.ShowExam(1,6);
            FirstViewControl.instance.SetIsCanRotate(true);
        });
    }
    private void OnTriggerFunc(GameObject go)
    {
        if (go == 小球)
        {
            方块.GetComponent<DragObject>().canMove = false;
            方块.OffHightligher();
            NextStep();
        }
    }

}
