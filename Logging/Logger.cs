using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using System.IO;
using log4net.Layout;

namespace Logging
{
    public class Logger
    {
        ILog logger;

        static Logger()
        {
            // Gets directory path of the calling application
            // RelativeSearchPath is null if the executing assembly i.e. calling assembly is a
            // stand alone exe file (Console, WinForm, etc). 
            // RelativeSearchPath is not null if the calling assembly is a web hosted application i.e. a web site
            string log4NetConfigDirectory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;

            string log4NetConfigFilePath = Path.Combine(log4NetConfigDirectory, "log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(log4NetConfigFilePath));
        }
        
        public Logger(Type logClass)
        {
            logger = LogManager.GetLogger(logClass);
        }

        public void SetThreadProperty(string propName, string propValue)
        {
            log4net.ThreadContext.Properties[propName] = propValue;
            //var appenders = log4net.LogManager.GetRepository().GetAppenders();
            //foreach (var appender in appenders.OfType<log4net.Appender.RollingFileAppender>())
            //{
            //    var serializedLayout = appender.Layout as SerializedLayout;
            //    if (serializedLayout == null) continue;

            //    serializedLayout.AddMember(propName);
            //    appender.Layout = serializedLayout;
            //}
            //((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
        }

        public void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public void Fatal(string message, Exception ex)
        {
            logger.Fatal(message, ex);
        }

        public void Fatal(string message, params object[] args)
        {
            logger.FatalFormat(message, args);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(string message, Exception ex)
        {
            logger.Error(message, ex);
        }

        public void Error(string message, params object[] args)
        {
            logger.ErrorFormat(message, args);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }
        
        public void Warn(string message, Exception ex)
        {
            logger.Warn(message, ex);
        }

        public void Warn(string message, params object[] args)
        {
            logger.WarnFormat(message, args);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Info(string message, Exception ex)
        {
            logger.Info(message, ex);
        }

        public void Info(string message, params object[] args)
        {
            logger.InfoFormat(message, args);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Debug(string message, Exception ex)
        {
            logger.Debug(message, ex);
        }

        public void Debug(string message, params object[] args)
        {
            logger.DebugFormat(message, args);
        }

    }
};