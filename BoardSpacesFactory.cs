using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class BoardSpacesFactory : IBoardSpacesFactory
    {
        private List<BoardSpace> spaces = new List<BoardSpace>();

        public IEnumerable<IBoardSpace> CreateAll()
        {
            CreateBoardSpaces();

            return spaces;
        }

        private void CreateBoardSpaces()
        {
            CreateRailRoadSpaces();
            CreateActionSpaces();
            CreateCardSpaces();
            CreateUtilitySpaces();
            CreatePropertySpaces();
            CreatePenaltySpaces();

            spaces = spaces.OrderBy(o => o.Position).ToList();
        }

        private void CreateRailRoadSpaces()
        {
            spaces.Add(new RailRoadSpace("Reading Railroad", 5));
            spaces.Add(new RailRoadSpace("Pennsylvania Railroad", 15));
            spaces.Add(new RailRoadSpace("B. & O. Railroad", 25));
            spaces.Add(new RailRoadSpace("Short Line", 35));
        }

        private void CreateActionSpaces()
        {
            spaces.Add(new ActionSpace("Go", 0));
            spaces.Add(new ActionSpace("Jail", 10));
            spaces.Add(new ActionSpace("Free Parking", 20));
            spaces.Add(new ActionSpace("Go To Jail", 30));
        }

        private void CreateCardSpaces()
        {
            spaces.Add(new CardSpace("Community Chest", 2));
            spaces.Add(new CardSpace("Chance", 7));
            spaces.Add(new CardSpace("Community Chest", 17));
            spaces.Add(new CardSpace("Chance", 22));
            spaces.Add(new CardSpace("Community Chest", 33));
            spaces.Add(new CardSpace("Chance", 36));
        }

        private void CreateUtilitySpaces()
        {
            spaces.Add(new UtilitySpace("Electric Company", 12));
            spaces.Add(new UtilitySpace("Water Works", 28));
        }

        private void CreatePropertySpaces()
        {
            spaces.Add(new PropertySpace("Mediterranean Avenue", 1, 60));
            spaces.Add(new PropertySpace("Baltic Avenue", 3, 60));
            spaces.Add(new PropertySpace("Oriental Avenue", 6, 100));
            spaces.Add(new PropertySpace("Vermont Avenue", 8, 100));
            spaces.Add(new PropertySpace("Connecticut Avenue", 9, 120));
            spaces.Add(new PropertySpace("St. Charles Place", 11, 140));
            spaces.Add(new PropertySpace("States Avenue", 13, 140));
            spaces.Add(new PropertySpace("Virginia Avenue", 14, 160));
            spaces.Add(new PropertySpace("St. James Place", 16, 180));
            spaces.Add(new PropertySpace("Tennessee Avenue", 18, 180));
            spaces.Add(new PropertySpace("New York Avenue", 19, 200));
            spaces.Add(new PropertySpace("Kentucky Avenue", 21, 220));
            spaces.Add(new PropertySpace("Indiana Avenue", 23, 220));
            spaces.Add(new PropertySpace("Illinois Avenue", 24, 240));
            spaces.Add(new PropertySpace("Atlantic Avenue", 26, 260));
            spaces.Add(new PropertySpace("Ventnor Avenue", 27, 260));
            spaces.Add(new PropertySpace("Marvin Gardens", 29, 280));
            spaces.Add(new PropertySpace("Pacific Avenue", 31, 300));
            spaces.Add(new PropertySpace("North Carolina Avenue", 32, 300));
            spaces.Add(new PropertySpace("Pennsylvania Avenue", 34, 320));
            spaces.Add(new PropertySpace("Park Place", 37, 350));
            spaces.Add(new PropertySpace("Boardwalk", 39, 400));
        }

        private void CreatePenaltySpaces()
        {
            spaces.Add(new PenaltySpace("Income Tax", 4, 200));
            spaces.Add(new PenaltySpace("Luxury Tax", 38, 100));
        }
    }
}
