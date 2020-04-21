using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RevDogs.Core.Model;

namespace RevDogs.Core.Interfaces
{
    public interface IProject2Repository
    {
        Task<Users> GetUsers(int id);
        Task<Users> PutUsers(Users users);
        Task<Users> PostUsers(Users users);
        Task<Users> DeleteUsers(int id);

        // gets all dogs
        Task<IEnumerable<Dogs>> GetDogs();
        // gets dog by id
        Task<Dogs> GetDogs(int id);
        // gets dog by User id
        Task<IEnumerable<Dogs>> GetUserDogs(int Userid);
        Task<Dogs> PutDogs(Dogs dogs);
        Task<Dogs> PostDogs(Dogs dogs);
        Task<Dogs> DeleteDogs(int id);

        // gets trick progress based off pet ID
        Task<IEnumerable<TricksProgress>> GetTricksProgress(int id);

        Task<TricksProgress> PutTricksProgress(TricksProgress tricksProgress);

        Task<TricksProgress> PostTricksProgress(TricksProgress tricksProgress);
    }
}