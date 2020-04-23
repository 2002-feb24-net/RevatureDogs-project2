using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RevDogs.DataAccess
{
    public static class Mapper
    {
        public static Core.Model.Dogs MapDogs(Model.Dogs dogs)
        {
            return new Core.Model.Dogs
            {
                Id = dogs.Id,
                DogTypeId = dogs.DogTypeId,
                UserId = dogs.UserId,
                PetName = dogs.PetName,
                Hunger = dogs.Hunger,
                Mood = dogs.Mood,
                IsAlive = dogs.IsAlive,
                AdoptionDate = dogs.AdoptionDate,
                Age = dogs.Age,
                Energy = dogs.Energy,
                TricksProgress = dogs.TricksProgress.Select(MapTricksProgress).ToList()
            };
        }

        public static Core.Model.DogTypes MapDogTypes(Model.DogTypes dogTypes)
        {
            return new Core.Model.DogTypes
            {
                Id = dogTypes.Id,
                Breed = dogTypes.Breed,
                LifeExpectancy = dogTypes.LifeExpectancy,
                Size = dogTypes.Size,
            };
        }

        public static Core.Model.Tricks MapTricks(Model.Tricks tricks)
        {
            return new Core.Model.Tricks
            {
                Id = tricks.Id,
                TrickName = tricks.TrickName,
                TrickBenefit = tricks.TrickBenefit,
                Diffculty = tricks.Diffculty,
                Points = tricks.Points
            };
        }

        public static Core.Model.TricksProgress MapTricksProgress(Model.TricksProgress tricksProgress)
        {
            return new Core.Model.TricksProgress
            {
                Id = tricksProgress.Id,
                PetId = tricksProgress.PetId,
                TrickId = tricksProgress.TrickId,
                Progress = tricksProgress.Progress,
                Trick = Mapper.MapTricks(tricksProgress.Trick)
            };
        }

        public static Core.Model.Users MapUsers(Model.Users users)
        {
            return new Core.Model.Users
            {
                Id = users.Id,
                UserName = users.UserName,
                FirstName = users.FirstName,
                LastName = users.LastName,
                Score = users.Score,
                Dogs = users.Dogs.Select(MapDogs).ToList(),
            };
        }

        public static Model.Dogs MapDogs(Core.Model.Dogs dogs)
        {
            return new Model.Dogs
            {
                Id = dogs.Id,
                DogTypeId = dogs.DogTypeId,
                UserId = dogs.UserId,
                PetName = dogs.PetName,
                Hunger = dogs.Hunger,
                Mood = dogs.Mood,
                IsAlive = dogs.IsAlive,
                AdoptionDate = dogs.AdoptionDate,
                Age = dogs.Age,
                Energy = dogs.Energy,
                TricksProgress = dogs.TricksProgress.Select(MapTricksProgress).ToList()
            };
        }

        public static Model.DogTypes MapDogTypes(Core.Model.DogTypes dogTypes)
        {
            return new Model.DogTypes
            {
                Id = dogTypes.Id,
                Breed = dogTypes.Breed,
                LifeExpectancy = dogTypes.LifeExpectancy,
                Size = dogTypes.Size
            };
        }

        public static Model.Tricks MapTricks(Core.Model.Tricks tricks)
        {
            return new Model.Tricks
            {
                Id = tricks.Id,
                TrickName = tricks.TrickName,
                TrickBenefit = tricks.TrickBenefit,
                Diffculty = tricks.Diffculty,
                Points = tricks.Points
            };
        }

        public static Model.TricksProgress MapTricksProgress(Core.Model.TricksProgress tricksProgress)
        {
            return new Model.TricksProgress
            {
                Id = tricksProgress.Id,
                PetId = tricksProgress.PetId,
                TrickId = tricksProgress.TrickId,
                Progress = tricksProgress.Progress,
                Trick = MapTricks(tricksProgress.Trick)
            };
        }

        public static Model.Users MapUsers(Core.Model.Users users)
        {
            return new Model.Users
            {
                Id = users.Id,
                UserName = users.UserName,
                FirstName = users.FirstName,
                LastName = users.LastName,
                Score = users.Score,
                Dogs = users.Dogs.Select(MapDogs).ToList(),
            };
        }
    }
}