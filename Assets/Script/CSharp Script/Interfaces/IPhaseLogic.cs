namespace Catan.Core.Interfaces
{
    public interface IPhaseLogic
    {
        void Enter();
        void Exit();
        void Handle(object command);
    }
}