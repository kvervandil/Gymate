using System.Collections.Generic;
using Gymate.Domain.Common;

namespace Gymate.Domain.Entity
{
    public class Routine : BaseEntity
    {
        public string Name { get; }

        public List<Exercise> ExercisesOfTheDay { get; set; }

        public Routine(int id, string name)
        {
            Id = id;

            Name = name;

            ExercisesOfTheDay = new List<Exercise>();
        }
        
    }
}
