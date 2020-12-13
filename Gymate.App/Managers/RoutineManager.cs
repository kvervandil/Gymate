using System.Collections.Generic;
using Gymate.App.Abstract;
using Gymate.Domain.Entity;

namespace Gymate.App.Managers
{
    public class RoutineManager
    {
        private IService<Routine> _routineService;
        private InformationProvider _informationProvider;

        public RoutineManager(IService<Routine> routineService, InformationProvider informationProvider)
        {
            _routineService = routineService;
            _informationProvider = informationProvider;
        }

        public int GetRoutineId()
        {
            _informationProvider.ShowSingleMessage("Choose Day of a week: ");

            var informationToShowList = new List<string>();

            _routineService.Items.ForEach(routine => informationToShowList.Add($"{routine.Id} - {routine.Name}"));
            _informationProvider.ShowMultipleInformation(informationToShowList);

            var id = _informationProvider.GetNumericInputKey();

            return id;
        }

        public void AddSelectedExerciseToRoutineDay(int routineDayId, Exercise exercise)
        {
            var routine = _routineService.GetItem(routineDayId);

            if (exercise != null)
            {
                routine.ExercisesOfTheDay.Add(exercise);
            }
            else
            {
                _informationProvider.ShowSingleMessage("Exercise does not exist");
            }
        }

        public void ShowWholeRoutine()
        {
            var routines = _routineService.GetAllItems();

            foreach (var routine in routines)
            {
                _informationProvider.ShowSingleMessage(routine.Name);

                var informationToShowList = new List<string>();

                for (int i = 0; i < routine.ExercisesOfTheDay.Count; i++)
                {
                    var exercise = routine.ExercisesOfTheDay[i];

                    informationToShowList.Add($"{i + 1}. {exercise.Name} - {exercise.Sets} x {exercise.Reps} x {exercise.Load}");
                }

                if (informationToShowList.Count != 0)
                {
                    _informationProvider.ShowMultipleInformation(informationToShowList);
                }
                else
                {
                    _informationProvider.ShowSingleMessage("No exercises added.");
                }

                _informationProvider.ShowSingleMessage("\n");
            }
        }
    }
}