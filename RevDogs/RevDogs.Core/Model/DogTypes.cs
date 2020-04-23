using System;
using System.Collections.Generic;

namespace RevDogs.Core.Model
{
    public class DogTypes
    {

        public int Id { get; set; }
        public string Breed { get; set; }
        public int LifeExpectancy { get; set; }
        public int? Size { get; set; }

        //public List<Dogs> Dogs { get; set; } = new List<Dogs>();
    }
}
