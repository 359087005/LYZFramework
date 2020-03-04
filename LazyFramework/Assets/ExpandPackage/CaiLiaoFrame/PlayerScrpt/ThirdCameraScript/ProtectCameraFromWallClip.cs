/************************************************************
  Copyright (C), 2007-2017,BJ Rainier Tech. Co., Ltd.
  FileName: FirstViewControl.cs
  Author:汪海波       Version :1.0          Date: 
  Description: 第三人称控制时，避免主摄像机视角进入物体内部
************************************************************/

using System;
using System.Collections;
using UnityEngine;

    public class ProtectCameraFromWallClip : MonoBehaviour
    {
        private float clipMoveTime = 0.01f;              // time taken to move when avoiding cliping (low value = fast, which it should be)
        private float returnTime = 0.01f;                 // time taken to move back towards desired position, when not clipping (typically should be a higher value than clipMoveTime)
        private float sphereCastRadius = 0.1f;           // the radius of the sphere used to test for object between camera and target
        private bool visualiseInEditor;                  // toggle for visualising the algorithm through lines for the raycast in the editor
        [HideInInspector]
        public float closestDistance = 0.5f;            // the closest distance the camera can be from the target
        public bool protecting { get; private set; }    // used for determining if there is an object between the target and the camera
        [HideInInspector]
        public string dontClipTag = "Player";           // don't clip against objects with this tag (useful for not clipping against the targeted object)

        [HideInInspector]
        public Transform m_CamLocation;                  // the transform of the camera
        private Transform m_Pivot;                // the point at which the camera pivots around
        [HideInInspector]
        public float m_OriginalDist;             // the original distance to the camera before any modification are made
        private float m_MoveVelocity;             // the velocity at which the camera moved
        private float m_CurrentDist;              // the current distance from the camera to the target
        private Ray m_Ray;                        // the ray used in the lateupdate for casting between the camera and the target
        private RaycastHit[] m_Hits;              // the hits between the camera and the target
        private RayHitComparer m_RayHitComparer;  // variable to compare raycast hit distances

       
        private void Start()
        {
            // find the camera in the object hierarchy
           // m_Cam = GetComponentInChildren<Camera>().transform;
            m_CamLocation = FreeLookCam.instance.m_CamLocation;
            m_Pivot = m_CamLocation.parent;
            m_OriginalDist = m_CamLocation.localPosition.magnitude;
            m_CurrentDist = m_OriginalDist;
            // create a new RayHitComparer
            m_RayHitComparer = new RayHitComparer();
        }


        private void LateUpdate()
        {
            // initially set the target distance
            float targetDist = m_OriginalDist;

            m_Ray.origin = m_Pivot.position + m_Pivot.forward*sphereCastRadius;
            m_Ray.direction = -m_Pivot.forward;

            // initial check to see if start of spherecast intersects anything
            var cols = Physics.OverlapSphere(m_Ray.origin, sphereCastRadius);

            bool initialIntersect = false;
            bool hitSomething = false;

            // loop through all the collisions to check if something we care about
            for (int i = 0; i < cols.Length; i++)
            {
                // if ((!cols[i].isTrigger) && !(cols[i].attachedRigidbody != null && cols[i].attachedRigidbody.CompareTag(dontClipTag)))
                if ((!cols[i].isTrigger) && !(cols[i].gameObject.CompareTag(dontClipTag)))
                {
                    initialIntersect = true;
                    break;
                }
            }

            // if there is a collision
            if (initialIntersect)
            {
                m_Ray.origin += m_Pivot.forward*sphereCastRadius;

                // do a raycast and gather all the intersections
                m_Hits = Physics.RaycastAll(m_Ray, m_OriginalDist - sphereCastRadius);
            }
            else
            {
                // if there was no collision do a sphere cast to see if there were any other collisions
                m_Hits = Physics.SphereCastAll(m_Ray, sphereCastRadius, m_OriginalDist + sphereCastRadius);
            }

            // sort the collisions by distance
            Array.Sort(m_Hits, m_RayHitComparer);

            // set the variable used for storing the closest to be as far as possible
            float nearest = Mathf.Infinity;

            // loop through all the collisions
            for (int i = 0; i < m_Hits.Length; i++)
            {
                // only deal with the collision if it was closer than the previous one, not a trigger, and not attached to a rigidbody tagged with the dontClipTag
              /*  if (m_Hits[i].distance < nearest && (!m_Hits[i].collider.isTrigger) &&
                    !(m_Hits[i].collider.attachedRigidbody != null &&
                      m_Hits[i].collider.attachedRigidbody.CompareTag(dontClipTag)))*/
                if (m_Hits[i].distance < nearest && (!m_Hits[i].collider.isTrigger) &&
                    !(m_Hits[i].collider.gameObject.CompareTag(dontClipTag)))
                {
                    // change the nearest collision to latest
                    nearest = m_Hits[i].distance;
                    targetDist = -m_Pivot.InverseTransformPoint(m_Hits[i].point).z;
                    hitSomething = true;
                }
            }

            // visualise the cam clip effect in the editor
            if (hitSomething)
            {
               // Debug.DrawRay(m_Ray.origin, -m_Pivot.forward*(targetDist + sphereCastRadius), Color.red);
            }

            // hit something so move the camera to a better position
            protecting = hitSomething;
            m_CurrentDist = Mathf.SmoothDamp(m_CurrentDist, targetDist, ref m_MoveVelocity,
                                           m_CurrentDist > targetDist ? clipMoveTime : returnTime);
            m_CurrentDist = Mathf.Clamp(m_CurrentDist, FreeLookCam.instance.minDis, m_OriginalDist);

            m_CurrentDist = Mathf.Clamp(m_CurrentDist, FreeLookCam.instance.minDis, FreeLookCam.instance.maxDis);
            m_CamLocation.localPosition = -Vector3.forward * m_CurrentDist;
            
        }


        // comparer for check distances in ray cast hits
        public class RayHitComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                return ((RaycastHit) x).distance.CompareTo(((RaycastHit) y).distance);
            }
        }
    }

