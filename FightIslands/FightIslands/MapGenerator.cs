using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightIslands
{
    public enum PerfTypes
    {
        Woda,
        Piasek,
        Trawa,
        Gory,
        Zniszczone
    }

    public static class Performances
    {
        private static float[] wodaPerf;
        private static float[] piasekPerf;
        private static float[] trawaPerf;
        private static float[] goryPerf;
        private static float[] destroyedPerf;

        private static Color wodaPerfColor;
        private static Color piasekPerfColor;
        private static Color trawaPerfColor;
        private static Color goryPerfColor;
        private static Color destroyedColor;

        public static void Initilize()
        {
            wodaPerf = new float[] { -0.1f, 0.1f };
            piasekPerf = new float[] { 0.1f, 0.3f };
            trawaPerf = new float[] { 0.3f, 0.8f };
            goryPerf = new float[] { 0.8f, 2.0f };
            destroyedPerf = new float[] { -1.1f, -0.5f };

            wodaPerfColor = Color.Blue;
            piasekPerfColor = Color.SandyBrown;
            trawaPerfColor = Color.Green;
            goryPerfColor = Color.Gray;
            destroyedColor = Color.FromArgb(64, 64, 64);
        }

        private static bool Between(float val, float from, float to)
        {
            if (val >= from && val < to) return true;
            return false;
        }

        public static Color GetColorByPerf(float perf)
        {
            if (Between(perf, wodaPerf[0], wodaPerf[1])) return wodaPerfColor;
            if (Between(perf, piasekPerf[0],piasekPerf[1])) return piasekPerfColor;
            if (Between(perf, trawaPerf[0], trawaPerf[1])) return trawaPerfColor;
            if (Between(perf, goryPerf[0], goryPerf[1])) return goryPerfColor;
            if (Between(perf, destroyedPerf[0], destroyedPerf[1])) return destroyedColor;
            return Color.Transparent;
        }

        public static int GetPerformanceLevel(float perf)
        {
            if (Between(perf, wodaPerf[0], wodaPerf[1])) return 0;
            if (Between(perf, piasekPerf[0], piasekPerf[1])) return 1;
            if (Between(perf, trawaPerf[0], trawaPerf[1])) return 2;
            if (Between(perf, goryPerf[0], goryPerf[1])) return 3;
            if (Between(perf, destroyedPerf[0], destroyedPerf[1])) return 4;
            return 0;
        }

        public static int PerfByType(PerfTypes type)
        {
            if (type == PerfTypes.Woda) return 0;
            if (type == PerfTypes.Piasek) return 1;
            if (type == PerfTypes.Trawa) return 2;
            if (type == PerfTypes.Gory) return 3;
            if (type == PerfTypes.Zniszczone) return 4;
            return 0;
        }

        public static PerfTypes GetTypeByPerf(float perf)
        {
            if (Between(perf, wodaPerf[0], wodaPerf[1])) return PerfTypes.Woda;
            if (Between(perf, piasekPerf[0], piasekPerf[1])) return PerfTypes.Piasek;
            if (Between(perf, trawaPerf[0], trawaPerf[1])) return PerfTypes.Trawa;
            if (Between(perf, goryPerf[0], goryPerf[1])) return PerfTypes.Gory;
            if (Between(perf, destroyedPerf[0], destroyedPerf[1])) return PerfTypes.Zniszczone;
            return PerfTypes.Woda;
        }

        public static string GetPerfNameByIndex(int index)
        {
            if (index == 0) return "Woda";
            if (index == 1) return "Piasek";
            if (index == 2) return "Trawa";
            if (index == 3) return "Góry";
            return "Zniszczony teren";
        }
    }

    public class MapGenerator
    {
        public static float[,] Generate(int width, int height, int seed, float waterLevel = 1f, int smoothingIterations = 5)
        {
            float[,] map = new float[width, height];
            int land = (int)(width * height * 0.7f);
            Random rand = new Random(seed);

            int borderX = (int)(width * 0.1f);
            int borderY = (int)(height * 0.1f);


            int x = width / 2;
            int y = height / 2;
            int nx, ny;

            Poi[] directions;
            bool ok = false;

            for (int i = 0; i < land; ++i)
            {
                ok = false;
                while (!ok)
                {
                    directions = _neightbours.OrderBy(p => rand.Next()).ToArray();
                    foreach (Poi p in directions)
                    {
                        nx = x + p.X;
                        ny = y + p.Y;

                        if (nx < (borderX + rand.Next(0, borderX)) || ny < (borderY + rand.Next(0, borderY)) || nx > width - (borderX + rand.Next(0, borderX)) || ny > height - (borderY + rand.Next(0, borderY)) || map[nx, ny] > waterLevel)
                            continue;
                        map[nx, ny] = (float)rand.NextDouble();
                        x = nx;
                        y = ny;
                        ok = true;
                        break;
                    }
                    if (ok) break;
                    x = rand.Next(borderX, width - borderX);
                    y = rand.Next(borderY, height - borderY);
                }
            }

            for (int s = 0; s < smoothingIterations; ++s)
            {
                int n = 0;
                nx = 0;
                ny = 0;
                float value = 0;
                for (x = 0; x < width; ++x)
                {
                    for (y = 0; y < height; ++y)
                    {
                        n = 1;
                        value = map[x, y];
                        foreach (Poi p in _neightbours)
                        {
                            nx = x + p.X;
                            ny = y + p.Y;
                            if (nx < 0 || nx >= width || ny < 0 || ny >= height)
                                continue;
                            value += map[nx, ny];
                            ++n;
                        }
                        map[x, y] = value / n;
                    }
                }
            }
            return map;
        }

        private static Poi[] _neightbours = new Poi[]
                                        {
                                            new Poi(){X=-1,Y=0},
                                            new Poi(){X=1,Y=0},
                                            new Poi(){X=0,Y=-1},
                                            new Poi(){X=0,Y=1}
                                        };


    }

    public struct Poi
    {
        public int X;
        public int Y;
    }
}
