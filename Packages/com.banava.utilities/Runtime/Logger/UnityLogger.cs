using System;
using JetBrains.Annotations;

namespace Bananva.Utilities.Logger
{
    public class UnityLogger : ILogger
    {
        public void Debug(string message)
        {
            UnityEngine.Debug.Log(message);
        }

        public void Exception(Exception exception)
        {
            UnityEngine.Debug.LogException(exception);
        }

        [StringFormatMethod("message")]
        public void Error(string message, params object[] args)
        {
            UnityEngine.Debug.LogErrorFormat(message, args);
        }
    }

    public class UnityLogger<T> : ILogger<T>
    {
        public void Debug(string message)
        {
            UnityEngine.Debug.Log($"[{typeof(T).Name}] {message}");
        }

        public void Exception(Exception exception)
        {
            Error("Caused an exception: {0}\n{1}", exception.Message, exception);
        }

        [StringFormatMethod("message")]
        public void Error(string message, params object[] args)
        {
            UnityEngine.Debug.LogErrorFormat($"[{typeof(T).Name}] {message}", args);
        }
    }
}