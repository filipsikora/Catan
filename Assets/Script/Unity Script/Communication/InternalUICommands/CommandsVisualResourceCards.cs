using Catan.Shared.Data;
using Catan.Shared.Interfaces;
using Catan.Unity.Data;

namespace Catan.Unity.Communication.InternalUICommands
{
    public class ResourceCardVisualStateChangedUICommand : ICommand
    {
        public int VisualResourceCardId;
        public EnumResourceCardLocation Location;
        public EnumResourceCardVisualState State { get; }

        public ResourceCardVisualStateChangedUICommand(int visualResourceCardId, EnumResourceCardLocation location, EnumResourceCardVisualState state)
        {
            VisualResourceCardId = visualResourceCardId;
            Location = location;
            State = state;
        }
    }

    public class ResourceCardTypeVisualStateChangedUICommand : ICommand
    {
        public EnumResourceTypes? Type { get; }
        public EnumResourceCardVisualState State { get; }

        public ResourceCardTypeVisualStateChangedUICommand(
            EnumResourceTypes? type,
            EnumResourceCardVisualState state)
        {
            Type = type;
            State = state;
        }
    }

    public class MultipleResourceCardVisualStateResetUICommand : ICommand
    {
        public EnumResourceCardLocation Location;

        public MultipleResourceCardVisualStateResetUICommand(EnumResourceCardLocation location)
        {
            Location = location;
        }
    }

    public class ResourceCardToggledUICommand : ICommand
    {
        public int VisualResourceCardId { get; }
        
        public ResourceCardToggledUICommand(int visualResourceCardId)
        {
            VisualResourceCardId = visualResourceCardId;
        }
    }
}