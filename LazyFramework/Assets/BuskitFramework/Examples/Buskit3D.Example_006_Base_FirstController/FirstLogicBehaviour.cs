/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：FirstControlLogicBehavioru
* 创建日期：2018-04-07 10:58:17
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 功能描述：第一人称控制逻辑
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/
using UnityEngine;
using UnityEditor;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D_Example_006_FirstController
{
    /// <summary>
    /// 第一人称控制逻辑
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class FirstLogicBehaviour : LogicBehaviour
    {
        //人称控制器
        private CharacterController _controller;

        /// <summary>
        /// Unity Method
        /// </summary>
        private void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        public override void ProcessLogic(PropertyMessage evt)
        {
            if (evt.PropertyName.Equals("localPosition"))
            {
                UpdatePosition((Vector3)(evt.NewValue));
            }
            if (evt.PropertyName.Equals("playerRotY"))
            {
                UpdateRotation((float)(evt.NewValue));
            }

        }

        /// <summary>
        /// 更新位置
        /// </summary>
        /// <param name="dir"></param>
        public void UpdatePosition(Vector3 dir)
        {
            _controller.Move(dir);
        }

        /// <summary>
        /// 更新旋转角
        /// </summary>
        /// <param name="rotY"></param>
        public void UpdateRotation(float rotY)
        {
            transform.localEulerAngles = new Vector3(0,rotY,0);
        }


#if UNITY_EDITOR
        [MenuItem("GameObject/Rainier/FirstPlayer", priority = 0)]
        public static void Creat()
        {
           
            GameObject obj = new GameObject("FirstPlayer");
            GameObject cam = new GameObject("Camera");
            cam.AddComponent<Camera>();
            CharacterController con =  obj.AddComponent<CharacterController>();
            con.height = 2.0f;
            con.radius = 0.2f;
            con.transform.position = Vector3.up;
            obj.AddComponent<FirstDataModel>();
            cam.AddComponent<FirstCameraDataModel>();
            cam.transform.SetParent(obj.transform);
            cam.transform.localPosition = Vector3.up * 0.5f;
        }
#endif
    }
}

