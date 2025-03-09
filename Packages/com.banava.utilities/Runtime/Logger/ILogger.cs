using System;
using JetBrains.Annotations;

namespace Bananva.Utilities.Logger
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