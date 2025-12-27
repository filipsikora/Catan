namespace Catan.Shared.Communication.Events
{
    public class DiceRolledEvent
    {
        public int RolledNumber { get; }
        public DiceRolledEvent(int rolledNumber)
        {
            RolledNumber = rolledNumber;
        }
    }
}