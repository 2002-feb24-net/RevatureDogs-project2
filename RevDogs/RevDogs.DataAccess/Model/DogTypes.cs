using System;
using System.Collections.Generic;

namespace RevDogs.DataAccess.Model
{
    public partial class DogTypes
    {
        public DogTypes()
        {
            Dogs = new HashSet<Dogs>();
        }

        public int Id { get; set; }
        public string Breed { get; set; }
        public int LifeExpectancy { get; set; }
        public int? Size { get; set; }

        public virtual ICollection<Dogs> Dogs { get; set; }
    }
}
