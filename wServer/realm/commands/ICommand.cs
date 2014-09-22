using wServer.realm.entities;

namespace wServer.realm.commands
{
    interface ICommand
    {
        string Command { get; }
        int RequiredRank { get; }

        void Execute(Player player, string[] args);
    }
}
