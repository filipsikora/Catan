using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan.Catan;
using Catan.Core;
using JetBrains.Annotations;

namespace Catan.Communication.Signals
{
    public class ResourceCardClickedSignal
    {
        public int VisualResourceCardId;
        public EnumResourceTypes Type;
        public EnumResourceCardLocation Location;
        public bool IsLeftClicked { get; }

        public ResourceCardClickedSignal(int visualResourceCardId, EnumResourceTypes type, EnumResourceCardLocation location, bool isLeftClicked)
        {
            VisualResourceCardId = visualResourceCardId;
            Type = type;
            Location = location;
            IsLeftClicked = isLeftClicked;
        }
    }

    public class ResourceCardVisualStateChangedSignal
    {
        public int VisualResourceCardId;
        public EnumResourceCardLocation Location;
        public EnumResourceCardVisualState State { get; }

        public ResourceCardVisualStateChangedSignal(int visualResourceCardId, EnumResourceCardLocation location, EnumResourceCardVisualState state)
        {
            VisualResourceCardId = visualResourceCardId;
            Location = location;
            State = state;
        }
    }

    public class MultipleResourceCardVisualStateChangedResetSignal
    {
        public EnumResourceCardLocation Location;
        public MultipleResourceCardVisualStateChangedResetSignal(EnumResourceCardLocation location)
        {
            Location = location;
        }
    }
}