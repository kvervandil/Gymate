using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Gymate.App.Abstract;
using Gymate.Domain.Entity;

namespace Gymate.App.Managers
{
    public class RoutineManager
    {
        private IService<Routine> _routineService;
        private InformationProvider _informationProvider;
        private readonly string XmlDestinationPath = @"Gymate.App\Source\routine.xml";

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

        public void GetAddedRoutineFromFile()
        {
            string path = GenerateFilePath();

            if (!File.Exists(path)) return;

            string xml = File.ReadAllText(path);

            if (string.IsNullOrEmpty(xml)) return;

            StringReader stringReader = new StringReader(xml);

            var xmlSerializer = InitialiseXmlSerializer();

            _routineService.Items = (List<Routine>)xmlSerializer.Deserialize(stringReader);
        }

        public void ExportToXml()
        {
            string path = GenerateFilePath();

            var xmlSerializer = InitialiseXmlSerializer();

            using StreamWriter sw = new StreamWriter(path);

            xmlSerializer.Serialize(sw, _routineService.Items);
        }

        private string GenerateFilePath()
        {
            string path = Directory.GetCurrentDirectory();

            for (int i = 0; i < 5; i++)
            {
                path = Directory.GetParent(path).FullName;
            }

            return Path.Combine(path, XmlDestinationPath);
        }
        
        private XmlSerializer InitialiseXmlSerializer()
        {
            XmlRootAttribute root = new XmlRootAttribute();

            root.ElementName = "Routine";

            root.IsNullable = true;

            return new XmlSerializer(typeof(List<Routine>), root);
        }
    }
}