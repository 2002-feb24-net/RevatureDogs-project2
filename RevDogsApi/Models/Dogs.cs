using System;
using System.Collections.Generic;

namespace RevDogsApi.Models
{
    public partial class Dogs
    {
        public Dogs()
        {
            TricksProgress = new HashSet<TricksProgress>();
        }

        public int Id { get; set; }
        public int? DogTypeId { get; set; }
        public int? UserId { get; set; }
        public string PetName { get; set; }
        public int? Hunger { get; set; }
        public string Mood { get; set; }
        public bool? IsAlive { get; set; }
        public DateTime? AdoptionDate { get; set; }
        public int? Age { get; set; }
        public int? Energy { get; set; }

        public virtual DogTypes DogType { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<TricksProgress> TricksProgress { get; set; }
    }
}
