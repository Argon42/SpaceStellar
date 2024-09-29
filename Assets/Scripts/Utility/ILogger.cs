using System;

namespace SpaceStellar.Utility
{
    public interface ILogger
    {
        void Debug(string message);
        void Exception(Exception exception);
    }
}