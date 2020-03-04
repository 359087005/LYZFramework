/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：ObjectAnim
* 创建日期：2020-2-27
* 作者名称：林奕州
* CLR 版本：4.0.30319.42000
* 功能描述：物体动画路线绘制
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Lazy
{
    public class ObjectAnim : MonoBehaviour
    {
        public int curSelectGroup = -1;
        public int CurSelectGroup
        {
            get
            {
                return curSelectGroup;
            }
            set
            {
                curSelectGroup = value;
            }
        }
        
        public int curNode = -1;
        public int CurNode
        {
            get
            {
                return curNode;
            }
            set
            {
                curNode = value;
            }
        }
      
        List<Transform> actor = new List<Transform>();
        public float dalay = 0;
        public float time = 1;
        public Ease ease;
        [Header("该物体所有动画")]
        public List<AnimGroup> animGroups = new List<AnimGroup>();

        /// <summary>
        /// 增加动画节点
        /// </summary>
        public void AddNode()
        {
            AnimNodeInfo temp = new AnimNodeInfo();
            for (int i = 0; i < animGroups[CurSelectGroup].actor.Count; i++)
            {
                temp.objectPointInfo.Add(new ObjectPointInfo()
                {
                    pos = animGroups[CurSelectGroup].actor[i].transform.position,
                    rot = animGroups[CurSelectGroup].actor[i].transform.eulerAngles,
                    sca = animGroups[CurSelectGroup].actor[i].transform.localScale,
                    time = time,
                    delay = dalay,
                });
            }
            animGroups[CurSelectGroup].animNodeInfo.Add(temp);
        }
      
        /// <summary>
        /// 增加一组动画
        /// </summary>
        public void AddGroup()
        {
            ClearPanel();
            string gropuName = "anim_";
            int count = 0;
            while (count < 100)
            {
                count++;
                if (CheckGroupName(gropuName + count)) break;
            }
            AddGroupData(gropuName+count);
        }
        public void AddGroupData(string _name)
        {
            if (CheckGroupName(_name))
            {
                animGroups.Add(new AnimGroup()
                {
                    groupName = _name,
                });
            }
        }
        public void ClearPanel()
        {
            CurNode = -1;
            dalay = 0;
            time = 1;
            ease = Ease.Unset;
        }
        private bool CheckGroupName(string _name)
        {
            for (int i = 0; i < animGroups.Count; i++)
            {
                if(animGroups[i].groupName==_name)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 删除当前节点
        /// </summary>
        public void RemoveNode()
        {

        }
        /// <summary>
        /// 删除当前组
        /// </summary>
        /// <param name="index"></param>
        public void RemoveGroup()
        {
            animGroups.RemoveAt(curSelectGroup);;
        }
      
      
     
//#if UNITY_EDITOR
//        void OnDrawGizmos()
//        {
//            Camera[] mCam = this.GetComponentsInChildren<Camera>();
//            List<Transform> mCamTrans = new List<Transform>();
//            List<Transform> mPlayTrans = new List<Transform>();
//            for (int i = 0; i < mCam.Length; i++)
//            {
//                mCamTrans.Add(mCam[i].transform);
//                //mCam[i].enabled = false;
//                mPlayTrans.Add(mCam[i].transform.parent);
//            }
//            DrawGizmos(mCamTrans, Color.red);
//            DrawGizmos(mPlayTrans, Color.blue);
//        }
//        private void DrawGizmos(List<Transform> mTrans, Color DrawColor)
//        {
//            if (mTrans.Count > 0)
//            {
//                Gizmos.color = DrawColor;
//                for (int i = 0; i < mTrans.Count - 1; i++)
//                {
//                    Gizmos.DrawSphere(mTrans[i].position, 0.1f);
//                    Gizmos.DrawLine(mTrans[i].position, mTrans[i + 1].position);
//                }
//                Gizmos.DrawSphere(mTrans[mTrans.Count - 1].position, 0.1f);
//            }
//        }
//#endif
    }
    [System.Serializable]
    public class AnimGroup
    {
        [Header("动画组名称")]
        public string groupName = "";
        public List<Transform> actor = new List<Transform>();
        /// <summary>
        /// 一组动画中的每一个动画节点
        /// </summary>
        public List<AnimNodeInfo> animNodeInfo = new List<AnimNodeInfo>();
    }
    /// <summary>
    /// 每一个物体的点信息
    /// </summary>
    [System.Serializable]
    public class ObjectPointInfo
    {
        [Header("位移")]
        public Vector3 pos;
        [Header("旋转")]
        public Vector3 rot;
        [Header("缩放")]
        public Vector3 sca;
        [Header("延时")]
        public float delay;
        [Header("时间")]
        public float time;
    }
    /// <summary>
    /// 动画节点信息
    /// </summary>
    [System.Serializable]
    public class AnimNodeInfo
    {
        public List<ObjectPointInfo> objectPointInfo = new List<ObjectPointInfo>();
        [Header("运动到该点执行的事件")]
        public UnityEvent OnAnimFinish;
    }
    /// <summary>
    /// 编辑器
    /// </summary>
    [CustomEditor(typeof(ObjectAnim))]
    public class ObjectAnimEditor : Editor
    {
        Transform temp = null;
        string ReName = "";
        List<Transform> actor = new List<Transform>() {};
        Vector2 animGroup = new Vector2(100,100);
       
        private void OnEnable()
        {
         
        }
       
        public override void OnInspectorGUI()
        {
            ObjectAnim myScript = (ObjectAnim)target;
            AddGroup(myScript);
            SetGroup(myScript);
            AnimInfoOperation(myScript);
            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }
        public void UpdatePanel(ObjectAnim myScript)
        {
            actor = myScript.animGroups[myScript.CurSelectGroup].actor;
          
        }
        private void SetGroup(ObjectAnim myScript)
        {
            EditorGUILayout.BeginHorizontal();
            if (myScript.animGroups.Count > 0)
            {
                EditorGUILayout.LabelField("动画重命名：");
                ReName = EditorGUILayout.TextField(ReName);
                GUI.backgroundColor = Color.green;
                if (GUILayout.Button("确认"))
                {
                    myScript.animGroups[myScript.curSelectGroup].groupName = ReName;
                }

                GUI.backgroundColor = Color.white;
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                GUI.backgroundColor = Color.green;
                if (GUILayout.Button("添加成员", GUILayout.MinWidth(20)))
                {
                    actor.Add(null);
                }
                EditorGUILayout.BeginVertical();
                for (int i = 0; i < actor.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    actor[i] = (Transform)EditorGUILayout.ObjectField(actor[i], typeof(Transform), true, GUILayout.MinWidth(50));
                    GUI.backgroundColor = Color.red;
                    if (GUILayout.Button("删除成员"))
                    {
                        actor.RemoveAt(i);
                    }
                    GUI.backgroundColor = Color.green;
                    EditorGUILayout.EndHorizontal();
                }
                GUI.backgroundColor = Color.white;
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }
        }
       
        private void AddGroup(ObjectAnim myScript)
        {
            EditorGUILayout.BeginVertical();
       
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("增加一组动画", GUILayout.MinWidth(50)))
            {
                myScript.AddGroup();
            }
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUI.backgroundColor = new Color(0.2f, 0.9f, 0.9f, 1);
            for (int i = 0; i < myScript.animGroups.Count; i++)
            {
              
                if (myScript.curSelectGroup == i)
                {
                    GUI.backgroundColor = new Color(0.5f, 0.6f, 0.6f, 1);
                    if (GUILayout.Button(myScript.animGroups[i].groupName))
                    {
                        myScript.curSelectGroup = i;
                        ReName = "";
                    }
                    GUI.backgroundColor = new Color(0.2f, 0.9f, 0.9f, 1);
                }
                else
                {
                    if (GUILayout.Button(myScript.animGroups[i].groupName))
                    {
                        myScript.curSelectGroup = i;
                        ReName = "";
                        Debug.Log("Update");
                        UpdatePanel(myScript);
                        //myScript.UpdatePanel();
                    }
                }
            }
            GUI.backgroundColor = Color.white;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        private void AnimInfoOperation(ObjectAnim myScript)
        {
            if (myScript.animGroups.Count > 0)
            {
                DrawDefaultInspector();
                GUILayout.Space(20);
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();

                GUI.backgroundColor = Color.green;
                if (GUILayout.Button("增加节点"))
                {
                    myScript.AddNode();
                }
                GUI.backgroundColor = Color.white;
                if (GUILayout.Button("修改节点"))
                {
                    //myScript
                }
                if (GUILayout.Button("查看节点"))
                {
                    //myScript
                }
                if (GUILayout.Button("模拟运行"))
                {
                    //myScript
                }
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("删除节点"))
                {
                    //myScript
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("删除当前动画组"))
                {
                    myScript.RemoveGroup();
                }
                GUI.backgroundColor = Color.white;
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }
        }
    }
}
