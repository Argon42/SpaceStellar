using System;

namespace SpaceStellar.Utility
{
    public interface ILogger
    {
        void Debug(string message);
        void Exception(Exception exception);
        void Error(string message, params object[] args);
    }

    public interface ILogger<T> : ILogger { }
}