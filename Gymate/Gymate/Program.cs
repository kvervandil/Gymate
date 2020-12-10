using System;
using Gymate.App.Abstract;
using Gymate.App.Concrete;
using Gymate.App.Managers;
using Gymate.Domain.Entity;

namespace Gymate
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuActionService actionService = new MenuActionService();
            IService<Exercise> exerciseService = new ExerciseService();
            IService<Routine> routineService = new RoutineService();
            ExerciseManager exerciseManager = new ExerciseManager(actionService, exerciseService);

            RoutineManager routineManager = new RoutineManager(routineService);

            Console.WriteLine("Welcome to Gymate app!");

            while (true)
            {
                Console.WriteLine("Please let me know what you want to do:");

                var mainMenu = actionService.GetMenuActionsByMenuName("Main");

                foreach (var menuAction in mainMenu)
                {
                    Console.WriteLine($"{menuAction.Id}. {menuAction.Name}");
                }

                var operation = Console.ReadKey();
                Console.WriteLine("\n");

                switch (operation.KeyChar)
                {
                    case '1':
                        exerciseManager.AddNewExercise();
                        break;
                    case '2':
                        exerciseManager.RemoveExercise();
                        break;
                    case '3':
                        exerciseManager.ShowAllExercises();
                        break;
                    case '4':
                        exerciseManager.ViewExerciseDetails();
                        break;
                    case '5':
                        exerciseManager.ViewExercisesByTypeId();
                        break;
                    case '6':
                        var dayOfWeekId = routineManager.GetRoutineById();
                        exerciseManager.ShowAllExercises();

                        var exerciseToAdd = exerciseManager.GetExerciseById();

                        routineManager.AddSelectedExerciseToRoutineDay(dayOfWeekId, exerciseToAdd);
                        break;
                    case '7':
                        routineManager.ShowWholeRoutine();
                        break;

                    default:
                        Console.WriteLine("Action you entered does not exist");
                        break;
                }

                Console.WriteLine("\n");
            }
        }

    }
}
