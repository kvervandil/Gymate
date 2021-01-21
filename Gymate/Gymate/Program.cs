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
            InformationProvider informationProvider = new InformationProvider();
            MenuActionService actionService = new MenuActionService();
            IService<Exercise> exerciseService = new ExerciseService();
            IService<Routine> routineService = new RoutineService();
            ExerciseManager exerciseManager = new ExerciseManager(actionService,
                                                                  exerciseService,
                                                                  informationProvider);

            RoutineManager routineManager = new RoutineManager(routineService, informationProvider);

            Console.WriteLine("Welcome to Gymate app!");

            exerciseManager.GetAddedExercicesFromFile();
            routineManager.GetAddedRoutineFromFile();

            while (true)
            {
                Console.WriteLine("Please let me know what you want to do:");

                var mainMenu = actionService.GetMenuActionsByMenuName("Main");

                foreach (var menuAction in mainMenu)
                {
                    Console.WriteLine($"{menuAction.Id}. {menuAction.Name}");
                }

                var operation = Console.ReadLine();
                Console.WriteLine("\n");

                switch (operation)
                {
                    case "1":
                        exerciseManager.AddNewExercise();
                        break;
                    case "2":
                        exerciseManager.RemoveExercise();
                        break;
                    case "3":
                        exerciseManager.ShowAllExercises();
                        break;
                    case "4":
                        exerciseManager.ViewExerciseDetails();
                        break;
                    case "5":
                        exerciseManager.ViewExercisesByTypeId();
                        break;
                    case "6":
                        var dayOfWeekId = routineManager.GetRoutineId();
                        exerciseManager.ShowAllExercises();

                        var exerciseToAdd = exerciseManager.GetExerciseById();

                        routineManager.AddSelectedExerciseToRoutineDay(dayOfWeekId, exerciseToAdd);
                        break;
                    case "7":
                        routineManager.ShowWholeRoutine();
                        break;
                    case "8":
                        exerciseManager.UpdateVolumeInExercise();
                        break;
                    case "9":
                        exerciseManager.ExportToXml();
                        break;
                    case "10":
                        routineManager.ExportToXml();
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
