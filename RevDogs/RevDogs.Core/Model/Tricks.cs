using System;
using System.Collections.Generic;

namespace RevDogs.Core.Model
{
    public class Tricks
    {

        public int Id { get; set; }
        public string TrickName { get; set; }
        public int TrickBenefit { get; set; }
        public int Diffculty { get; set; }
        public int Points { get; set; }

        //public List<TricksProgress> TricksProgress { get; set; } = new List<TricksProgress>();
    }
}
