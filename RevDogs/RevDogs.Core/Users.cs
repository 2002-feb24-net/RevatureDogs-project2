using System;
using System.Collections.Generic;

namespace RevDogs.Core
{
    public class Users
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Score { get; set; }

        public List<Dogs> Dogs { get; set; } = new List<Dogs>();
    }
}
