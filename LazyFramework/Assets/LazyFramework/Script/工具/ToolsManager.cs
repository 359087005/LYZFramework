using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsManager : MonoBehaviour
{
   [Header("测试版本")] [SerializeField] public bool showDebugger;
   [Header("是否显示Scene场景中物体名称标识")][SerializeField] public bool showobjInfo;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    /// <summary>
    /// 面板刷新
    /// </summary>
    private void OnDrawGizmos()
    {
        ToolsCommonField.canShowInfo = showobjInfo;
    }
}
