using System;
using System.Collections.Generic;

namespace RevDogs.Core.Model
{
    public class Dogs
    {

        public int Id { get; set; }
        public int? DogTypeId { get; set; }
        public int? UserId { get; set; }
        public string PetName { get; set; }
        public int? Hunger { get; set; }
        public int? Mood { get; set; }
        public bool? IsAlive { get; set; }
        public DateTime? AdoptionDate { get; set; }
        public int? Age { get; set; }
        public int? Energy { get; set; }
        //public DogTypes DogType { get; set; }
        //public Users User { get; set; }
        public List<TricksProgress> TricksProgress { get; set; } = new List<TricksProgress>();
    }
}
