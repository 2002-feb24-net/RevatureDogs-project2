using Microsoft.EntityFrameworkCore;
using RevDogs.Core.Interfaces;
using RevDogs.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevDogs.DataAccess.Repository
{
    public class Project2Repository : IProject2Repository
    {
        private readonly RevatureDogsP2Context _dbContext;

        public Project2Repository(RevatureDogsP2Context dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
        }
        /// <summary>
        /// User methods
        /// </summary>
        public async Task<IEnumerable<Core.Model.Users>> GetUsersAsync()
        {
            List<Users> listUsers = await _dbContext.Users
                                        .Include(d => d.Dogs)
                                        .ToListAsync();
            return listUsers.Select(Mapper.MapUsers);
        }
        public async Task<Core.Model.Users> GetUsersAsync(int id)
        {
            Model.Users users = await _dbContext.Users
                                    .Include(d => d.Dogs)
                                    .FirstOrDefaultAsync(u => u.Id == id);

            return Mapper.MapUsers(users);
        }
        public async Task<Core.Model.Users> GetUserLoginAsync(string username)
        {
            var users = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);

            return Mapper.MapUsers(users);
        }
        public async Task<Core.Model.Users> PutUsersAsync(int id, Core.Model.Users users)
        {
            var user = _dbContext.Users.Find(id);
            user.FirstName = users.FirstName;
            user.LastName = users.LastName;
            user.UserName = users.UserName;

            _dbContext.Update(user);

            await _dbContext.SaveChangesAsync();
            return users;
        }
        public async Task<Core.Model.Users> PostUsersAsync(Core.Model.Users users)
        {
            var newUser = new Model.Users
            {
                FirstName = users.FirstName,
                LastName = users.LastName,
                UserName = users.UserName
            };

            _dbContext.Add(newUser);
            await _dbContext.SaveChangesAsync();
            int id = await _dbContext.Users.MaxAsync(u => u.Id);
            var addedUser = _dbContext.Users.Find(id);
            return Mapper.MapUsers(addedUser);
        }
        public async Task<bool> RemoveUsersAsync(int id)
        {
            Model.Users user = await _dbContext.Users
                            .Include(d => d.Dogs)
                            .FirstOrDefaultAsync(u => u.Id == id);

            foreach (var d in user.Dogs)
            {
                foreach (var t in d.TricksProgress)
                {
                    _dbContext.Remove(t);
                }

                _dbContext.Remove(d);
            }

            _dbContext.Remove(user);
            int removed = await _dbContext.SaveChangesAsync();

            return removed > 0;
        }

        /// <summary>
        /// Dogs methods
        /// </summary>
        public async Task<IEnumerable<Core.Model.Dogs>> GetDogsAsync()
        {
            List<Model.Dogs> dogs = await _dbContext.Dogs
                                        .Include(t => t.TricksProgress)
                                        .ThenInclude(t => t.Trick).ToListAsync();

            return dogs.Select(Mapper.MapDogs);
        }

        public async Task<Core.Model.Dogs> GetDogsAsync(int id)
        {
            var dog = await _dbContext.Dogs
                                .Include(u => u.User)
                                .Include(t => t.TricksProgress)
                                .ThenInclude(t => t.Trick)
                                .FirstOrDefaultAsync(d => d.UserId == id);

            return Mapper.MapDogs(dog);
        }

        public async Task<IEnumerable<Core.Model.Dogs>> GetUserDogsAsync(int Userid)
        {
            List<Model.Dogs> dogs = await _dbContext.Dogs
                                        .Include(u => u.User)
                                        .Include(t => t.TricksProgress)
                                        .Include(p => p.DogType)
                                        .Where(d => d.UserId == Userid).ToListAsync();

            return dogs.Select(Mapper.MapDogs);
        }

        public async Task<Core.Model.Dogs> PutDogsAsync(int id, Core.Model.Dogs dogs)
        {
            Model.Dogs oldDog = await _dbContext.Dogs
                                .FirstOrDefaultAsync(o => o.Id == dogs.Id);

            oldDog.Hunger = dogs.Hunger;
            oldDog.Mood = dogs.Mood;
            oldDog.IsAlive = dogs.IsAlive;
            oldDog.Age = dogs.Age;
            oldDog.Energy = dogs.Energy;

            _dbContext.Update(oldDog);
            await _dbContext.SaveChangesAsync();
            var updatedDog = await _dbContext.Dogs
                                    .FindAsync(id);

            return Mapper.MapDogs(updatedDog);
        }

        public async Task<Core.Model.Dogs> PostDogsAsync(Core.Model.Dogs dogs)
        {
            var newDog = new Model.Dogs
            {
                UserId = dogs.UserId,
                DogTypeId = dogs.DogTypeId,
                PetName = dogs.PetName
            };

            _dbContext.Add(newDog);
            await _dbContext.SaveChangesAsync();
            int id = await _dbContext.Dogs.MaxAsync(d => d.Id);
            var nd = await _dbContext.Dogs.FindAsync(id);
            return Mapper.MapDogs(nd);
        }

        public async Task<bool> RemoveDogsAsync(int id)
        {
            Model.Dogs dog = await _dbContext.Dogs.FirstOrDefaultAsync(d => d.Id == id);

            foreach (var trick in dog.TricksProgress)
            {
                _dbContext.TricksProgress.Remove(trick);
            }

            _dbContext.Dogs.Remove(dog);
            int removed = await _dbContext.SaveChangesAsync();

            return removed > 0;
        }

        public async Task<IEnumerable<Core.Model.DogTypes>> GetDogTypesAsync()
        {
            List<Model.DogTypes> dogTypes = await _dbContext.DogTypes
                                                    .Include(d => d.Dogs)
                                                    .ToListAsync();

            return dogTypes.Select(Mapper.MapDogTypes);
        }

        /// <summary>
        /// Trick methods
        /// </summary>
        public async Task<IEnumerable<Core.Model.TricksProgress>> GetTricksProgressAsync(int id)
        {
            List<Model.TricksProgress> tricksProgresses = await _dbContext.TricksProgress
                                                                .Include(t => t.Trick)
                                                                .Where(u => u.PetId == id)
                                                                .ToListAsync();

            return tricksProgresses.Select(Mapper.MapTricksProgress);
        }

        public async Task<Core.Model.TricksProgress> GetTricksProgressByIdAsync(int id)
        {
            var tricksProgress = await _dbContext.TricksProgress
                                                    .Include(t => t.Trick)
                                                    .FirstOrDefaultAsync(t => t.Id == id);

            return Mapper.MapTricksProgress(tricksProgress);
        }

        public async Task<Core.Model.TricksProgress> PutTricksProgressAsync(int id, Core.Model.TricksProgress tricksProgress)
        {
            Model.TricksProgress oldTricks = await _dbContext.TricksProgress
                                                    .FirstOrDefaultAsync(t => t.Id == tricksProgress.Id);

            oldTricks.PetId = tricksProgress.PetId;
            oldTricks.TrickId = tricksProgress.TrickId;
            oldTricks.Progress = tricksProgress.Progress;

            _dbContext.Update(oldTricks);
            await _dbContext.SaveChangesAsync();

            return Mapper.MapTricksProgress(oldTricks);
        }

        public async Task<Core.Model.TricksProgress> PostTricksProgressAsync(Core.Model.TricksProgress tricksProgress)
        {
            var newTricks = new Model.TricksProgress
            {
                PetId = tricksProgress.PetId,
                TrickId = tricksProgress.TrickId,
            };

            _dbContext.Add(newTricks);
            await _dbContext.SaveChangesAsync();
            int id = await _dbContext.TricksProgress.MaxAsync(i => i.Id);
            var nt = await _dbContext.TricksProgress
                                    .Include(t => t.Trick)
                                    .FirstOrDefaultAsync(t => t.Id == id);
            return Mapper.MapTricksProgress(nt);
        }

        public async Task<bool> RemoveTricksProgressAsync(int id)
        {
            Model.TricksProgress tricksProgress = await _dbContext
                                                    .TricksProgress.FirstOrDefaultAsync(t => t.Id == id);

            _dbContext.Remove(tricksProgress);
            int removed = await _dbContext.SaveChangesAsync();

            return removed > 0;
        }

        public async Task<IEnumerable<Core.Model.Tricks>> GetTricksAsync()
        {
            List<Model.Tricks> tricks = await _dbContext.Tricks
                                            .Include(t => t.TricksProgress)
                                            .ToListAsync();

            return tricks.Select(Mapper.MapTricks);
        }
    }
}
