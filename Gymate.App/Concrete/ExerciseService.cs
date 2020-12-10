using System;
using System.Collections.Generic;
using System.Text;
using Gymate.App.Common;
using Gymate.Domain.Entity;

namespace Gymate.App.Concrete
{
    public class ExerciseService : BaseService<Exercise>
    {
        public ExerciseService()
        {
            Items = new List<Exercise>();
        }
        
        public List<Exercise> FindExercisesByTypeId(int typeId)
        {
            return Items.FindAll(e => e.TypeId == typeId);
        }
    }
}