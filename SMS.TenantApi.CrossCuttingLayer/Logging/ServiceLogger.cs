using NLog;
using SMS.TenantApi.BusinessModel.Config;
using SMS.TenantApi.CrossCuttingLayer.Logging.Interfaces;
using SMS.TenantApi.CrossCuttingLayer.Logging.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SMS.TenantApi.CrossCuttingLayer.Logging
{
    public class ServiceLogger : IServiceLogger
    {
        private readonly AppSettingsModel _appsettings;
        private readonly Logger _logger;
        public ServiceLogger(AppSettingsModel appsettings)
        {
            _appsettings = appsettings;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Error(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            _logger.Error(exception, message, args);
        }

        public void Error([Localizable(false)] string message)
        {
            _logger.Error(message);
        }

        public void Error([Localizable(false)] string message, [Localizable(false)] string argument)
        {
            _logger.Error(message, argument);
        }

        public void Info([Localizable(false)] string message)
        {
            _logger.Info(message);
        }
        /// <summary>
        /// Log the error or info for the method executions 
        /// </summary>
        /// <param name="logInformation"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        /// <returns></returns>
        public void Log(LogInformation logInformation, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (_appsettings.Settings.IsLoggingEnabled)
            {
                SetNLogContext(logInformation, memberName, sourceFilePath, sourceLineNumber);
                if (logInformation.Exception != null)
                    _logger.Error(logInformation.Exception, logInformation.Message);
                else
                    _logger.Info(logInformation.Message);
                //Now clear the context
                GlobalDiagnosticsContext.Clear();
            }
        }

        private void SetNLogContext(LogInformation logInformation, string memberName, string sourceFilePath,
            int sourceLineNumber)
        {
            #region Set the values in the NLog context object

            GlobalDiagnosticsContext.Set("userGuid", logInformation.UserGuid);
            GlobalDiagnosticsContext.Set("module", logInformation.Module);
            GlobalDiagnosticsContext.Set("logData", logInformation.Data);
            GlobalDiagnosticsContext.Set("method", memberName);
            GlobalDiagnosticsContext.Set("fileName", sourceFilePath);
            GlobalDiagnosticsContext.Set("lineNumber", sourceLineNumber.ToString());

            #endregion
        }
    }
}
