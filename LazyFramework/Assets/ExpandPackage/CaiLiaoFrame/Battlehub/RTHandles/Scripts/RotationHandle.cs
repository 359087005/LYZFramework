using System;
using UnityEngine;

namespace Battlehub.RTHandles
{
    //NOTE: Does not work with Global pivot rotation (always local)
    public enum Axis
    {
        xyz,
        x,
        y,
        z,
    }
    public class RotationHandle : BaseHandle
    {
        [HideInInspector]
        public float GridSize = 15.0f;
        [HideInInspector]
        public float XSpeed = 10.0f;
        [HideInInspector]
        public float YSpeed = 10.0f;

        private Matrix4x4 m_targetInverse;
        private Matrix4x4 m_matrix;
        private Matrix4x4 m_inverse;
   
        private  float innerRadius = 1.0f;
        private  float outerRadius = 1.2f;
        private const float hitDot = 0.2f;

        private float m_deltaX;
        private float m_deltaY;

        [SerializeField, TooltipAttribute("轴长度")]
        public float circleSize = 1f;//轴长度

        private Axis mAxis;
        float minAngle;
        float maxAngle;

        public static RotationHandle Current
        {
            get;
            private set;
        }

        protected override RuntimeTool Tool
        {
            get { return RuntimeTool.Rotate; }
        }

        protected override float CurrentGridSize
        {
            get { return GridSize; }
        }

        protected override void StartOverride()
        {
            mAxis = gameObject.GetComponent<Handle>().mAxis;
            Current = this;
        }

        protected override void OnDestroyOverride()
        {
            if (Current == this)
            {
                Current = null;
            }
        }

        protected override void OnEnableOverride()
        {
            base.OnEnableOverride();
            
        }

        private bool Intersect(Ray r, Vector3 sphereCenter, float sphereRadius, out float hit1Distance, out float hit2Distance)
        {
            hit1Distance = 0.0f;
            hit2Distance = 0.0f;
           
            Vector3 L = sphereCenter - r.origin;
            float tc = Vector3.Dot(L, r.direction);
            if (tc < 0.0)
            {
                return false;
            }

            float d2 = Vector3.Dot(L, L) - (tc * tc);
            float radius2 = sphereRadius * sphereRadius;
            if (d2 > radius2)
            {
                return false;
            }

            float t1c = Mathf.Sqrt(radius2 - d2);
            hit1Distance = tc - t1c;
            hit2Distance = tc + t1c;

            return true;
        }

        private RuntimeHandleAxis Hit()
        {
            float hit1Distance;
            float hit2Distance;
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
            float scale = RuntimeHandles.GetScreenScale(Target.position, Camera);
            if (Intersect(ray, Target.position, outerRadius * scale, out hit1Distance, out hit2Distance))
            {
                Vector3 dpHitPoint;
                GetPointOnDragPlane(GetDragPlane(), Input.mousePosition, out dpHitPoint);
                bool isInside = (dpHitPoint - Target.position).magnitude <= innerRadius * scale;

                if(isInside)
                {
                    Intersect(ray, Target.position, innerRadius * scale, out hit1Distance, out hit2Distance);
                    
                    Vector3 hitPoint = m_targetInverse.MultiplyPoint(ray.GetPoint(hit1Distance));
                    Vector3 radiusVector = hitPoint.normalized;
               
                    float dotX = Mathf.Abs(Vector3.Dot(radiusVector, Vector3.right));
                    float dotY = Mathf.Abs(Vector3.Dot(radiusVector, Vector3.up));
                    float dotZ = Mathf.Abs(Vector3.Dot(radiusVector, Vector3.forward));

                    if (dotX < hitDot)
                    {
                        return RuntimeHandleAxis.X;
                    }
                    else if (dotY < hitDot)
                    {
                        return RuntimeHandleAxis.Y;
                    }
                    else if (dotZ < hitDot)
                    {
                        return RuntimeHandleAxis.Z;
                    }
                    else
                    {
                        return RuntimeHandleAxis.Free;
                    }
                }
                else
                {
                    return RuntimeHandleAxis.None;
                    //return RuntimeHandleAxis.Screen;
                }
            }

            return RuntimeHandleAxis.None;
        }

        private float angleX, angleY, angleZ;
        protected override bool OnBeginDrag()
        {
            mAxis = gameObject.GetComponent<Handle>().mAxis;
            minAngle = gameObject.GetComponent<Handle>().minAngle;
            maxAngle = gameObject.GetComponent<Handle>().maxAngle;

            outerRadius = 1.2f * circleSize;
            innerRadius = 1 * circleSize;
            m_targetInverse = Matrix4x4.TRS(Target.position, Target.rotation, Vector3.one).inverse;
            SelectedAxis = Hit();
            m_deltaX = 0.0f;
            m_deltaY = 0.0f;

            if (SelectedAxis == RuntimeHandleAxis.Screen)
            {
                Vector2 center = Camera.WorldToScreenPoint(Target.position);
                Vector2 point = Input.mousePosition;

                float angle = Mathf.Atan2(point.y - center.y, point.x - center.x);
                m_matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.forward), Vector3.one);
            }
            else
            {
                m_matrix = Matrix4x4.TRS(Vector3.zero, Target.rotation, Vector3.one);
            }
            m_inverse = m_matrix.inverse;

