using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Web.Core.Log {
    public class LogHelper {
        //选择<logger name="logInfo">的配置
        public static readonly log4net.ILog logInfo = log4net.LogManager.GetLogger("logInfo");

        //选择<logger name="logError">的配置
        public static readonly log4net.ILog logError = log4net.LogManager.GetLogger("logError");


        public static void SetConfig() {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// Write system info.
        /// </summary>
        /// <param name="info"></param>
        public static void WriteLog(string info) {
            if (logInfo.IsInfoEnabled) {
                logInfo.Info(info);
            }
        }
        /// <summary>
        /// Write system errors.
        /// </summary>
        /// <param name="errMsg">Error messages.</param>
        /// <param name="ex">Exceptions.</param>
        public static void WriteLog(string errMsg, Exception ex) {
            if (logError.IsErrorEnabled) {
                logError.Error("异常信息: " + errMsg, ex);
            }
        }
    }
}