/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UIController
* 创建日期：2019-01-08 11:30:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：框架Demo_001(数据模型)
* 修改记录：
* 日期 描述：
* 
******************************************************************************/
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit3D.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Rainier.Buskit3D.Example_001
{
    /// <summary>
    /// 对象模型类
    /// </summary>
    public class HelloWorldDataModel : DataModelBehaviour
    {
        //“HelloWorld”UI
        public GameObject HelloWorlld;
        
        //记录保存时的“HelloWorld”UI显隐状态
        private bool HelloWorldIsActive;
        
        //实体对象
        private HelloWorldDataEntity _entity;

        /// <summary>
        /// 监视数据模型实体（HelloWorldDataEntity）
        /// 监视自己的实体模型，并在Awake中初始化
        /// </summary>
        private void Awake()
        {
            HelloWorldDataEntity entity = new HelloWorldDataEntity();
            this.DataEntity = entity;
            Watch(this);
        }

        /// <summary>
        /// 监视自身，一定得执行 base.Start()方法
        /// </summary>
        protected override void Start()
        {
            _entity = (HelloWorldDataEntity)GameObject.FindObjectOfType<HelloWorldDataModel>().DataEntity;   
            base.Start();
        }

        /// <summary>
        /// 按钮点击事件（消息触发器）
        /// </summary>
        public void OnClick()
        {
            _entity.IsShow = !_entity.IsShow;
        }

        /// <summary>
        /// 由于helloWorld面板初始状态为隐藏状态。所以脚本挂载在父物体上，以下重写了还原功能函数。
        /// 重写保存数据函数
        /// </summary>
        public override void SaveStorageData()
        {
            base.SaveStorageData();
            HelloWorldIsActive = HelloWorlld.activeSelf;
        }

        /// <summary>
        /// 重写还原函数
        /// </summary>
        public override void LoadStorageData()
        {
            base.LoadStorageData();
            HelloWorlld.gameObject.SetActive(HelloWorldIsActive);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}


