using System;
using System.Collections.Generic;

namespace RevDogsApi.Models
{
    public partial class Users
    {
        public Users()
        {
            Dogs = new HashSet<Dogs>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Score { get; set; }

        public virtual ICollection<Dogs> Dogs { get; set; }
    }
}
