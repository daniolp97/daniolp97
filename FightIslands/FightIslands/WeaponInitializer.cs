using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightIslands
{
    public static class WeaponInitializer
    {
        public static List<Weapon> CreateWeapons()
        {
            List<Weapon> list = new List<Weapon>();
            list.Add(CreateRakieta());
            list.Add(CreateBomba());
            return list;
            
        }

        private static Weapon CreateRakieta()
        {
            List<Point> points = new List<Point>();
            Weapon w = new Weapon();
            w.weaponIndex = 1;
            w.weaponName = "Rakieta";
            w.weaponCount = 0;
            w.weaponType = WeaponType.Rakieta;
            w.weaponTag = "R";
            w.weaponDesc = "Zwykła rakieta";
            w.multiplierAnimSize = 3;

            points.Add(new Point(0, 0));
            w.weaponRangePoints = points;
            return w;
        }

        private static Weapon CreateBomba()
        {
            List<Point> points = new List<Point>();
            Weapon w = new Weapon();
            w.weaponIndex = 2;
            w.weaponName = "Bomba";
            w.weaponCount = 0;
            w.weaponType = WeaponType.Bomba;
            w.weaponTag = "B";
            w.weaponDesc = "Zwykła bomba";
            w.multiplierAnimSize = 5;

            points.Add(new Point(0, 0));
            points.Add(new Point(-1, 0));
            points.Add(new Point(1, 0));
            points.Add(new Point(0, -1));
            points.Add(new Point(0, 1));

            w.weaponRangePoints = points;
            return w;
        }
    }
}
