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
    }
}