using System;
using Gymate.App.Abstract;
using Gymate.App.Concrete;
using Gymate.App.Helpers;
using Gymate.App.Managers;
using Gymate.Domain.Entity;

namespace Gymate
{
    class Program
    {
        static void Main(string[] args)
        {
            FileManager fileManager = new FileManager();
            InformationProvider informationProvider = new InformationProvider();
            MenuActionService actionService = new MenuActionService();
            IService<Exercise> exerciseService = new ExerciseService();
            IService<Routine> routineService = new RoutineService();
            ExerciseManager exerciseManager = new ExerciseManager(actionService,
                                                                  exerciseService,
                                                                  informationProvider,
                                                                  fileManager);

            RoutineManager routineManager = new RoutineManager(routineService, informationProvider, fileManager);

            Console.WriteLine("Welcome to Gymate app!");

            exerciseManager.GetAddedExercicesFromFile();
            routineManager.GetAddedRoutineFromFile();

            while (true)
            {
                informationProvider.ShowSingleMessage("Please let me know what you want to do:");

                var mainMenu = actionService.GetMenuActionsByMenuName("Main");

                foreach (var menuAction in mainMenu)
                {
                    informationProvider.ShowSingleMessage($"{menuAction.Id}. {menuAction.Name}");
                }

                var operation = informationProvider.GetInputString();
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
