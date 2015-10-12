using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class Board
    {
        public const int NUMBER_OF_SPACES = 40;

        public List<BoardSpace> Spaces { get; private set; } = new List<BoardSpace>(NUMBER_OF_SPACES);

        public Board()
        {
            CreateBoardSpaces();
        }

        private void CreateBoardSpaces()
        {
            CreateRailRoadSpaces();
            CreateActionSpaces();
            CreateCardSpaces();
            CreateUtilitySpaces();
            CreatePropertySpaces();
            CreatePenaltySpaces();

            Spaces = Spaces.OrderBy(o => o.Position).ToList();
        }

        private void CreateRailRoadSpaces()
        {
            Spaces.Add(new RailRoadSpace("Reading Railroad", 5));
            Spaces.Add(new RailRoadSpace("Pennsylvania Railroad", 15));
            Spaces.Add(new RailRoadSpace("B. & O. Railroad", 25));
            Spaces.Add(new RailRoadSpace("Short Line", 35));
        }

        private void CreateActionSpaces()
        {
            Spaces.Add(new ActionSpace("Go", 0));
            Spaces.Add(new ActionSpace("Jail", 10));
            Spaces.Add(new ActionSpace("Free Parking", 20));
            Spaces.Add(new ActionSpace("Go To Jail", 30));
        }

        private void CreateCardSpaces()
        {
            Spaces.Add(new CardSpace("Community Chest", 2));
            Spaces.Add(new CardSpace("Chance", 7));
            Spaces.Add(new CardSpace("Community Chest", 17));
            Spaces.Add(new CardSpace("Chance", 22));
            Spaces.Add(new CardSpace("Community Chest", 33));
            Spaces.Add(new CardSpace("Chance", 36));
        }

        private void CreateUtilitySpaces()
        {
            Spaces.Add(new UtilitySpace("Electric Company", 12));
            Spaces.Add(new UtilitySpace("Water Works", 28));
        }

        private void CreatePropertySpaces()
        {
            Spaces.Add(new PropertySpace("Mediterranean Avenue", 1, 60));
            Spaces.Add(new PropertySpace("Baltic Avenue", 3, 60));
            Spaces.Add(new PropertySpace("Oriental Avenue", 6, 100));
            Spaces.Add(new PropertySpace("Vermont Avenue", 8, 100));
            Spaces.Add(new PropertySpace("Connecticut Avenue", 9, 120));
            Spaces.Add(new PropertySpace("St. Charles Place", 11, 140));
            Spaces.Add(new PropertySpace("States Avenue", 13, 140));
            Spaces.Add(new PropertySpace("Virginia Avenue", 14, 160));
            Spaces.Add(new PropertySpace("St. James Place", 16, 180));
            Spaces.Add(new PropertySpace("Tennessee Avenue", 18, 180));
            Spaces.Add(new PropertySpace("New York Avenue", 19, 200));
            Spaces.Add(new PropertySpace("Kentucky Avenue", 21, 220));
            Spaces.Add(new PropertySpace("Indiana Avenue", 23, 220));
            Spaces.Add(new PropertySpace("Illinois Avenue", 24, 240));
            Spaces.Add(new PropertySpace("Atlantic Avenue", 26, 260));
            Spaces.Add(new PropertySpace("Ventnor Avenue", 27, 260));
            Spaces.Add(new PropertySpace("Marvin Gardens", 29, 280));
            Spaces.Add(new PropertySpace("Pacific Avenue", 31, 300));
            Spaces.Add(new PropertySpace("North Carolina Avenue", 32, 300));
            Spaces.Add(new PropertySpace("Pennsylvania Avenue", 34, 320));
            Spaces.Add(new PropertySpace("Park Place", 37, 350));
            Spaces.Add(new PropertySpace("Boardwalk", 39, 400));
        }

        private void CreatePenaltySpaces()
        {
            Spaces.Add(new PenaltySpace("Income Tax", 4, 200));
            Spaces.Add(new PenaltySpace("Luxury Tax", 38, 100));
        }
    }
}
