using System;
using System.Collections.Generic;
using Gymate.App.Common;
using Gymate.Domain.Entity;

namespace Gymate.App.Concrete
{
    public class RoutineService : BaseService<Routine>
    {

        public RoutineService()
        {
            InitialiseWeekRoutine();
        }

        public void InitialiseWeekRoutine()
        {
            Items = new List<Routine>
            {
                new Routine(1, "Monday"),
                new Routine(2, "Tuesday"),
                new Routine(3, "Wednesday"),
                new Routine(4, "Thursday"),
                new Routine(5, "Friday"),
                new Routine(6, "Saturday"),
                new Routine(7, "Sunday")
            };
        }
    }
}
