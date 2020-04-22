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

        public async Task<IEnumerable<Core.Model.Dogs>> GetDogsAsync()
        {
            List<Dogs> dogs = await _dbContext.Dogs
                                        .Include(u => u.User)
                                        .Include(t => t.TricksProgress).ToListAsync();

            return dogs.Select(Mapper.MapDogs);
        }

        public async Task<Core.Model.Dogs> GetDogsAsync(int id)
        {
            var dog = await _dbContext.Dogs
                                .Include(u => u.User)
                                .Include(u => u.TricksProgress)
                                .FirstOrDefaultAsync(d => d.UserId == id);

            return Mapper.MapDogs(dog);
        }

        public async Task<IEnumerable<Core.Model.DogTypes>> GetDogTypesAsync()
        {
            List<DogTypes> dogTypes = await _dbContext.DogTypes
                                                    .Include(d => d.Dogs)
                                                    .ToListAsync();

            return dogTypes.Select(Mapper.MapDogTypes);
        }

        public async Task<IEnumerable<Core.Model.Tricks>> GetTricksAsync()
        {
            List<Tricks> tricks = await _dbContext.Tricks
                                            .Include(t => t.TricksProgress)
                                            .ToListAsync();

            return tricks.Select(Mapper.MapTricks);
        }

        public async Task<IEnumerable<Core.Model.TricksProgress>> GetTricksProgressAsync(int id)
        {
            List<TricksProgress> tricksProgresses = await _dbContext.TricksProgress
                                                                .Include(t => t.Trick)
                                                                .Include(d => d.Pet)
                                                                .ToListAsync();

            return tricksProgresses.Select(Mapper.MapTricksProgress);
        }

        public async Task<IEnumerable<Core.Model.Dogs>> GetUserDogsAsync(int Userid)
        {
            List<Dogs> dogs = await _dbContext.Dogs
                                        .Include(u => u.User)
                                        .Include(t => t.TricksProgress)
                                        .Include(p => p.DogType)
                                        .Where(d => d.UserId == Userid).ToListAsync();

            return dogs.Select(Mapper.MapDogs);
        }

        public async Task<Core.Model.Users> GetUsersAsync(int id)
        {
            Users users = await _dbContext.Users
                                    .Include(d => d.Dogs)
                                    .FirstOrDefaultAsync(u => u.Id == id);

            return Mapper.MapUsers(users);
        }

        public async Task<Core.Model.Dogs> PostDogsAsync(Core.Model.Dogs dogs)
        {
            var newDog = new Dogs
            {
                UserId = dogs.UserId,
                DogTypeId = dogs.DogTypeId,
                PetName = dogs.PetName
            };

            _dbContext.Add(newDog);
            await _dbContext.SaveChangesAsync();

            return Mapper.MapDogs(newDog);
        }

        public async Task<Core.Model.TricksProgress> PostTricksProgressAsync(Core.Model.TricksProgress tricksProgress)
        {
            var newTricks = new TricksProgress
            {
                PetId = tricksProgress.PetId,
                TrickId = tricksProgress.TrickId
            };

            _dbContext.Add(newTricks);
            await _dbContext.SaveChangesAsync();

            return Mapper.MapTricksProgress(newTricks);
        }

        public async Task<Core.Model.Users> PostUsersAsync(Core.Model.Users users)
        {
            var newUser = new Users
            {
                FirstName = users.FirstName,
                LastName = users.LastName,
                UserName = users.UserName
            };

            _dbContext.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return Mapper.MapUsers(newUser);
        }

        public async Task<Core.Model.Dogs> PutDogsAsync(Core.Model.Dogs dogs)
        {
            Dogs oldDog = await _dbContext.Dogs
                                .FirstOrDefaultAsync(o => o.Id == dogs.Id);

            oldDog.Hunger = dogs.Hunger;
            oldDog.Mood = dogs.Mood;
            oldDog.IsAlive = dogs.IsAlive;
            oldDog.Age = dogs.Age;
            oldDog.Energy = dogs.Energy;

            _dbContext.Update(oldDog);
            await _dbContext.SaveChangesAsync();

            return Mapper.MapDogs(oldDog);
        }

        public async Task<Core.Model.TricksProgress> PutTricksProgressAsync(Core.Model.TricksProgress tricksProgress)
        {
            TricksProgress oldTricks = await _dbContext.TricksProgress
                                                    .FirstOrDefaultAsync(t => t.Id == tricksProgress.Id);

            oldTricks.PetId = tricksProgress.PetId;
            oldTricks.TrickId = tricksProgress.TrickId;
            oldTricks.Progress = tricksProgress.Progress;

            _dbContext.Update(oldTricks);
            await _dbContext.SaveChangesAsync();

            return Mapper.MapTricksProgress(oldTricks);
        }

        public async Task<Core.Model.Users> PutUsersAsync(Core.Model.Users users)
        {
            Users oldUser = await _dbContext.Users
                                    .FirstOrDefaultAsync(u => u.Id == users.Id);

            oldUser.FirstName = users.FirstName;
            oldUser.LastName = users.LastName;
            oldUser.UserName = users.UserName;
            oldUser.Score = users.Score;

            _dbContext.Update(oldUser);
            await _dbContext.SaveChangesAsync();

            return Mapper.MapUsers(oldUser);
        }

        public async Task<bool> RemoveDogsAsync(int id)
        {
            Dogs dog = await _dbContext.Dogs.FirstOrDefaultAsync(d => d.Id == id);

            foreach (var trick in dog.TricksProgress)
            {
                _dbContext.TricksProgress.Remove(trick);
            }

            _dbContext.Dogs.Remove(dog);
            int removed = await _dbContext.SaveChangesAsync();

            return removed > 0;
        }

        public async Task<bool> RemoveUsersAsync(int id)
        {
            Users user = await _dbContext.Users
                            .Include(d => d.Dogs)
                            .FirstOrDefaultAsync(u => u.Id == id);

            foreach(var d in user.Dogs)
            {
                foreach(var t in d.TricksProgress)
                {
                    _dbContext.Remove(t);
                }

                _dbContext.Remove(d);
            }

            _dbContext.Remove(user);
            int removed = await _dbContext.SaveChangesAsync();

            return removed > 0;
        }
    }
}
