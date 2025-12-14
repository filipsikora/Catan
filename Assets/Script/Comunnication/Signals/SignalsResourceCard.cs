using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan.Catan;
using Catan.Core;

namespace Catan.Communication.Signals
{
    public class ResourceCardClickedSignal
    {
        public VisualResourceCard Card;
        public bool IsLeftClick { get; }

        public ResourceCardClickedSignal(VisualResourceCard card, bool isLeftClick)
        {
            Card = card;
            IsLeftClick = isLeftClick;
        }
    }

    public class ResourceCardSelectionChangedSignal
    {
        public VisualResourceCard Card { get; }
        public bool IsSelected { get; }

        public ResourceCardSelectionChangedSignal(VisualResourceCard card, bool isSelected)
        {
            Card = card;
            IsSelected = isSelected;
        }
    }
}