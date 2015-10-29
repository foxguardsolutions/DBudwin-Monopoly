using System.Collections.Generic;
using System.Linq;
using Monopoly.Game.Properties;

namespace Monopoly.Game.MonopolyBoard
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
            spaces.Add(new RailroadSpace("Reading Railroad", BoardSpace.SpaceKeys.ReadingRR, PropertyColorGroup.Groups.Railroads, 150, 25));
            spaces.Add(new RailroadSpace("Pennsylvania Railroad", BoardSpace.SpaceKeys.PennsylvaniaRR, PropertyColorGroup.Groups.Railroads, 150, 25));
            spaces.Add(new RailroadSpace("B. & O. Railroad", BoardSpace.SpaceKeys.BORR, PropertyColorGroup.Groups.Railroads, 150, 25));
            spaces.Add(new RailroadSpace("Short Line", BoardSpace.SpaceKeys.ShortLine, PropertyColorGroup.Groups.Railroads, 150, 25));
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
            spaces.Add(new UtilitySpace("Electric Company", BoardSpace.SpaceKeys.ElectricCompany, PropertyColorGroup.Groups.Utilities, 150, 0));
            spaces.Add(new UtilitySpace("Water Works", BoardSpace.SpaceKeys.WaterWorks, PropertyColorGroup.Groups.Utilities, 150, 0));
        }

        private void CreatePropertySpaces()
        {
            spaces.Add(new PropertySpace("Mediterranean Avenue", BoardSpace.SpaceKeys.Mediterranean, PropertyColorGroup.Groups.Brown, 60, 5));
            spaces.Add(new PropertySpace("Baltic Avenue", BoardSpace.SpaceKeys.Baltic, PropertyColorGroup.Groups.Brown, 60, 5));
            spaces.Add(new PropertySpace("Oriental Avenue", BoardSpace.SpaceKeys.Oriental, PropertyColorGroup.Groups.LightBlue, 100, 10));
            spaces.Add(new PropertySpace("Vermont Avenue", BoardSpace.SpaceKeys.Vermont, PropertyColorGroup.Groups.LightBlue, 100, 10));
            spaces.Add(new PropertySpace("Connecticut Avenue", BoardSpace.SpaceKeys.Connecticut, PropertyColorGroup.Groups.LightBlue, 120, 10));
            spaces.Add(new PropertySpace("St. Charles Place", BoardSpace.SpaceKeys.StCharles, PropertyColorGroup.Groups.Pink, 140, 20));
            spaces.Add(new PropertySpace("States Avenue", BoardSpace.SpaceKeys.States, PropertyColorGroup.Groups.Pink, 140, 20));
            spaces.Add(new PropertySpace("Virginia Avenue", BoardSpace.SpaceKeys.Virginia, PropertyColorGroup.Groups.Pink, 160, 20));
            spaces.Add(new PropertySpace("St. James Place", BoardSpace.SpaceKeys.StJames, PropertyColorGroup.Groups.Orange, 180, 40));
            spaces.Add(new PropertySpace("Tennessee Avenue", BoardSpace.SpaceKeys.Tennessee, PropertyColorGroup.Groups.Orange, 180, 40));
            spaces.Add(new PropertySpace("New York Avenue", BoardSpace.SpaceKeys.NewYork, PropertyColorGroup.Groups.Orange, 200, 40));
            spaces.Add(new PropertySpace("Kentucky Avenue", BoardSpace.SpaceKeys.Kentucky, PropertyColorGroup.Groups.Red, 220, 50));
            spaces.Add(new PropertySpace("Indiana Avenue", BoardSpace.SpaceKeys.Indiana, PropertyColorGroup.Groups.Red, 220, 50));
            spaces.Add(new PropertySpace("Illinois Avenue", BoardSpace.SpaceKeys.Illinois, PropertyColorGroup.Groups.Red, 240, 50));
            spaces.Add(new PropertySpace("Atlantic Avenue", BoardSpace.SpaceKeys.Atlantic, PropertyColorGroup.Groups.Yellow, 260, 100));
            spaces.Add(new PropertySpace("Ventnor Avenue", BoardSpace.SpaceKeys.Ventnor, PropertyColorGroup.Groups.Yellow, 260, 100));
            spaces.Add(new PropertySpace("Marvin Gardens", BoardSpace.SpaceKeys.MarvinGardens, PropertyColorGroup.Groups.Yellow, 280, 100));
            spaces.Add(new PropertySpace("Pacific Avenue", BoardSpace.SpaceKeys.Pacific, PropertyColorGroup.Groups.Green, 300, 150));
            spaces.Add(new PropertySpace("North Carolina Avenue", BoardSpace.SpaceKeys.NorthCarolina, PropertyColorGroup.Groups.Green, 300, 150));
            spaces.Add(new PropertySpace("Pennsylvania Avenue", BoardSpace.SpaceKeys.Pennsylvania, PropertyColorGroup.Groups.Green, 320, 150));
            spaces.Add(new PropertySpace("Park Place", BoardSpace.SpaceKeys.ParkPlace, PropertyColorGroup.Groups.DarkBlue, 350, 200));
            spaces.Add(new PropertySpace("Boardwalk", BoardSpace.SpaceKeys.Boardwalk, PropertyColorGroup.Groups.DarkBlue, 400, 200));
        }

        private void CreatePenaltySpaces()
        {
            spaces.Add(new PenaltySpace("Income Tax", BoardSpace.SpaceKeys.IncomeTax, 200));
            spaces.Add(new PenaltySpace("Luxury Tax", BoardSpace.SpaceKeys.LuxuryTax, 75));
        }
    }
}
