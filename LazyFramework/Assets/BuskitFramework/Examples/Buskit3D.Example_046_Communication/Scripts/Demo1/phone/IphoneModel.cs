/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： IphoneModel
* 创建日期：2019-04-08 16:03:54
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_045_IoCApplication
{
	/// <summary>
    /// 
    /// </summary>
	public class IphoneModel : DataModelBehaviour
	{
        /// <summary>
        /// IphoneID
        /// </summary>
        public int iphoneID;
        /// <summary>
        /// 所在的区域塔
        /// </summary>
        public TaModel taModel;
        /// <summary>
        /// 通讯的目标
        /// </summary>
        public IphoneModel targetIphoneModel;

        private void Awake()
        {
            this.DataEntity = new IphoneEntity();
            Watch(this);
        }

        public override void LoadStorageData()
        {
            base.LoadStorageData();
            Watch(this);
        }
    }
}

