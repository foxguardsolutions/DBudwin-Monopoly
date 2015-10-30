namespace Monopoly.Game
{
    public abstract class BoardSpace : IBoardSpace
    {
        public enum SpaceKeys
        {
            Go = 0,
            Mediterranean = 1,
            CC1 = 2,
            Baltic = 3,
            IncomeTax = 4,
            ReadingRR = 5,
            Oriental = 6,
            Chance1 = 7,
            Vermont = 8,
            Connecticut = 9,
            Jail = 10,
            StCharles = 11,
            ElectricCompany = 12,
            States = 13,
            Virginia = 14,
            PennsylvaniaRR = 15,
            StJames = 16,
            CC2 = 17,
            Tennessee = 18,
            NewYork = 19,
            FreeParking = 20,
            Kentucky = 21,
            Chance2 = 22,
            Indiana = 23,
            Illinois = 24,
            BORR = 25,
            Atlantic = 26,
            Ventnor = 27,
            WaterWorks = 28,
            MarvinGardens = 29,
            GoToJail = 30,
            Pacific = 31,
            NorthCarolina = 32,
            CC3 = 33,
            Pennsylvania = 34,
            ShortLine = 35,
            Chance3 = 36,
            ParkPlace = 37,
            LuxuryTax = 38,
            Boardwalk = 39
        }

        public string Name { get; set; }
        public SpaceKeys Position { get; set; }
    }
}
