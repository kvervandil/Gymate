using System;
using Gymate.App.Abstract;
using Gymate.App.Concrete;
using Gymate.Domain.Entity;

namespace Gymate.App.Managers
{
    public class RoutineManager
    {
        private IService<Routine> _routineService;

        public RoutineManager(IService<Routine> routineService)
        {
            _routineService = routineService;
        }

        public int GetRoutineById()
        {
            Console.WriteLine("Choose Day of a week: ");

            _routineService.Items.ForEach(routine => Console.WriteLine($"{routine.Id} - {routine.Name}"));

            int.TryParse(Console.ReadKey().KeyChar.ToString(), out var id);

            return id;
        }

        public void AddSelectedExerciseToRoutineDay(int routineDayId, Exercise exercise)
        {
            var routine = _routineService.GetItem(routineDayId);

            routine.ExercisesOfTheDay.Add(exercise);
        }

        public void ShowWholeRoutine()
        {
            var routines = _routineService.GetAllItems();

            foreach (var routine in routines)
            {
                Console.WriteLine(routine.Name);
                for (int i = 0; i < routine.ExercisesOfTheDay.Count; i++)
                {
                    Console.WriteLine($"{i+1}. {routine.ExercisesOfTheDay[i].Name}");
                }

                Console.WriteLine("\n");
            }
        }
    }
}