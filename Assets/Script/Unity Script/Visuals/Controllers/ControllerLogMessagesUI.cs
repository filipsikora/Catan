using Catan.Unity.InternalUIEvents;
using Catan.Unity.Helpers;
using Catan.Shared.Data;
using Catan.Unity.Panels;
using System;
using System.Collections.Generic;

namespace Catan.Unity.Visuals.Controllers
{
    public class ControllerLogMessagesUI
    {
        private readonly EventBus _bus;
        private readonly Dictionary<EnumLogTypes, Action<LogMessageUIEvent>> _handlers;
        private LogsUI _panel;

        public ControllerLogMessagesUI(EventBus bus, LogsUI panel)
        {
            _bus = bus;
            _panel = panel;

            _bus.Subscribe<LogMessageUIEvent>(OnLogMessageReceived);
            _bus.Subscribe<ActionRejectedUIEvent>(OnActionRejectedReceived);

            _handlers = new Dictionary<EnumLogTypes, Action<LogMessageUIEvent>>
            {
                {EnumLogTypes.Info, e => _panel.AddInfo(e.Message, e.Time) },
                {EnumLogTypes.Error, e => _panel.AddError(e.Message) },
                {EnumLogTypes.Warning, e => _panel.AddWarning(e.Message) }
            };
        }

        private void OnLogMessageReceived(LogMessageUIEvent signal)
        {
            if (_handlers.TryGetValue(signal.Type, out var handler))
            {
                handler(signal);
            }
        }

        private void OnActionRejectedReceived(ActionRejectedUIEvent signal)
        {
            _panel.AddError(signal.Reason.ToString());
        }

        public void Dispose()
        {
            _bus.Unsubscribe<LogMessageUIEvent>(OnLogMessageReceived);
            _bus.Unsubscribe<ActionRejectedUIEvent>(OnActionRejectedReceived);
        }
    }
}