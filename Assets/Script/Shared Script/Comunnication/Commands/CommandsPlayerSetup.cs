using Catan.Shared.Interfaces;

namespace Catan.Shared.Communication.Commands
{
    public class PlayerCountSelectedCommand : ICommand
    {
        public int PlayerCount;
        public PlayerCountSelectedCommand(int playerCount)
        {
            PlayerCount = playerCount;
        }
    }
}