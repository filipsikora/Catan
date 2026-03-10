using Catan.Unity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Unity.Helpers
{
    public class EventBus
    {
        private readonly Dictionary<Type, List<Subscription>> _subscribers = new();

        private class Subscription
        {
            public object Target;
            public Delegate Callback;
            public Action<IInternalUIEvents> Wrapper;
        }

        public void Subscribe<T>(Action<T> callback) where T : IInternalUIEvents
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

        public void Unsubscribe<T>(Action<T> callback) where T : IInternalUIEvents
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

        public void Publish(IInternalUIEvents signal)
        {
            var signalType = signal.GetType();

            var subscribersSnapshot = _subscribers.ToArray();

            foreach (var (subscribedType, list) in subscribersSnapshot)
            {
                if (!subscribedType.IsAssignableFrom(signalType))
                    continue;

                foreach (var sub in list.ToArray())
                {
                    sub.Wrapper(signal);
                }
            }
        }
    }
}