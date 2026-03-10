namespace Catan.Core.PhaseLogic
{
    public abstract class BaseLogic
    {
        protected readonly GameSession Session;

        protected BaseLogic(GameSession session)
        {
            Session = session;
        }
    }
}
