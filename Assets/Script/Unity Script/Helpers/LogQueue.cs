using Catan.Unity.InternalUIEvents;
using System.Collections.Generic;

namespace Catan.Unity.Helpers
{
    public class LogQueue
    {
        private Queue<LogMessageUIEvent> _logMessages { get; } = new();
        private bool _isDisplaying;
        private EventBus _bus;

        public LogQueue(EventBus bus)
        {
            _bus = bus;
        }

        public void AddLogToQueue(LogMessageUIEvent logMessage)
        {
            _logMessages.Enqueue(logMessage);

            if (!_isDisplaying)
            {
                DisplayNextLog();
            }
        }

        public void DisplayNextLog()
        {
            if (_logMessages.Count == 0)
            {
                _isDisplaying = false;
                return;
            }

            _isDisplaying = true;

            var nextLog = _logMessages.Dequeue();
            _bus.Publish(nextLog);
        }
    }
}
