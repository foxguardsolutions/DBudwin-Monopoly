using System.Collections.Generic;
using System.Linq;
using Monopoly.Game.Properties;

namespace Monopoly.Game
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
            spaces.Add(new RailRoadSpace("Reading Railroad", BoardSpace.SpaceKeys.ReadingRR));
            spaces.Add(new RailRoadSpace("Pennsylvania Railroad", BoardSpace.SpaceKeys.PennsylvaniaRR));
            spaces.Add(new RailRoadSpace("B. & O. Railroad", BoardSpace.SpaceKeys.BORR));
            spaces.Add(new RailRoadSpace("Short Line", BoardSpace.SpaceKeys.ShortLine));
        }

        private void CreateActionSpaces()
        {
            spaces.Add(new ActionSpace("Go", BoardSpace.SpaceKeys.Go));
            spaces.Add(new ActionSpace("Jail", BoardSpace.SpaceKeys.Jail));
            spaces.Add(new ActionSpace("Free Parking", BoardSpace.SpaceKeys.FreeParking));
            spaces.Add(new ActionSpace("Go To Jail", BoardSpace.SpaceKeys.GoToJail));
        }

        private void CreateCardSpaces()
        {
            spaces.Add(new CardSpace("Community Chest", BoardSpace.SpaceKeys.CC1));
            spaces.Add(new CardSpace("Chance", BoardSpace.SpaceKeys.Chance1));
            spaces.Add(new CardSpace("Community Chest", BoardSpace.SpaceKeys.CC2));
            spaces.Add(new CardSpace("Chance", BoardSpace.SpaceKeys.Chance2));
            spaces.Add(new CardSpace("Community Chest", BoardSpace.SpaceKeys.CC3));
            spaces.Add(new CardSpace("Chance", BoardSpace.SpaceKeys.Chance3));
        }

        private void CreateUtilitySpaces()
        {
            spaces.Add(new UtilitySpace("Electric Company", BoardSpace.SpaceKeys.ElectricCompany));
            spaces.Add(new UtilitySpace("Water Works", BoardSpace.SpaceKeys.WaterWorks));
        }

        private void CreatePropertySpaces()
        {
            spaces.Add(new PropertySpace("Mediterranean Avenue", BoardSpace.SpaceKeys.Mediterranean, 60));
            spaces.Add(new PropertySpace("Baltic Avenue", BoardSpace.SpaceKeys.Baltic, 60));
            spaces.Add(new PropertySpace("Oriental Avenue", BoardSpace.SpaceKeys.Oriental, 100));
            spaces.Add(new PropertySpace("Vermont Avenue", BoardSpace.SpaceKeys.Vermont, 100));
            spaces.Add(new PropertySpace("Connecticut Avenue", BoardSpace.SpaceKeys.Connecticut, 120));
            spaces.Add(new PropertySpace("St. Charles Place", BoardSpace.SpaceKeys.StCharles, 140));
            spaces.Add(new PropertySpace("States Avenue", BoardSpace.SpaceKeys.States, 140));
            spaces.Add(new PropertySpace("Virginia Avenue", BoardSpace.SpaceKeys.Virginia, 160));
            spaces.Add(new PropertySpace("St. James Place", BoardSpace.SpaceKeys.StJames, 180));
            spaces.Add(new PropertySpace("Tennessee Avenue", BoardSpace.SpaceKeys.Tennessee, 180));
            spaces.Add(new PropertySpace("New York Avenue", BoardSpace.SpaceKeys.NewYork, 200));
            spaces.Add(new PropertySpace("Kentucky Avenue", BoardSpace.SpaceKeys.Kentucky, 220));
            spaces.Add(new PropertySpace("Indiana Avenue", BoardSpace.SpaceKeys.Indiana, 220));
            spaces.Add(new PropertySpace("Illinois Avenue", BoardSpace.SpaceKeys.Illinois, 240));
            spaces.Add(new PropertySpace("Atlantic Avenue", BoardSpace.SpaceKeys.Atlantic, 260));
            spaces.Add(new PropertySpace("Ventnor Avenue", BoardSpace.SpaceKeys.Ventnor, 260));
            spaces.Add(new PropertySpace("Marvin Gardens", BoardSpace.SpaceKeys.MarvinGardens, 280));
            spaces.Add(new PropertySpace("Pacific Avenue", BoardSpace.SpaceKeys.Pacific, 300));
            spaces.Add(new PropertySpace("North Carolina Avenue", BoardSpace.SpaceKeys.NorthCarolina, 300));
            spaces.Add(new PropertySpace("Pennsylvania Avenue", BoardSpace.SpaceKeys.Pennsylvania, 320));
            spaces.Add(new PropertySpace("Park Place", BoardSpace.SpaceKeys.ParkPlace, 350));
            spaces.Add(new PropertySpace("Boardwalk", BoardSpace.SpaceKeys.Boardwalk, 400));
        }

        private void CreatePenaltySpaces()
        {
            spaces.Add(new PenaltySpace("Income Tax", BoardSpace.SpaceKeys.IncomeTax, 200));
            spaces.Add(new PenaltySpace("Luxury Tax", BoardSpace.SpaceKeys.LuxuryTax, 75));
        }
    }
}
