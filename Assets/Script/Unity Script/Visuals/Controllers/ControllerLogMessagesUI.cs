using Catan.Unity.Communication.InternalUIEvents;
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
        private readonly Dictionary<EnumLogTypes, Action<LogMessageMessage>> _handlers;
        private LogsUI _panel;

        public ControllerLogMessagesUI(EventBus bus, LogsUI panel)
        {
            _bus = bus;
            _panel = panel;

            _bus.Subscribe<LogMessageMessage>(OnLogMessageReceived);
            _bus.Subscribe<ActionRejectedMessage>(OnActionRejectedReceived);

            _handlers = new Dictionary<EnumLogTypes, Action<LogMessageMessage>>
            {
                {EnumLogTypes.Info, e => _panel.AddInfo(e.Message, e.Time) },
                {EnumLogTypes.Error, e => _panel.AddError(e.Message) },
                {EnumLogTypes.Warning, e => _panel.AddWarning(e.Message) }
            };
        }

        private void OnLogMessageReceived(LogMessageMessage signal)
        {
            if (_handlers.TryGetValue(signal.Type, out var handler))
            {
                handler(signal);
            }
        }

        private void OnActionRejectedReceived(ActionRejectedMessage signal)
        {
            _panel.AddError(signal.Reason.ToString());
        }

        public void Dispose()
        {
            _bus.Unsubscribe<LogMessageMessage>(OnLogMessageReceived);
            _bus.Unsubscribe<ActionRejectedMessage>(OnActionRejectedReceived);
        }
    }
}