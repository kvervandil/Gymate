using System;
using System.Linq;
using Gymate.App.Abstract;
using Gymate.App.Concrete;
using Gymate.Domain.Entity;

namespace Gymate.App.Managers
{
    public class ExerciseManager
    {
        private readonly MenuActionService _actionService;
        private IService<Exercise> _exerciseService;

        public ExerciseManager(MenuActionService actionService, IService<Exercise> exerciseService)
        {
            _exerciseService = exerciseService;
            _actionService = actionService;
        }

        public int AddNewExercise()
        {
            var addNewExerciseMenu = _actionService.GetMenuActionsByMenuName("AddNewExerciseMenu");

            Console.WriteLine("Please select exercise type:");

            addNewExerciseMenu.ForEach(menuAction => Console.WriteLine($"{menuAction.Id}. {menuAction.Name}"));

            int.TryParse(Console.ReadLine(), out var typeId);

            Console.WriteLine("\nPlease insert name for item: ");
            var name = Console.ReadLine();
            var lastId = _exerciseService.GetLastId();

            var exercise = new Exercise(lastId + 1, name, typeId);

            _exerciseService.AddItem(exercise);

            return exercise.Id;
        }

        public void RemoveExercise()
        {
            Console.WriteLine("Please enter id for exercise you want to remove");

            ShowAllExercises();

            var operation = Console.ReadKey();
            int.TryParse(operation.KeyChar.ToString(), out var id);
            
            var itemToRemove = _exerciseService.Items.Single(e => e.Id == id);
            _exerciseService.RemoveItem(itemToRemove);
        }


        public void ViewExerciseDetails()
        {
            Console.WriteLine("Please enter id for exercise you want to show/add");

            ShowAllExercises();

            int.TryParse(Console.ReadLine(), out var id);

            var exerciseToShow = _exerciseService.GetItem(id);

            Console.WriteLine($"Exercise id: {exerciseToShow.Id}");
            Console.WriteLine($"Exercise name: {exerciseToShow.Name}");
            Console.WriteLine($"Exercise type id: {exerciseToShow.TypeId}");
        }

        public void ShowAllExercises()
        {
            _exerciseService.Items.ForEach(exercise => Console.WriteLine($"{exercise.Id} - {exercise.Name}"));
        }

        public void ViewExercisesByTypeId()
        {
            Console.WriteLine("Please enter Type id for exercise you want to show:");

            int.TryParse(Console.ReadKey().KeyChar.ToString(), out var typeId);

            var exercisesToShow = _exerciseService.GetAllItems().FindAll(e => e.TypeId == typeId);

            foreach (var exerciseToShow in exercisesToShow)
            {
                Console.WriteLine($"\n{exerciseToShow.Id} - {exerciseToShow.Name}");
            }
        }

        public Exercise GetExerciseById()
        {
            Console.WriteLine("Please enter exercise id you want to add");

            int.TryParse(Console.ReadKey().KeyChar.ToString(), out var id);

            return _exerciseService.GetItem(id);
        }
    }
}