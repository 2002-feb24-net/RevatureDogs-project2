using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RevDogs.Core.Interfaces;
using RevDogs.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevDogs.DataAccess.Repository
{
    /// <summary>
    /// A repository to manage data access for Revature Dogs API
    /// using Entity Framework
    /// </summary>
    public class Project2Repository : IProject2Repository
    {
        private readonly RevatureDogsP2Context _dbContext;

        private readonly ILogger<Project2Repository> _logger;

        /// <summary>
        /// Inistializes a new Revature Dogs repository.
        /// </summary>
        /// <param name="dbContext">That data source</param>
        /// <param name="logger">the logger</param>
        public Project2Repository(RevatureDogsP2Context dbContext, ILogger<Project2Repository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }
        
        /// <summary>
        /// Getting all users asynchronously
        /// </summary>
        /// <returns>A list of Users</returns>
        public async Task<IEnumerable<Core.Model.Users>> GetUsersAsync()
        {
            List<Users> listUsers = await _dbContext.Users
                                        .Include(d => d.Dogs)
                                        .ToListAsync();
            return listUsers.Select(Mapper.MapUsers);
        }

        /// <summary>
        /// Get a user by ID asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A single user with id</returns>
        public async Task<Core.Model.Users> GetUsersAsync(int id)
        {
            Model.Users users = await _dbContext.Users
                                    .Include(d => d.Dogs)
                                    .FirstOrDefaultAsync(u => u.Id == id);

            return Mapper.MapUsers(users);
        }

        /// <summary>
        /// Get a user by Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>returns a single user with username</returns>
        public async Task<Core.Model.Users> GetUserLoginAsync(string username)
        {
            var users = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);

            return Mapper.MapUsers(users);
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="users"></param>
        /// <returns>Returns the user after updated</returns>
        public async Task<Core.Model.Users> PutUsersAsync(int id, Core.Model.Users users)
        {
            _logger.LogInformation("Updating user with ID {users.Id}.", users.Id);

            // This method only makrs the properties changed
            // as modified. 
            var user = await _dbContext.Users.FindAsync(id);
            var newUser = Mapper.MapUsers(users);

            _dbContext.Entry(user).CurrentValues.SetValues(newUser);

            await _dbContext.SaveChangesAsync();
            return Mapper.MapUsers(newUser);
        }

        /// <summary>
        /// Adds a new Ueser to the DB.
        /// </summary>
        /// <param name="users"></param>
        /// <returns>The user that was added</returns>
        public async Task<Core.Model.Users> PostUsersAsync(Core.Model.Users users)
        {
            var newUser = new Model.Users
            {
                FirstName = users.FirstName,
                LastName = users.LastName,
                UserName = users.UserName
            };

            _logger.LogInformation("Adding a new User.");

            _dbContext.Add(newUser);
            await _dbContext.SaveChangesAsync();

            // After the user is added, return the user
            // by getting the max id (newest user).
            int id = await _dbContext.Users.MaxAsync(u => u.Id);
            var addedUser = _dbContext.Users.Find(id);
            return Mapper.MapUsers(addedUser);
        }

        /// <summary>
        /// Removes User from DB based on ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True of False based on success</returns>
        public async Task<bool> RemoveUsersAsync(int id)
        {
            _logger.LogInformation("Removing User based on {User.Id}, Also removeing " +
                                        "Dogs and TricksProgress", id);
            Model.Users user = await _dbContext.Users
                            .Include(d => d.Dogs)
                            .FirstOrDefaultAsync(u => u.Id == id);

            // Loop through all the dogs, and then TricksProgress
            // and remove each record.
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
        /// Getting all dogs
        /// </summary>
        /// <returns>A list of all Dogs with tricks</returns>
        public async Task<IEnumerable<Core.Model.Dogs>> GetDogsAsync()
        {
            List<Model.Dogs> dogs = await _dbContext.Dogs
                                        .Include(t => t.TricksProgress)
                                        .ThenInclude(t => t.Trick).ToListAsync();

            return dogs.Select(Mapper.MapDogs);
        }

        /// <summary>
        /// Getting dogs, Users, TricksProgress, and Tricks
        /// based on a Dogs ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A dog</returns>
        public async Task<Core.Model.Dogs> GetDogsAsync(int id)
        {
            var dog = await _dbContext.Dogs
                                .Include(u => u.User)
                                .Include(t => t.TricksProgress)
                                .ThenInclude(t => t.Trick)
                                .FirstOrDefaultAsync(d => d.Id == id);

            return Mapper.MapDogs(dog);
        }

        /// <summary>
        /// Getting dogs, Users, TricksProgress, and Tricks
        /// based on a Users ID
        /// </summary>
        /// <param name="Userid"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Core.Model.Dogs>> GetUserDogsAsync(int Userid)
        {
            List<Model.Dogs> dogs = await _dbContext.Dogs
                                        .Include(u => u.User)
                                        .Include(t => t.TricksProgress)
                                        .Include(p => p.DogType)
                                        .Where(d => d.UserId == Userid).ToListAsync();

            return dogs.Select(Mapper.MapDogs);
        }

        /// <summary>
        /// Updating a Dog in the DB
        /// based on Dogs ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dogs"></param>
        /// <returns>The Updated Dog</returns>
        public async Task<Core.Model.Dogs> PutDogsAsync(int id, Core.Model.Dogs dogs)
        {
            _logger.LogInformation("Updating the Dog with ID {Dogs.Id}.", dogs.Id);

            // This method only makrs the properties changed
            // as modified. 
            var oldDog = await _dbContext.Dogs.FindAsync(id);
            var newDog = Mapper.MapDogs(dogs);

            _dbContext.Entry(oldDog).CurrentValues.SetValues(newDog);

            await _dbContext.SaveChangesAsync();

            return Mapper.MapDogs(newDog);
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
