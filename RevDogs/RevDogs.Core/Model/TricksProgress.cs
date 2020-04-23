using System;
using System.Collections.Generic;

namespace RevDogs.Core.Model
{
    public class TricksProgress
    {
        public int Id { get; set; }
        public int? PetId { get; set; }
        public int? TrickId { get; set; }
        public int? Progress { get; set; }

        //public Dogs Pet { get; set; }
        public Tricks Trick { get; set; }
    }
}
