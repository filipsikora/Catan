using System;
using System.Collections.Generic;

namespace Catan.Communication
{
    public class EventBus
    {
        private readonly Dictionary<Type, List<Subscription>> _subscribers = new();

        private class Subscription
        {
            public object Target;
            public Delegate Callback;
            public Action<object> Wrapper;
        }

        public void Subscribe<T>(Action<T> callback)
        {
            var type = typeof(T);

            if (!_subscribers.TryGetValue(type, out var list))
            {
                list = new List<Subscription>();
                _subscribers[type] = list;
            }

            var wrapper = new Action<object>(obj => callback((T)obj));

            list.Add(new Subscription
            {
                Target = callback.Target,
                Callback = callback,
                Wrapper = wrapper
            });
        }

        public void Unsubscribe<T>(Action<T> callback)
        {
            var type = typeof(T);

            if (!_subscribers.TryGetValue(type, out var list))
                return;

            list.RemoveAll(sub =>
                sub.Callback == (Delegate)callback &&
                sub.Target == callback.Target
            );

            if (list.Count == 0)
                _subscribers.Remove(type);
        }

        public void Publish<T>(T signal)
        {
            var type = typeof(T);

            if (!_subscribers.TryGetValue(type, out var list))
                return;

            foreach (var sub in list.ToArray())
            {
                sub.Wrapper(signal);
            }
        }
    }
}