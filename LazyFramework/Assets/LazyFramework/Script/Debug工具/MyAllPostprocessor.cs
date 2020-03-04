/*******************************************************************************
* 类 名 称： MyAllPostprocessor
* 创建日期：2020-02-28 15:32:50
* 作者名称：
* 功能描述：
* 备注：用于查看资源 导入导出
******************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Lazy
{
    class MyAllPostprocessor : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (string str in importedAssets)
            {
                Debug.Log("导入: " + str);
            }
            foreach (string str in deletedAssets)
            {
                Debug.Log("删除: " + str);
            }
            for (int i = 0; i < movedAssets.Length; i++)
            {
                Debug.Log("移动: " + movedAssets[i] + " from: " + movedFromAssetPaths[i]);
            }
        }
    }
}

