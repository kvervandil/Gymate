using Gymate.Domain.Common;
using System.Xml.Serialization;

namespace Gymate.Domain.Entity
{
    public class Exercise : BaseEntity
    {
        public Exercise()
        {

        }

        public Exercise(int id, string name, int typeId)
        {
            Id = id;
            Name = name;
            TypeId = typeId;
        }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Type")]
        public int TypeId { get; set; }

        [XmlElement("Sets")]
        public int Sets { get; set; }

        [XmlElement("Reps")]
        public int Reps { get; set; }

        [XmlElement("Load")]
        public int Load { get; set; }
    }
}
