using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RevDogs.Core.Model;

namespace RevDogs.Core.Interfaces
{
    public interface IProject2Repository
    {
        Task<Users> GetUsersAsync(int id);
        Task<Users> PutUsersAsync(Users users);
        Task<Users> PostUsersAsync(Users users);
        Task<bool> RemoveUsersAsync(int id);

        // gets all dogs
        Task<IEnumerable<Dogs>> GetDogsAsync();
        // gets dog by id
        Task<Dogs> GetDogsAsync(int id);
        // gets dog by User id
        Task<IEnumerable<Dogs>> GetUserDogsAsync(int Userid);
        Task<Dogs> PutDogsAsync(Dogs dogs);
        Task<Dogs> PostDogsAsync(Dogs dogs);
        Task<bool> RemoveDogsAsync(int id);

        // gets trick progress based off pet ID
        Task<IEnumerable<TricksProgress>> GetTricksProgressAsync(int id);

        Task<TricksProgress> PutTricksProgressAsync(TricksProgress tricksProgress);

        Task<TricksProgress> PostTricksProgressAsync(TricksProgress tricksProgress);

        Task<IEnumerable<DogTypes>> GetDogTypesAsync();

        Task<IEnumerable<Tricks>> GetTricksAsync();
    }
}