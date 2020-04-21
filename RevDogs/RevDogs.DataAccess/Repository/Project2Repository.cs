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

        public void DeleteDogs(Core.Model.Dogs dogs)
        {
            throw new NotImplementedException();
        }

        public void DeleteUsers(Core.Model.Users users)
        {
            throw new NotImplementedException();
        }

        public void GetDogs(Core.Model.Dogs dogs)
        {
            throw new NotImplementedException();
        }

        public void GetUsers(Core.Model.Users users)
        {
            throw new NotImplementedException();
        }

        public void PostDogs(Core.Model.Dogs dogs)
        {
            throw new NotImplementedException();
        }

        public void PostUsers(Core.Model.Users users)
        {
            throw new NotImplementedException();
        }

        public void PutDogs(Core.Model.Dogs dogs)
        {
            throw new NotImplementedException();
        }

        public void PutUsers(Core.Model.Users users)
        {
            throw new NotImplementedException();
        }
    }
}
