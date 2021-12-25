using SMS.TenantApi.CrossCuttingLayer.Logging.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SMS.TenantApi.CrossCuttingLayer.Logging.Interfaces
{
    public interface IServiceLogger
    {
        void Error([Localizable(false)] string message, [Localizable(false)] string argument);
        void Error(Exception exception, [Localizable(false)] string message, params object[] args);
        void Error([Localizable(false)] string message);
        void Info([Localizable(false)] string message);
        void Log(LogInformation logInformation, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);
    }
}
