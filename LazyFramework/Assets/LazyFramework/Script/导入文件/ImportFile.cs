/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：ImportFile
* 创建日期：2020-1-2
* 作者名称：林奕州
* CLR 版本：4.0.30319.42000
* 功能描述：打开路径选择器
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Lazy
{

    public partial class ImportFile
    {
        public void OpenLocalVideo(out string filePath, Action onSelect)
        {
            filePath = "";
            OpenFileName openFileName = new OpenFileName();
            openFileName.structSize = Marshal.SizeOf(openFileName);
            openFileName.filter = " Mp4\0*.mp4\0";
            openFileName.file = new string(new char[256]);
            openFileName.maxFile = openFileName.file.Length;
            openFileName.fileTitle = new string(new char[64]);
            openFileName.maxFileTitle = openFileName.fileTitle.Length;
            openFileName.initialDir = Application.streamingAssetsPath.Replace('/', '\\');//默认路径
            openFileName.title = "选择导入视频";
            openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;
            if (LocalDialog.GetOpenFileName(openFileName))
            {
                filePath = openFileName.file;
                Debug.Log("成功获取路径：" + openFileName.file);
                onSelect.Invoke();
            }
        }
        public void OpenLocalImage(out string filePath, Action onSelect)
        {
            filePath = "";
            OpenFileName openFileName = new OpenFileName();
            openFileName.structSize = Marshal.SizeOf(openFileName);
            openFileName.filter = "图片文件(*.png;*.jpg;*.bmp;*.jpeg)\0*.png;*.jpg;*.bmp;*.jpeg\0\0\0\0";
            openFileName.file = new string(new char[256]);
            openFileName.maxFile = openFileName.file.Length;
            openFileName.fileTitle = new string(new char[64]);
            openFileName.maxFileTitle = openFileName.fileTitle.Length;
            openFileName.initialDir = Application.streamingAssetsPath.Replace('/', '\\');//默认路径
            openFileName.title = "选择导入图片";
            openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;
            if (LocalDialog.GetOpenFileName(openFileName))
            {
                filePath = openFileName.file;
                Debug.Log("成功获取路径：" + openFileName.file);
                onSelect.Invoke();
            }
        }
    }
}