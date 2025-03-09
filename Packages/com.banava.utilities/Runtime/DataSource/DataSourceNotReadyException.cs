using System;

namespace Bananva.Utilities.DataSource
{
    public class DataSourceNotReadyException : Exception
    {
        public DataSourceNotReadyException(string message)
            : base(message) { }
    }
}