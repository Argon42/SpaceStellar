using System;

namespace SpaceStellar.Utility
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
            UnityEngine.Debug.LogException(exception);
        }

        public void Error(string message, params object[] args)
        {
            UnityEngine.Debug.LogErrorFormat($"[{typeof(T).Name}] {message}", args);
        }
    }
}