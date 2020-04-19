using System;
using System.Collections.Generic;

namespace RevDogsApi.Models
{
    public partial class Tricks
    {
        public Tricks()
        {
            TricksProgress = new HashSet<TricksProgress>();
        }

        public int Id { get; set; }
        public string TrickName { get; set; }
        public int TrickBenefit { get; set; }
        public int Diffculty { get; set; }
        public int Points { get; set; }

        public virtual ICollection<TricksProgress> TricksProgress { get; set; }
    }
}
