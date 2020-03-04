/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：UIEquipmentLogic
* 创建日期：2019-04-1 09:36:53
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

//includes for Unity
using UnityEngine;

//includes for System
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// 名称空间定义：SHHY.Otoliths
/// </summary>
namespace Buskit3D.Example_039_Mvvm_ObjectIntroduce
{
    /// <summary>
    /// 类 名 称：SHHY.Otoliths.UIEquipmentLogic
    /// 类 功 能：
    /// 主要接口：
    /// </summary>

    public class UIEquipmentLogic : LogicBehaviour
    {
        /// <summary>
        /// 设备字典【设备ID，设备描述】
        /// </summary>
        private Dictionary<int, string> _contentDic = new Dictionary<int, string>();
        /// <summary>
        /// 设备目录
        /// </summary>
        public Transform[] equipParent;
        /// <summary>
        /// 当前的点击数量标识
        /// </summary>
        private int Num = 0;
        /// <summary>
        /// 生命周期
        /// </summary>
        private void Awake()
        {
            InitDic();
        }
        /// <summary>
        /// 初始化字典
        /// </summary>
        private void InitDic()
        {
            _contentDic.Add(0, "\u3000\u3000研磨机是用涂上或嵌入磨料的研具对工件表面进行研磨的磨床。主要用于研磨工件中的高精度平面、内外圆柱面、圆锥面、球面、螺纹面和其他型面。研磨机的主要类型有圆盘式研磨机、转轴式研磨机和各种专用研磨机。研磨机控制系统以PLC为控制核心，文本显示器为人机对话界面的控制方式。人机对话界面可以就设备维护、运行、故障等信息与人对话；操作界面直观方便、程序控制、操作简单。全方位安全考虑，非正常状态的误操作无效。实时监控，故障、错误报警，维护方便。");
            _contentDic.Add(1, "\u3000\u3000双目显微镜是指可以两个眼睛同时观察的显微镜。随着科学技术的发展，人们发觉用一个眼睛观察很不方便也别扭，于是就诞生了双目显微镜，也就是二只眼睛同时观察，同时还有三目显微镜，第三目通常是用来连接视频成像设备的，也就是把镜下的图片拍摄下来就行保存。双目观察已成为现代目视仪器的特点，其作用是不象单眼观察那样长时间工作而感到疲劳。在大型生物显微镜和金相显微镜中，一般都用分象系统实现双目倾斜观察。");
            _contentDic.Add(2, "\u3000\u3000随着现代机械加工业地发展，对切割的质量、精度要求的不断提高，对提高生产效率、降低生产成本、具有高智能化的自动切割功能的要求也在提升。数控切割机的发展必须要适应现代机械加工业发展的要求。切割机分为火焰切割机、等离子切割机、激光切割机、水切割等。激光切割机为效率最快，切割精度最高，切割厚度一般较小。等离子切割机切割速度也很快，切割面有一定的斜度。火焰切割机针对于厚度较大的碳钢材质。激光切割机为效率最快，切割精度最高，切割厚度一般较小。");
        }
        /// <summary>
        /// 逻辑处理
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
            var entity = (UIEquipmentDataModelEntity)GetComponent<ViewModelBehaviour>().DataEntity;
            if (evt.PropertyName.Equals("IsClose"))
            {
                return;
            }

            if (evt.PropertyName.Equals("IsChangeToNext"))
            {
                if (evt.NewValue.ToString().Equals("0")) return;
                Num++;
                Num = Num % 3;
                entity.currentEquipID = Num;
                entity.content = _contentDic[Num];
                return;
            }

            if (evt.PropertyName.Equals("IsChangeToLast"))
            {
                if (evt.NewValue.ToString().Equals("0")) return;
                Num--;
                if (Num < 0)
                {
                    Num = 2;
                }
                Num = Num % 3;
                entity.currentEquipID = Num;
                entity.content = _contentDic[Num];
                return;
            }

            if (evt.PropertyName.Equals("currentEquipID"))
            {
                for (int i = 0; i < equipParent.Length; i++)
                {
                    if ((int)evt.NewValue == i)
                    {
                        equipParent[i].gameObject.SetActive(true);                        
                    } 
                    else
                    {
                        equipParent[i].gameObject.SetActive(false);
                    }
                }
                EquipRotateController.instance.ResetObject();
            }
        }
    }
}

