using Catan.Unity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Unity.Helpers
{
    public class EventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers = new();

        public void Subscribe<T>(Action<T> callback) where T : IInternalUIEvents
        {
            var type = typeof(T);

            if (!_subscribers.TryGetValue(type, out var list))
            {
                list = new List<Delegate>();
                _subscribers[type] = list;
            }

            list.Add(callback);
        }

        public void Unsubscribe<T>(Action<T> callback) where T : IInternalUIEvents
        {
            var type = typeof(T);

            if (!_subscribers.TryGetValue(type, out var list))
                return;

            list.Remove(callback);

            if (list.Count == 0)
                _subscribers.Remove(type);
        }

        public void Publish(IInternalUIEvents signal)
        {
            var signalType = signal.GetType();

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