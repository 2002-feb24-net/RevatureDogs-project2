using System;
using System.Collections.Generic;

namespace RevDogsApi.Models
{
    public partial class TricksProgress
    {
        public int Id { get; set; }
        public int? PetId { get; set; }
        public int? TrickId { get; set; }
        public int? Progress { get; set; }

        public virtual Dogs Pet { get; set; }
        public virtual Tricks Trick { get; set; }
    }
}
