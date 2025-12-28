using Catan.Shared.Communication;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;
using Catan.Unity.Panels;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Catan.Unity.Visuals.Controllers
{
    public class ControllerLogMessagesUI
    {
        private readonly EventBus _bus;
        private readonly Dictionary<EnumLogTypes, Action<LogMessageEvent>> _handlers;
        private LogsUI _panel;

        public ControllerLogMessagesUI(EventBus bus, LogsUI panel)
        {
            _bus = bus;
            _panel = panel;

            _bus.Subscribe<LogMessageEvent>(OnLogMessageReceived);
            _bus.Subscribe<ActionRejectedEvent>(OnActionRejectedReceived);

            _handlers = new Dictionary<EnumLogTypes, Action<LogMessageEvent>>
            {
                {EnumLogTypes.Info, e => _panel.AddInfo(e.Message, e.Time) },
                {EnumLogTypes.Error, e => _panel.AddError(e.Message) },
                {EnumLogTypes.Warning, e => _panel.AddWarning(e.Message) }
            };
        }

        private void OnLogMessageReceived(LogMessageEvent signal)
        {
            if (_handlers.TryGetValue(signal.Type, out var handler))
            {
                handler(signal);
            }
        }

        private void OnActionRejectedReceived(ActionRejectedEvent signal)
        {
            _panel.AddError(signal.Reason.ToString());
        }

        public void Dispose()
        {
            _bus.Unsubscribe<LogMessageEvent>(OnLogMessageReceived);
            _bus.Unsubscribe<ActionRejectedEvent>(OnActionRejectedReceived);
        }
    }
}