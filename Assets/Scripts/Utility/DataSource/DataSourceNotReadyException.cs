using System;

namespace SpaceStellar.Utility.DataSource
{
    public class DataSourceNotReadyException : Exception
    {
        public DataSourceNotReadyException(string message)
            : base(message)
        {
        }
    }
}