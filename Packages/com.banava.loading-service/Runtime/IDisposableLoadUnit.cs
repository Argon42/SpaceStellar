using System;

namespace Bananva.LoadingService
{
    public interface IDisposableLoadUnit : ILoadUnit, IDisposable { }
    
    public interface IDisposableLoadUnit<in T> : ILoadUnit<T>, IDisposable { }
}