            return SelectedAxis != RuntimeHandleAxis.None;
        }
          protected override void OnDrag()
        {
            float deltaX = Input.GetAxis("Mouse X");
            float deltaY = Input.GetAxis("Mouse Y");

            deltaX = deltaX * XSpeed;
            deltaY = deltaY * YSpeed;

            m_deltaX += deltaX;
            m_deltaY += deltaY;

            Vector3 delta = m_inverse.MultiplyVector(Camera.cameraToWorldMatrix.MultiplyVector(new Vector3(m_deltaY, -m_deltaX, 0)));
            Quaternion rotation = Quaternion.identity;

            if (SelectedAxis == RuntimeHandleAxis.X && (mAxis == Axis.xyz || mAxis == Axis.x))
            {
                if (EffectiveGridSize != 0.0f)
                {
                    if(Mathf.Abs(delta.x) >= EffectiveGridSize)
                    {
                        delta.x = Mathf.Sign(delta.x) * EffectiveGridSize;
                        m_deltaX = 0.0f;
                        m_deltaY = 0.0f;
                    }
                    else
                    {
                        delta.x = 0.0f;
                    }
                }
                if (mAxis == Axis.x)
                {
                    angleX += delta.x;
                    if(angleX>minAngle&&angleX<maxAngle)
                    {
                        rotation = Quaternion.Euler(delta.x, 0, 0);
                    }
                    else
                    {
                        angleX -= delta.x;
                    }
                }
                else if (mAxis == Axis.xyz)
                {
                    rotation = Quaternion.Euler(delta.x, 0, 0);
                }
                
            }
            else if (SelectedAxis == RuntimeHandleAxis.Y && (mAxis == Axis.xyz || mAxis == Axis.y))
            {
                if (EffectiveGridSize != 0.0f)
                {
                    if (Mathf.Abs(delta.y) >= EffectiveGridSize)
                    {
                        delta.y = Mathf.Sign(delta.y) * EffectiveGridSize;
                        m_deltaX = 0.0f;
                        m_deltaY = 0.0f;
                    }
                    else
                    {
                        delta.y = 0.0f;
                    }
                }

                if (mAxis == Axis.y)
                {
                    angleY += delta.y;
                    if (angleY > minAngle && angleY < maxAngle)
                    {
                        rotation = Quaternion.Euler(0, delta.y, 0);
                    }
                    else
                    {
                        angleY -= delta.y;
                    }
                }
                else if (mAxis == Axis.xyz)
                {
                    rotation = Quaternion.Euler(0, delta.y, 0);
                }
                
            }
            else if (SelectedAxis == RuntimeHandleAxis.Z&&(mAxis == Axis.xyz||mAxis == Axis.z))
            {
                if (EffectiveGridSize != 0.0f)
                {
                    if (Mathf.Abs(delta.z) >= EffectiveGridSize)
                    {
                        delta.z = Mathf.Sign(delta.z) * EffectiveGridSize;
                        m_deltaX = 0.0f;
                        m_deltaY = 0.0f;
                    }
                    else
                    {
                        delta.z = 0.0f;
                    }
                }
                if (mAxis == Axis.z)
                {
                    angleZ += delta.z;
                    if (angleZ > minAngle && angleZ < maxAngle)
                    {
                        rotation = Quaternion.Euler(0, 0, delta.z);
                    }
                    else
                    {
                        angleZ -= delta.z;
                    }
                }
                else if (mAxis == Axis.xyz)
                {
                    rotation = Quaternion.Euler(0, 0, delta.z);
                }
                
            }
            //else if(SelectedAxis == RuntimeHandleAxis.Free)
            //{
            //    rotation = Quaternion.Euler(delta.x, delta.y, delta.z);
            //    m_deltaX = 0.0f;
            //    m_deltaY = 0.0f;
            //}
            //else
            //{
            //    delta = m_inverse.MultiplyVector(new Vector3(m_deltaY, -m_deltaX, 0));
            //    if (EffectiveGridSize != 0.0f)
            //    {
            //        if (Mathf.Abs(delta.x) >= EffectiveGridSize)
            //        {
            //            delta.x = Mathf.Sign(delta.x) * EffectiveGridSize;
            //            m_deltaX = 0.0f;
            //            m_deltaY = 0.0f;
            //        }
            //        else
            //        {
            //            delta.x = 0.0f;
            //        }
            //    }
            //    Vector3 axis = m_targetInverse.MultiplyVector(Camera.cameraToWorldMatrix.MultiplyVector(-Vector3.forward));
            //    rotation = Quaternion.AngleAxis(delta.x, axis);
            //}

            if (EffectiveGridSize == 0.0f)
            {
                m_deltaX = 0.0f;
                m_deltaY = 0.0f;
            }

            for (int i = 0; i < Targets.Length; ++i)
            {
                Targets[i].rotation *= rotation;
            }
        }

       
        private float MinimumX = -60F;
        private float MaximumX = 60F;
        Quaternion ClampRotationAroundXAxis(Quaternion q)
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
        float TiaoZhenJiaoDu(float angle)
        {
            if (angle > 180)
                return angle - 360;
            else if(angle <= -180)
                return angle + 360;
            return angle ;
        }
        protected override void DrawOverride()
        {
            RuntimeHandles.DoRotationHandle(Target.rotation, Target.position, circleSize,mAxis, SelectedAxis);
        }
    }
}