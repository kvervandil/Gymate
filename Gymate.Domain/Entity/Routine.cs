using System.Collections.Generic;
using System.Xml.Serialization;
using Gymate.Domain.Common;

namespace Gymate.Domain.Entity
{
    public class Routine : BaseEntity
    {
        public Routine()
        {

        }

        [XmlElement("Name")]
        public string Name { get; }

        [XmlElement("Exercises")]
        public List<Exercise> ExercisesOfTheDay { get; set; }

        public Routine(int id, string name)
        {
            Id = id;

            Name = name;

            ExercisesOfTheDay = new List<Exercise>();
        }
        
    }
}
