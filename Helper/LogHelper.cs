using System;
namespace HighSpotJson.Helper
{
    public static class LogHelper
    {
        /// <summary>
        /// Log information to file/DB or call service asynchronous
        /// logging to be change with settings verbos or minimal etc.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logtype"></param>
        public static void LogInformation(string message, LogType logtype = LogType.Information)
        {
            //to do add proper logging for information based on log type and tracing level
            Console.WriteLine(string.Format("information Logged:{0}", message));

        }

        /// <summary>
        /// Log error to file/DB or call service asynchronous
        /// </summary>
        /// <param name="ex"></param>
        public static void LogError(Exception ex)
        {
            //to do add proper logging
            Console.WriteLine("Error occure");
            Console.WriteLine(ex.Message);

        }

    }

    public enum LogType
    {
        Error,
        Information,
        Debug,
        Warnings
    }
}