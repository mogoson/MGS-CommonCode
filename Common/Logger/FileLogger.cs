﻿/*************************************************************************
 *  Copyright © 2018-2019 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  FileLogger.cs
 *  Description  :  Loggger for log to local file.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  9/19/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.IO;

namespace MGS.Common.Logger
{
    /// <summary>
    /// Loggger for log to local file.
    /// </summary>
    public sealed class FileLogger : ILogger
    {
        #region Field and Property
        /// <summary>
        /// Root directory of log files.
        /// </summary>
        public string RootDir { get; } = string.Format("{0}/Log/", Environment.CurrentDirectory);
        #endregion

        #region Private Method
        /// <summary>
        /// Logs a formatted message to local file.
        /// </summary>
        /// <param name="tag">Tag of log message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        private void LogToFile(string tag, string format, params object[] args)
        {
            var logFileName = DateTime.Now.ToString("MM-dd-yyyy");
            var logFilePath = string.Format("{0}/{1}.log", RootDir, logFileName);
            if (RequireDirectory(logFilePath))
            {
                try
                {
                    var logTimeStamp = DateTime.Now.ToLongTimeString();
                    var logContent = string.Format(format, args);
                    var formatLog = string.Format("{0}-{1}-{2}\r\n", logTimeStamp, tag, logContent);
                    File.AppendAllText(logFilePath, formatLog);
                }
#if DEBUG
                catch (Exception ex)
                {
                    throw ex;
                }
#else
                catch { }
#endif
            }
        }

        /// <summary>
        /// Require the directory of path exist.
        /// </summary>
        /// <param name="path">Directory or file path.</param>
        /// <returns>Succeed?</returns>
        private bool RequireDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            var dir = Path.GetDirectoryName(path);
            if (Directory.Exists(dir))
            {
                return true;
            }

            try
            {
                Directory.CreateDirectory(dir);
                return true;
            }
#if DEBUG
            catch (Exception ex)
            {
                throw ex;
            }
#else
            catch
            {
                return false;
            }
#endif
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// Default dir is Environment.CurrentDirectory/Log
        /// </summary>
        public FileLogger() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rootDir">Root directory of log files.</param>
        public FileLogger(string rootDir)
        {
            if (RequireDirectory(rootDir))
            {
                RootDir = rootDir;
            }
        }

        /// <summary>
        /// Logs a formatted message to local file.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        public void Log(string format, params object[] args)
        {
            LogToFile("Log", format, args);
        }

        /// <summary>
        /// Logs a formatted error message to local file.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        public void LogError(string format, params object[] args)
        {
            LogToFile("Error", format, args);
        }

        /// <summary>
        /// Logs a formatted warning message to local file.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        public void LogWarning(string format, params object[] args)
        {
            LogToFile("Warning", format, args);
        }
        #endregion
    }
}