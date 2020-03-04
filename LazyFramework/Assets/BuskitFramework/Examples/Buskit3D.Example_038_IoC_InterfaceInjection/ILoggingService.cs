/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：ILoggingService
* 创建日期：2019-03-31 14:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：日志工具
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using System.IO;

namespace Buskit3D.Training.IoC.B
{
    /// <summary>
    /// 日志工具
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// 向一个流中输出消息
        /// </summary>
        /// <param id="msg"></param>
        /// <param id="stream"></param>
        void Info(string msg, StreamWriter stream);

        /// <summary>
        /// 向一个流中输出错误
        /// </summary>
        /// <param id="msg"></param>
        /// <param id="stream"></param>
        void Error(string msg, StreamWriter stream);

        /// <summary>
        /// 向一个流中输出警告
        /// </summary>
        /// <param id="msg"></param>
        /// <param id="stream"></param>
        void Warring(string msg, StreamWriter stream);

        /// <summary>
        /// 向一个流中输出消息
        /// </summary>
        /// <param id="msg"></param>
        void Info(string msg);

        /// <summary>
        /// 向一个流中输出错误
        /// </summary>
        /// <param id="msg"></param>
        void Error(string msg);

        /// <summary>
        /// 向一个流中输出警告
        /// </summary>
        /// <param id="msg"></param>
        /// <param id="stream"></param>
        void Warring(string msg);
    }
}
