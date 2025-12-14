using System;
using System.Collections.Generic;

namespace Catan.Communication
{
    public class EventBus
    {
        private readonly Dictionary<Type, List<Action<object>>> _subscribers =
            new Dictionary<Type, List<Action<object>>>();

        public void Subscribe<T>(Action<T> callback)
        {
            var type = typeof(T);

            if (!_subscribers.ContainsKey(type))
                _subscribers[type] = new List<Action<object>>();

            _subscribers[type].Add(obj => callback((T)obj));
        }

        public void Unsubscribe<T>(Action<T> callback)
        {
            var type = typeof(T);


            if (!_subscribers.TryGetValue(type, out var list))
                return;

            list.RemoveAll(wrapper =>
            {
                var method = wrapper.Method;

                return method == callback.Method;
            });
        }

        public void Publish<T>(T signal)
        {
            var type = typeof(T);

            if (!_subscribers.TryGetValue(type, out var callbacks))
                return;

            foreach (var cb in callbacks.ToArray())
            {
                cb(signal);
            }
        }
    }
}