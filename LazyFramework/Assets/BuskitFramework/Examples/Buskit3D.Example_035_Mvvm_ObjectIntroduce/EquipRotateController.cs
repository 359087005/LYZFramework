
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： EquipRotateController
* 创建日期：2019-04-01 16:12:58
* 作者名称：Author
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;

namespace Buskit3D.Example_039_Mvvm_ObjectIntroduce
{
    /// <summary>
    /// 
    /// </summary>
	public class EquipRotateController : MonoBehaviour
    {
        //[Tooltip("")]
        /// <summary>
        /// 当前的小相机
        /// </summary>
        private Camera _currentCamera;
        /// <summary>
        /// 旋转速度
        /// </summary>
        private float roSpeed = 30;
        /// <summary>
        /// 缩放速度
        /// </summary>
        private float scaleSpeed = 2;
        /// <summary>
        /// 控制目标物体
        /// </summary>
        private Transform _target;
        /// <summary>
        /// 开始的位置
        /// </summary>
        private Vector3 _startPos;
        /// <summary>
        /// 开始的旋转
        /// </summary>
        private Quaternion _startRo;
        /// <summary>
        /// 鼠标偏移量
        /// </summary>
        private float x, y;
        /// <summary>
        /// 相机视野
        /// </summary>
        private float fieldOfView;
        public static EquipRotateController instance;
        public int currentID;
        /// <summary>
        /// 数据实体
        /// </summary>
        UIEquipmentDataModelEntity entity = new UIEquipmentDataModelEntity();
        /// <summary>
        /// 生命周期
        /// </summary>
        private void Awake()
        {
            instance = this;
        }
        /// <summary>
        /// 生命周期
        /// </summary>
        private void Start()
        {
            _currentCamera = GameObject.Find("SceneRoot/Camera").GetComponent<Camera>();
            fieldOfView = _currentCamera.fieldOfView;
            _startPos = _currentCamera.transform.position;
            _startRo = _currentCamera.transform.rotation;
            y = _currentCamera.transform.eulerAngles.x;
            x = _currentCamera.transform.eulerAngles.y;
            _target = transform.GetChild(0);
            entity = (UIEquipmentDataModelEntity)FindObjectOfType<UIEquipmentViewDataModel>().DataEntity;
            ResetObject();
        }
        /// <summary>
        /// 生命周期
        /// </summary>
        private void Update()
        {
            Ro();
            ChangedSpeed();
        }
        /// <summary>
        /// 重置物体
        /// </summary>
        public void ResetObject()
        {
            _target = transform.GetChild(entity.currentEquipID);
            _currentCamera.transform.position = _startPos;
            _currentCamera.transform.rotation = _startRo;
            x = _currentCamera.transform.eulerAngles.y;
            y = _currentCamera.transform.eulerAngles.x;
        }
        /// <summary>
        /// 旋转控制
        /// </summary>
        private void Ro()
        {
            //右键操作
            if (Input.GetMouseButton(1))
            {
                x += Input.GetAxis("Mouse X") * roSpeed * 0.05f;
                y -= Input.GetAxis("Mouse Y") * roSpeed * 0.05f;

                Quaternion rotation;

                if (_target != null)
                {
                    rotation = Quaternion.Euler(y, x, 0.0f);
                    Vector3 disVector = new Vector3(0.0f, 0.0f, -0.8f);
                    Vector3 position = rotation * disVector + _target.position;
                    _currentCamera.transform.rotation = Quaternion.Slerp(_currentCamera.transform.rotation, rotation, Time.deltaTime * 5);
                    _currentCamera.transform.position = Vector3.Slerp(_currentCamera.transform.position, position, Time.deltaTime * 5);
                }
            }
        }
        /// <summary>
        /// 速度控制
        /// </summary>
        private void ChangedSpeed()
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * scaleSpeed;
                fieldOfView = Mathf.Clamp(fieldOfView, 30, 80);
                _currentCamera.fieldOfView = fieldOfView;
            }
        }
    }
}