using RevDogs.Core.Interfaces;
using RevDogs.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RevDogs.DataAccess.Repository
{
    public class Project2Repository : IProject2Repository
    {
        private readonly RevatureDogsP2Context _dbContext;

        public Project2Repository(RevatureDogsP2Context dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
        }
    }
}
