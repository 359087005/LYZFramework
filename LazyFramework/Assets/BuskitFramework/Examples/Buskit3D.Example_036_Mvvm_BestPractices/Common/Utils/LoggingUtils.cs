/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：EntityUtils
* 创建日期：2019-03-31 14:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：日志工具
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using System.IO;
using UnityEngine;

namespace Buskit3D.Example_036_Mvvm_BestPractices
{ 
    /// <summary>
    /// 日志工具
    /// </summary>
    public class LoggingUtils
    {
        /// <summary>
        /// 向一个流中输出消息
        /// </summary>
        /// <param id="msg"></param>
        /// <param id="stream"></param>
        public void Info(string msg,StreamWriter stream)
        {
            stream.WriteLine("Info :[" + msg + "]");
        }

        /// <summary>
        /// 向一个流中输出错误
        /// </summary>
        /// <param id="msg"></param>
        /// <param id="stream"></param>
        public void Error(string msg, StreamWriter stream)
        {
            stream.WriteLine("Error:[" + msg + "]");
        }

        /// <summary>
        /// 向一个流中输出警告
        /// </summary>
        /// <param id="msg"></param>
        /// <param id="stream"></param>
        public void Warring(string msg, StreamWriter stream)
        {
            stream.WriteLine("Warr :[" + msg + "]");
        }

        /// <summary>
        /// 向一个流中输出消息
        /// </summary>
        /// <param id="msg"></param>
        public void Info(string msg)
        {
            Debug.Log("Info :[" + msg + "]");
        }

        /// <summary>
        /// 向一个流中输出错误
        /// </summary>
        /// <param id="msg"></param>
        public void Error(string msg)
        {
            Debug.LogError("Error:[" + msg + "]");
        }

        /// <summary>
        /// 向一个流中输出警告
        /// </summary>
        /// <param id="msg"></param>
        /// <param id="stream"></param>
        public void Warring(string msg)
        {
            Debug.LogWarning("Warr :[" + msg + "]");
        }
    }
}
