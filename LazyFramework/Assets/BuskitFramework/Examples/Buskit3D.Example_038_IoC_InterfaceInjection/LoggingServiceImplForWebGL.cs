/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：LoggingServiceImpl
* 创建日期：2019-03-31 14:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：日志工具
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using Com.Rainier.Buskit.Unity.Architecture.Injector;
using Com.Rainier.Buskit3D;
using System.IO;

namespace Buskit3D.Training.IoC.B
{ 
    /// <summary>
    /// 日志工具
    /// </summary>
    public class LoggingServiceImplForWebGL : ILoggingService
    {
        [Inject]
        IWebGlWebAPIService webApi;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LoggingServiceImplForWebGL()
        {
            InjectService.InjectInto(this);
        }
        /// <summary>
        /// 向一个流中输出消息
        /// </summary>
        /// <param id="msg"></param>
        /// <param id="stream"></param>
        public void Info(string msg,StreamWriter stream)
        {
            webApi.PrintInfo("Info :[" + msg + "]");
        }

        /// <summary>
        /// 向一个流中输出错误
        /// </summary>
        /// <param id="msg"></param>
        /// <param id="stream"></param>
        public void Error(string msg, StreamWriter stream)
        {
            webApi.PrintError("Error :[" + msg + "]");
        }

        /// <summary>
        /// 向一个流中输出警告
        /// </summary>
        /// <param id="msg"></param>
        /// <param id="stream"></param>
        public void Warring(string msg, StreamWriter stream)
        {
            webApi.PrintError("Warr :[" + msg + "]");
        }

        /// <summary>
        /// 向一个流中输出消息
        /// </summary>
        /// <param id="msg"></param>
        public void Info(string msg)
        {
            webApi.PrintInfo("Info :[" + msg + "]");
        }

        /// <summary>
        /// 向一个流中输出错误
        /// </summary>
        /// <param id="msg"></param>
        public void Error(string msg)
        {
            webApi.PrintInfo("Error:[" + msg + "]");
        }

        /// <summary>
        /// 向一个流中输出警告
        /// </summary>
        /// <param id="msg"></param>
        /// <param id="stream"></param>
        public void Warring(string msg)
        {
            webApi.PrintInfo("Warr:[" + msg + "]");
        }
    }
}
