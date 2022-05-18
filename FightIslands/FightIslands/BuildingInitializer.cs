using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightIslands
{
    public enum BuildingTypes
    {
        Miasto, 
        Wioska,
        TransportMorski,
        TransportLadowy,
        Elektrownia,
        GeneratoryWiatrowe,
        Szpieg,
        PlatformaRakietowa,
        Fabryka
    }

    public enum BuildingWeapon
    {
        NotCreator,
        Creator
    }

    public static class BuildingInitializer
    {
        public static int buildIndex;
        public static BuildingTypes buildingType;
        public static BuildingWeapon buildingCreator;
        public static WeaponType weaponCreation;
        public static string buildingName;
        public static string buildingTag;
        public static int minusCashPerTurn;
        public static int minusEnergyPerTurn;
        public static int cashCostBuild;
        public static int energyCostBuild;
        public static int peopleCostBuild;
        public static int addEnergyPerTurn;
        public static int addPeopleOnce;
        public static int addCashPerTurn;
        public static int[] performances; // bad, normal, good

        public static List<Building> CreateBuildings() // LISTA W GUI WEDŁUG KOLEJNOSCI TUTAJ
        {
            List<Building> list = new List<Building>();
            list.Add(CreateMiasto());
            list.Add(CreateWioska());
            list.Add(CreateTransportMorski());
            list.Add(CreateTransportLadowy());
            list.Add(CreateElektrownia());
            list.Add(CreatePoleWiatrowe());
            list.Add(CreateSzpieg());
            list.Add(CreatePlatforma());
            list.Add(CreateFabryka());
            return list;
        }

        private static Building CreateMiasto()
        {
            buildIndex = 1;
            buildingType = BuildingTypes.Miasto;
            buildingCreator = BuildingWeapon.NotCreator;
            weaponCreation = WeaponType.Null;
            buildingName = "Miasto";
            buildingTag = "M";
            minusCashPerTurn = 10;
            minusEnergyPerTurn = 5;
            cashCostBuild = 15;
            energyCostBuild = 12;
            peopleCostBuild = 0;
            addEnergyPerTurn = 0;
            addPeopleOnce = 15;
            addCashPerTurn = 0;
            performances = new int[] { Performances.PerfByType(PerfTypes.Gory), Performances.PerfByType(PerfTypes.Piasek), Performances.PerfByType(PerfTypes.Trawa) };

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Zwiększa populację wyspy o " + addPeopleOnce + " ludzi.");
            builder.AppendLine("Do budowy wymaga : " + energyCostBuild + " energii, " + cashCostBuild + " funduszy.");
            builder.AppendLine("Budynek co turę pobiera : " + minusEnergyPerTurn + " energii, " + minusCashPerTurn + " funduszy");
            builder.AppendLine("");
            builder.AppendLine("Wydajność na podłożu od najlepszego : ");
            for(int i = performances.Length - 1; i >= 0; i--)
            {
                builder.AppendLine(Performances.GetPerfNameByIndex(performances[i]));
            }
            Building b = new Building(buildIndex, buildingType, buildingCreator, weaponCreation, buildingName, buildingTag, minusCashPerTurn, minusEnergyPerTurn, cashCostBuild, energyCostBuild, peopleCostBuild, addEnergyPerTurn, addPeopleOnce, addCashPerTurn, performances);
            b.SetDescription(builder.ToString());
            return b;
        }

        private static Building CreateWioska()
        {
            buildIndex = 2;
            buildingType = BuildingTypes.Wioska;
            buildingCreator = BuildingWeapon.NotCreator;
            weaponCreation = WeaponType.Null;
            buildingName = "Wioska";
            buildingTag = "W";
            minusCashPerTurn = 10;
            minusEnergyPerTurn = 5;
            cashCostBuild = 10;
            energyCostBuild = 8;
            peopleCostBuild = 0;
            addEnergyPerTurn = 0;
            addPeopleOnce = 10;
            addCashPerTurn = 0;
            performances = new int[] { Performances.PerfByType(PerfTypes.Gory), Performances.PerfByType(PerfTypes.Piasek), Performances.PerfByType(PerfTypes.Trawa) };

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Zwiększa populację wyspy o " + addPeopleOnce + " ludzi.");
            builder.AppendLine("Do budowy wymaga : " + energyCostBuild + " energii, " + cashCostBuild + " funduszy.");
            builder.AppendLine("Budynek co turę pobiera : " + minusEnergyPerTurn + " energii, " + minusCashPerTurn + " funduszy");
            builder.AppendLine("");
            builder.AppendLine("Wydajność na podłożu od najlepszego : ");
            for (int i = performances.Length - 1; i >= 0; i--)
            {
                builder.AppendLine(Performances.GetPerfNameByIndex(performances[i]));
            }
            Building b = new Building(buildIndex, buildingType, buildingCreator, weaponCreation, buildingName, buildingTag, minusCashPerTurn, minusEnergyPerTurn, cashCostBuild, energyCostBuild, peopleCostBuild, addEnergyPerTurn, addPeopleOnce, addCashPerTurn, performances);
            b.SetDescription(builder.ToString());
            return b;
        }

        private static Building CreateTransportMorski()
        {
            buildIndex = 3;
            buildingType = BuildingTypes.TransportMorski;
            buildingCreator = BuildingWeapon.NotCreator;
            weaponCreation = WeaponType.Null;
            buildingName = "Transport morski";
            buildingTag = "Tm";
            minusCashPerTurn = 0;
            minusEnergyPerTurn = 5;
            cashCostBuild = 13;
            energyCostBuild = 12;
            peopleCostBuild = 8;
            addEnergyPerTurn = 0;
            addPeopleOnce = 0;
            addCashPerTurn = 15;
            performances = new int[] { Performances.PerfByType(PerfTypes.Gory), Performances.PerfByType(PerfTypes.Trawa), Performances.PerfByType(PerfTypes.Piasek) };

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Generuje fundusze wyspy w ilości : " + addCashPerTurn + " co turę");
            builder.AppendLine("Do budowy wymaga : " + energyCostBuild + " energii, " + cashCostBuild + " funduszy, " + peopleCostBuild + " ludzi");
            builder.AppendLine("Budynek co turę pobiera : " + minusEnergyPerTurn + " energii");
            builder.AppendLine("");
            builder.AppendLine("Wydajność na podłożu od najlepszego : ");
            for (int i = performances.Length - 1; i >= 0; i--)
            {
                builder.AppendLine(Performances.GetPerfNameByIndex(performances[i]));
            }

            Building b = new Building(buildIndex, buildingType, buildingCreator, weaponCreation, buildingName, buildingTag, minusCashPerTurn, minusEnergyPerTurn, cashCostBuild, energyCostBuild, peopleCostBuild, addEnergyPerTurn, addPeopleOnce, addCashPerTurn, performances);
            b.SetDescription(builder.ToString());
            return b;
        }

        private static Building CreateTransportLadowy()
        {
            buildIndex = 4;
            buildingType = BuildingTypes.TransportLadowy;
            buildingCreator = BuildingWeapon.NotCreator;
            weaponCreation = WeaponType.Null;
            buildingName = "Transport lądowy";
            buildingTag = "Tl";
            minusCashPerTurn = 0;
            minusEnergyPerTurn = 5;
            cashCostBuild = 15;
            energyCostBuild = 15;
            peopleCostBuild = 10;
            addEnergyPerTurn = 0;
            addPeopleOnce = 0;
            addCashPerTurn = 17;
            performances = new int[] { Performances.PerfByType(PerfTypes.Gory), Performances.PerfByType(PerfTypes.Piasek), Performances.PerfByType(PerfTypes.Trawa) };

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Generuje fundusze wyspy w ilości : " + addCashPerTurn + " co turę");
            builder.AppendLine("Do budowy wymaga : " + energyCostBuild + " energii, " + cashCostBuild + " funduszy, " + peopleCostBuild + " ludzi");
            builder.AppendLine("Budynek co turę pobiera : " + minusEnergyPerTurn + " energii");
            builder.AppendLine("");
            builder.AppendLine("Wydajność na podłożu od najlepszego : ");
            for (int i = performances.Length - 1; i >= 0; i--)
            {
                builder.AppendLine(Performances.GetPerfNameByIndex(performances[i]));
            }

            Building b = new Building(buildIndex, buildingType, buildingCreator, weaponCreation, buildingName, buildingTag, minusCashPerTurn, minusEnergyPerTurn, cashCostBuild, energyCostBuild, peopleCostBuild, addEnergyPerTurn, addPeopleOnce, addCashPerTurn, performances);
            b.SetDescription(builder.ToString());
            return b;
        }

        private static Building CreateElektrownia()
        {
            buildIndex = 5;
            buildingType = BuildingTypes.Elektrownia;
            buildingCreator = BuildingWeapon.NotCreator;
            weaponCreation = WeaponType.Null;
            buildingName = "Elektrownia";
            buildingTag = "E";
            minusCashPerTurn = 8;
            minusEnergyPerTurn = 0;
            cashCostBuild = 10;
            energyCostBuild = 0;
            peopleCostBuild = 7;
            addEnergyPerTurn = 12;
            addPeopleOnce = 0;
            addCashPerTurn = 0;
            performances = new int[] { Performances.PerfByType(PerfTypes.Gory), Performances.PerfByType(PerfTypes.Piasek), Performances.PerfByType(PerfTypes.Trawa) };

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Generuje energię dla wyspy w ilości : " + addEnergyPerTurn + " co turę");
            builder.AppendLine("Do budowy wymaga : " + energyCostBuild + " energii, " + cashCostBuild + " funduszy, " + peopleCostBuild + " ludzi");
            builder.AppendLine("Budynek co turę pobiera : " + minusCashPerTurn + " funduszy");
            builder.AppendLine("");
            builder.AppendLine("Wydajność na podłożu od najlepszego : ");
            for (int i = performances.Length - 1; i >= 0; i--)
            {
                builder.AppendLine(Performances.GetPerfNameByIndex(performances[i]));
            }

            Building b = new Building(buildIndex, buildingType, buildingCreator, weaponCreation, buildingName, buildingTag, minusCashPerTurn, minusEnergyPerTurn, cashCostBuild, energyCostBuild, peopleCostBuild, addEnergyPerTurn, addPeopleOnce, addCashPerTurn, performances);
            b.SetDescription(builder.ToString());
            return b;
        }

        private static Building CreatePoleWiatrowe()
        {
            buildIndex = 6;
            buildingType = BuildingTypes.GeneratoryWiatrowe;
            buildingCreator = BuildingWeapon.NotCreator;
            weaponCreation = WeaponType.Null;
            buildingName = "Generatory wiatrowe";
            buildingTag = "Gw";
            minusCashPerTurn = 4;
            minusEnergyPerTurn = 0;
            cashCostBuild = 8;
            energyCostBuild = 0;
            peopleCostBuild = 6;
            addEnergyPerTurn = 10;
            addPeopleOnce = 0;
            addCashPerTurn = 0;
            performances = new int[] { Performances.PerfByType(PerfTypes.Trawa), Performances.PerfByType(PerfTypes.Piasek), Performances.PerfByType(PerfTypes.Gory) };

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Generuje energię dla wyspy w ilości : " + addEnergyPerTurn + " co turę");
            builder.AppendLine("Do budowy wymaga : " + energyCostBuild + " energii, " + cashCostBuild + " funduszy, " + peopleCostBuild + " ludzi");
            builder.AppendLine("Budynek co turę pobiera : " + minusCashPerTurn + " funduszy");
            builder.AppendLine("");
            builder.AppendLine("Wydajność na podłożu od najlepszego : ");
            for (int i = performances.Length - 1; i >= 0; i--)
            {
                builder.AppendLine(Performances.GetPerfNameByIndex(performances[i]));
            }

            Building b = new Building(buildIndex, buildingType, buildingCreator, weaponCreation, buildingName, buildingTag, minusCashPerTurn, minusEnergyPerTurn, cashCostBuild, energyCostBuild, peopleCostBuild, addEnergyPerTurn, addPeopleOnce, addCashPerTurn, performances);
            b.SetDescription(builder.ToString());
            return b;
        }
        
        private static Building CreateSzpieg()
        {
            buildIndex = 7;
            buildingType = BuildingTypes.Szpieg;
            buildingCreator = BuildingWeapon.NotCreator;
            weaponCreation = WeaponType.Null;
            buildingName = "Siedziba szpiegów";
            buildingTag = "Ss";
            minusCashPerTurn = 10;
            minusEnergyPerTurn = 10;
            cashCostBuild = 15;
            energyCostBuild = 15;
            peopleCostBuild = 4;
            addEnergyPerTurn = 0;
            addPeopleOnce = 0;
            addCashPerTurn = 0;
            performances = new int[] { Performances.PerfByType(PerfTypes.Trawa), Performances.PerfByType(PerfTypes.Piasek), Performances.PerfByType(PerfTypes.Gory) };

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Co turę ujawnia (zależnie od poziomu) 1, 2 lub 3 pola przeciwnika, może być wybudowny tylko raz!");
            builder.AppendLine("Do budowy wymaga : " + energyCostBuild + " energii, " + cashCostBuild + " funduszy, " + peopleCostBuild + " ludzi");
            builder.AppendLine("Budynek co turę pobiera : " + minusEnergyPerTurn + " energii, " + minusCashPerTurn + " funduszy");
            builder.AppendLine("");
            builder.AppendLine("Wydajność na podłożu od najlepszego : ");
            for (int i = performances.Length - 1; i >= 0; i--)
            {
                builder.AppendLine(Performances.GetPerfNameByIndex(performances[i]));
            }

            Building b = new Building(buildIndex, buildingType, buildingCreator, weaponCreation, buildingName, buildingTag, minusCashPerTurn, minusEnergyPerTurn, cashCostBuild, energyCostBuild, peopleCostBuild, addEnergyPerTurn, addPeopleOnce, addCashPerTurn, performances);
            b.SetDescription(builder.ToString());
            return b;
        }

        private static Building CreatePlatforma()
        {
            buildIndex = 8;
            buildingType = BuildingTypes.PlatformaRakietowa;
            buildingCreator = BuildingWeapon.Creator;
            weaponCreation = WeaponType.Rakieta;
            buildingName = "Platforma rakietowa";
            buildingTag = "Pr";
            minusCashPerTurn = 8;
            minusEnergyPerTurn = 12;
            cashCostBuild = 9;
            energyCostBuild = 9;
            peopleCostBuild = 3;
            addEnergyPerTurn = 0;
            addPeopleOnce = 0;
            addCashPerTurn = 0;
            performances = new int[] { Performances.PerfByType(PerfTypes.Trawa), Performances.PerfByType(PerfTypes.Piasek), Performances.PerfByType(PerfTypes.Gory) };

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Budynek co turę dodaje jedną rakietę graczowi.");
            builder.AppendLine("Do budowy wymaga : " + energyCostBuild + " energii, " + cashCostBuild + " funduszy, " + peopleCostBuild + " ludzi");
            builder.AppendLine("Budynek co turę pobiera : " + minusEnergyPerTurn + " energii, " + minusCashPerTurn + " funduszy");

            Building b = new Building(buildIndex, buildingType, buildingCreator, weaponCreation, buildingName, buildingTag, minusCashPerTurn, minusEnergyPerTurn, cashCostBuild, energyCostBuild, peopleCostBuild, addEnergyPerTurn, addPeopleOnce, addCashPerTurn, performances);
            b.SetDescription(builder.ToString());
            return b;
        }

        private static Building CreateFabryka()
        {
            buildIndex = 9;
            buildingType = BuildingTypes.Fabryka;
            buildingCreator = BuildingWeapon.Creator;
            weaponCreation = WeaponType.Bomba;
            buildingName = "Fabryka";
            buildingTag = "Fa";
            minusCashPerTurn = 18;
            minusEnergyPerTurn = 20;
            cashCostBuild = 18;
            energyCostBuild = 18;
            peopleCostBuild = 15;
            addEnergyPerTurn = 0;
            addPeopleOnce = 0;
            addCashPerTurn = 0;
            performances = new int[] { Performances.PerfByType(PerfTypes.Trawa), Performances.PerfByType(PerfTypes.Piasek), Performances.PerfByType(PerfTypes.Gory) };

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Budynek co turę dodaje jedną bombę graczowi.");
            builder.AppendLine("Do budowy wymaga : " + energyCostBuild + " energii, " + cashCostBuild + " funduszy, " + peopleCostBuild + " ludzi");
            builder.AppendLine("Budynek co turę pobiera : " + minusEnergyPerTurn + " energii, " + minusCashPerTurn + " funduszy");

            Building b = new Building(buildIndex, buildingType, buildingCreator, weaponCreation, buildingName, buildingTag, minusCashPerTurn, minusEnergyPerTurn, cashCostBuild, energyCostBuild, peopleCostBuild, addEnergyPerTurn, addPeopleOnce, addCashPerTurn, performances);
            b.SetDescription(builder.ToString());
            return b;
        }
    }
}
