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
            var listUsers = await _dbContext.Users
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
            var users = await _dbContext.Users
                                    .Include(d => d.Dogs)
                                    .FirstOrDefaultAsync(u => u.Id == id);

            if (users is null)
            {
                return null;
            }

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
            var user = await _dbContext.Users.FindAsync(id);

            if (user is null)
            {
                _logger.LogError("User with ID {userId} does not exist", user.Id);
                return null;
            }
            else
            {
                _logger.LogInformation("Updating user with ID {usersId}.", users.Id);

                // This method only marks the properties changed
                // as modified. 

                var newUser = Mapper.MapUsers(users);

                _dbContext.Entry(user).CurrentValues.SetValues(newUser);

                await _dbContext.SaveChangesAsync();
                return Mapper.MapUsers(newUser);
            }
        }

        /// <summary>
        /// Adds a new Ueser to the DB.
        /// </summary>
        /// <param name="users"></param>
        /// <returns>The user that was added</returns>
        public async Task<Core.Model.Users> PostUsersAsync(Core.Model.Users users)
        {
            var exists = await _dbContext.Users
                                .FirstOrDefaultAsync(u => u.UserName == users.UserName);
            
            // if username already exist in DB
            // return null.
            if (exists is null)
            {
                var newUser = new Users
                {
                    FirstName = users.FirstName,
                    LastName = users.LastName,
                    UserName = users.UserName
                };

                _logger.LogInformation("Adding a new User.");

                _dbContext.Add(newUser);
                await _dbContext.SaveChangesAsync();

                // After the Entity is added, return the Entity
                // by getting the max id (newest Entity).
                int id = await _dbContext.Users.MaxAsync(u => u.Id);
                var addedUser = _dbContext.Users.Find(id);
                return Mapper.MapUsers(addedUser);
            }
            else
            {
                _logger.LogWarning("User name {username} already exist.", exists.UserName);
                return null;
            }
        }

        /// <summary>
        /// Removes User from DB based on ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a Bool based on success/failure</returns>
        public async Task<bool> RemoveUsersAsync(int id)
        {
            var user = await _dbContext.Users
                            .Include(d => d.Dogs)
                            .FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                _logger.LogError("The User with ID {userId} does not exist.", id);
                return false;
            }
            else
            {
                _logger.LogInformation("Removing User based on {User.Id}, Also removeing " +
                                            "Dogs and TricksProgress", id);
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
                await _dbContext.SaveChangesAsync();

                return true;
            }
        }

        /// <summary>
        /// Getting all dogs
        /// </summary>
        /// <returns>A list of all Dogs with tricks</returns>
        public async Task<IEnumerable<Core.Model.Dogs>> GetDogsAsync()
        {
            var dogs = await _dbContext.Dogs
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

            if (dog is null)
            {
                return null;
            }

            return Mapper.MapDogs(dog);
        }

        /// <summary>
        /// Getting dogs, Users, TricksProgress, and Tricks
        /// based on a Users ID (not used yet)
        /// </summary>
        /// <param name="Userid"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Core.Model.Dogs>> GetUserDogsAsync(int Userid)
        {
            var dogs = await _dbContext.Dogs
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
            var oldDog = await _dbContext.Dogs.FindAsync(id);

            if (oldDog is null)
            {
                _logger.LogError("The Dog with ID {dogsId} does not exist", dogs.Id);
                return null;
            }
            else
            {
                _logger.LogInformation("Updating the Dog with ID {dogsId}.", dogs.Id);

                // This method only marks the properties changed
                // as modified.
                var newDog = Mapper.MapDogs(dogs);

                _dbContext.Entry(oldDog).CurrentValues.SetValues(newDog);

                await _dbContext.SaveChangesAsync();

                return Mapper.MapDogs(newDog);
            }
        }

        /// <summary>
        /// Adding a dog to the DB
        /// </summary>
        /// <param name="dogs"></param>
        /// <returns>The Dog added</returns>
        public async Task<Core.Model.Dogs> PostDogsAsync(Core.Model.Dogs dogs)
        {
            var newDog = new Dogs
            {
                UserId = dogs.UserId,
                DogTypeId = dogs.DogTypeId,
                PetName = dogs.PetName
            };

            _logger.LogInformation("Addng an Dog to the DB");

            _dbContext.Add(newDog);
            await _dbContext.SaveChangesAsync();

            // After the Entity is added, return the Entity
            // by getting the max id (newest Entity).
            int id = await _dbContext.Dogs.MaxAsync(d => d.Id);
            var nd = await _dbContext.Dogs.FindAsync(id);
            return Mapper.MapDogs(nd);
        }

        /// <summary>
        /// Removing Dog based on ID
        /// from the DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a Bool based on success/failure</returns>
        public async Task<bool> RemoveDogsAsync(int id)
        {
            var dog = await _dbContext.Dogs.FirstOrDefaultAsync(d => d.Id == id);

            if (dog is null)
            {
                _logger.LogError("The Dog you with ID {dogsId} doesn't exist.", id);
                return false;
            }
            else
            {
                _logger.LogInformation("Removing a dog based on {Dog.Id}, " +
                                    "and all the tricks related to Dog", id);

                // Loop through all the TricksProgress
                // and remove each record.
                foreach (var trick in dog.TricksProgress)
                {
                    _dbContext.TricksProgress.Remove(trick);
                }

                _dbContext.Dogs.Remove(dog);
                await _dbContext.SaveChangesAsync();

                return true;
            }
        }

        /// <summary>
        /// Retreiving all dog types
        /// from the DB
        /// </summary>
        /// <returns>a List of Dog Types</returns>
        public async Task<IEnumerable<Core.Model.DogTypes>> GetDogTypesAsync()
        {
            var dogTypes = await _dbContext.DogTypes
                                    .Include(d => d.Dogs)
                                    .ToListAsync();

            return dogTypes.Select(Mapper.MapDogTypes);
        }

        /// <summary>
        /// Gets a all TricksProgress based on
        /// Dogs Id and includes Tricks
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of a dogs Tricks</returns>
        public async Task<IEnumerable<Core.Model.TricksProgress>> GetTricksProgressAsync(int id)
        {
            var tricksProgresses = await _dbContext.TricksProgress
                                                                .Include(t => t.Trick)
                                                                .Where(u => u.PetId == id)
                                                                .ToListAsync();

            if (tricksProgresses.Count() == 0)
            {
                return null;
            }

            return tricksProgresses.Select(Mapper.MapTricksProgress);
        }

        /// <summary>
        /// Returns a single Trick Progress
        /// based on ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A single Trick Progress</returns>
        public async Task<Core.Model.TricksProgress> GetTricksProgressByIdAsync(int id)
        {
            var tricksProgress = await _dbContext.TricksProgress
                                                    .Include(t => t.Trick)
                                                    .FirstOrDefaultAsync(t => t.Id == id);

            if (tricksProgress is null)
            {
                return null;
            }

            return Mapper.MapTricksProgress(tricksProgress);
        }

        /// <summary>
        /// Updates a dogs Trick Progress based
        /// on Dogs ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tricksProgress"></param>
        /// <returns>The updated TricksProgress</returns>
        public async Task<Core.Model.TricksProgress> PutTricksProgressAsync(int id, Core.Model.TricksProgress tricksProgress)
        {
            var oldTricks = await _dbContext.TricksProgress
                                                    .FirstOrDefaultAsync(t => t.Id == tricksProgress.Id);

            if (oldTricks is null)
            {
                _logger.LogError("The TricksProgress with ID {tricksProgressId} does not exist.", tricksProgress.Id);
                return null;
            }
            else
            {
                _logger.LogInformation("Updating a TricksProgress with ID {tricksProgressId}", tricksProgress.Id);

                // This method only marks the properties changed
                // as modified
                var newTricks = Mapper.MapTricksProgress(tricksProgress);

                _dbContext.Entry(oldTricks).CurrentValues.SetValues(newTricks);
                await _dbContext.SaveChangesAsync();

                return Mapper.MapTricksProgress(newTricks);

            }
        }

        /// <summary>
        /// Adding a new TricksProgress
        /// to the DB
        /// </summary>
        /// <param name="tricksProgress"></param>
        /// <returns>The added TricksProgress</returns>
        public async Task<Core.Model.TricksProgress> PostTricksProgressAsync(Core.Model.TricksProgress tricksProgress)
        {
            var exists = await _dbContext.TricksProgress
                                    .FirstOrDefaultAsync(t => t.PetId == tricksProgress.PetId && t.TrickId == tricksProgress.TrickId);

            if (exists is null)
            {
                var newTricks = new TricksProgress
                {
                    PetId = tricksProgress.PetId,
                    TrickId = tricksProgress.TrickId,
                };

                _logger.LogInformation("Adding a new TricksProgress.");

                _dbContext.Add(newTricks);
                await _dbContext.SaveChangesAsync();

                // After the Entity is added, return
                // the Entity by getting the max id (Newest Entity)
                int id = await _dbContext.TricksProgress.MaxAsync(i => i.Id);
                var nt = await _dbContext.TricksProgress
                                        .Include(t => t.Trick)
                                        .FirstOrDefaultAsync(t => t.Id == id);
                return Mapper.MapTricksProgress(nt);
            }
            else
            {
                _logger.LogInformation("Dog with this ID {tricksProgressId} already knows this Trick.", tricksProgress.PetId);
                return null;
            }
        }

        /// <summary>
        /// Removing TricksProgress from DB
        /// based on ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A bool based on success/failure</returns>
        public async Task<bool> RemoveTricksProgressAsync(int id)
        {
            var tricksProgress = await _dbContext
                        .TricksProgress.FirstOrDefaultAsync(t => t.Id == id);

            if (tricksProgress is null)
            {
                _logger.LogError("TricksProgress with ID {tricksProgressId} does not exist.", id);
                return false;
            }
            else
            {
                _logger.LogInformation("Removing a TricksProgress based on " +
                                            "{TricksProgress.Id}", id);

                _dbContext.Remove(tricksProgress);
                await _dbContext.SaveChangesAsync();

                return true;
            }
        }

        /// <summary>
        /// Get a list of all Tricks
        /// from the DB
        /// </summary>
        /// <returns>Lists of Tricks</returns>
        public async Task<IEnumerable<Core.Model.Tricks>> GetTricksAsync()
        {
            var tricks = await _dbContext.Tricks
                                            .Include(t => t.TricksProgress)
                                            .ToListAsync();

            return tricks.Select(Mapper.MapTricks);
        }
    }
}
