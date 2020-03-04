/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：FirstControlDataModel
* 创建日期：2018-04-07 10:58:17
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 功能描述：第一人称数据模型
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/


using UnityEngine;
using Com.Rainier.Buskit3D;

namespace Buskit3D_Example_006_FirstController
{
    /// <summary>
    /// 第一人称数据模型
    /// </summary>
    [RequireComponent(typeof(FirstLogicBehaviour))]
    public class FirstDataModel : DataModelBehaviour
    {

        #region control params
        //是否限制竖直方向旋转
        public bool clampVerticalRotation = true;

        //是否开启平滑旋转
        public bool smooth = true;

        //移动速度
        public float speed = 10;

        //平滑时间
        public float smoothTime = 5f;

        //水平方向敏感系数
        public float xSensitivity = 3f;

        //垂直方向敏感系数
        public float ySensitivity = 3f;

        //旋转最小值
        public float MinimumX = -60f;

        //旋转最大值
        public float MaximumX = 60f;

        //相机的欧拉角
        public Quaternion m_CamRot;

        //playe人的欧拉角
        public Quaternion m_PlayerRot;

        //相机
        public Transform cameraTrans;
        #endregion

        //是否可用
        public bool isUse = true;
       
        /// <summary>
        /// Unity Method
        /// </summary>
        private void Awake()
        {
            this.DataEntity = new FirstDataEntity();
            Watch(this);
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        protected override void Start()
        {
            base.Start();
            cameraTrans = transform.GetChild(0);
            m_CamRot = cameraTrans.localRotation;
            m_PlayerRot = transform.localRotation;
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        public override void Update()
        {
            if (isUse)
            {
                float dh = Input.GetAxis("Horizontal");
                float dv = Input.GetAxis("Vertical");

                if (Input.GetMouseButton(0))
                {
                    float dx = Input.GetAxis("Mouse X")*xSensitivity;
                    float dy = Input.GetAxis("Mouse Y")*ySensitivity;
                    RotPlayer(new Vector2(dx,dy));
                }
                if (dh != 0 || dv != 0)
                {
                    ToMove(new Vector3(dh, 0,dv));
                }
            }

            base.Update();
        }

        /// <summary>
        /// 旋转player
        /// </summary>
        /// <param name="value">鼠标变化值</param>
        public virtual void RotPlayer(Vector2 value)
        {
            FirstDataEntity data = (FirstDataEntity)DataEntity;
            
            m_PlayerRot *= Quaternion.Euler(0f, value.x, 0f);
            m_CamRot *= Quaternion.Euler(-value.y, 0f, 0f);

            if (clampVerticalRotation)
                m_CamRot = ClampRotationAroundXAxis(m_CamRot);
            if (smooth)
            {
                Quaternion playerTemp, cameraTemp;
                playerTemp = Quaternion.Slerp(transform.localRotation, m_PlayerRot, smoothTime * Time.deltaTime);
                cameraTemp = Quaternion.Slerp(cameraTrans.localRotation, m_CamRot, smoothTime * Time.deltaTime);
               
                data.playerRotY = playerTemp.eulerAngles.y;
                data.cameraRotX = cameraTemp.eulerAngles.x;
            }
            else
            {
                data.playerRotY = m_PlayerRot.eulerAngles.y;
                data.cameraRotX = m_CamRot.eulerAngles.x;
            }
        }

        /// <summary>
        /// 人物移动
        /// </summary>
        protected void ToMove(Vector3 dir)
        {
            Vector3 _dir = transform.TransformDirection(dir.x, 0, dir.z) * speed * Time.deltaTime;
            FirstDataEntity data = (FirstDataEntity)DataEntity;
            data.localPosition = _dir;
        }

        /// <summary>
        /// 角度修正
        /// </summary>
        /// <param name="q">相机localrotation</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
            angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }
    }
}

