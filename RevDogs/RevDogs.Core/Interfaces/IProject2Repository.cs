using System;
using System.Collections.Generic;
using System.Text;
using RevDogs.Core.Model;

namespace RevDogs.Core.Interfaces
{
    public interface IProject2Repository
    {
        void PutDogs(Dogs dogs);
        void GetDogs(Dogs dogs);
        void PostDogs(Dogs dogs);
        void DeleteDogs(Dogs dogs);
        void GetUsers(Users users);
        void PutUsers(Users users);
        void PostUsers(Users users);
        void DeleteUsers(Users users);



    }
}