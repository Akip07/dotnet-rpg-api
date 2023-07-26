using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Chara";

        public int HitPoints { get; set; } = 100;

        public int Strength { get; set; } = 10;

        public int Defense { get; set; } = 10;

        public int Inteligence { get; set; } = 10;

        public RpgClass Class { get; set; } = RpgClass.Knight;
    }
}