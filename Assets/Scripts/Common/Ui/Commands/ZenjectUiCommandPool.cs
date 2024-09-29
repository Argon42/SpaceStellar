using System;
using System.Collections.Generic;
using Zenject;

namespace SpaceStellar.Common.Ui.Commands
{
    public class ZenjectUiCommandPool : IUiCommandPool
    {
        private readonly IInstantiator _instantiator;
        private readonly Dictionary<Type, Stack<IUiCommand>> _commandPool = new();

        public ZenjectUiCommandPool(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public void Release(IUiCommand command)
        {
            command.Clear();
            Type? type = command.GetType();

            if (!_commandPool.ContainsKey(type))
                _commandPool[type] = new Stack<IUiCommand>();

            _commandPool[type].Push(command);
        }

        public T Create<T>() where T : IUiCommand
        {
            if (!_commandPool.ContainsKey(typeof(T)))
                _commandPool[typeof(T)] = new Stack<IUiCommand>();

            if (_commandPool[typeof(T)].Count > 0)
                return (T)_commandPool[typeof(T)].Pop();

            return _instantiator.Instantiate<T>();
        }
    }
}
