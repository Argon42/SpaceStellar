using System;
using JetBrains.Annotations;

namespace SpaceStellar.Utility
{
    public interface ILogger
    {
        void Debug(string message);
        void Exception(Exception exception);
        [StringFormatMethod("message")]
        void Error(string message, params object[] args);
    }

    public interface ILogger<T> : ILogger { }
}