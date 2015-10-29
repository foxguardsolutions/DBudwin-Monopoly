using Monopoly.Game.MonopolyBoard;

namespace Monopoly.Game.Properties
{
    public class ActionSpace : BoardSpace
    {
        public ActionSpace(string name, SpaceKeys position)
        {
            Name = name;
            Position = position;
        }
    }
}
