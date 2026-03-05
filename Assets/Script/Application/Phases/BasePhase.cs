#nullable enable
using Catan.Application.Controllers;
using Catan.Application.Interfaces;

namespace Catan.Application.Phases
{
    public abstract class BasePhase
    {
        protected readonly Facade Facade;

        protected BasePhase(Facade facade)
        {
            Facade = facade;
        }

        public abstract GameResult Handle(object command);

        public virtual IUIMessages? Enter()
        {
            return null;
        }
    }
}