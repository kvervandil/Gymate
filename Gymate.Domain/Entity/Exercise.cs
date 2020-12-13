using Gymate.Domain.Common;

namespace Gymate.Domain.Entity
{
    public class Exercise : BaseEntity
    {
        public Exercise(int id, string name, int typeId)
        {
            Id = id;
            Name = name;
            TypeId = typeId;
        }

        public string Name { get; set; }
        public int TypeId { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int Load { get; set; }
    }
}
