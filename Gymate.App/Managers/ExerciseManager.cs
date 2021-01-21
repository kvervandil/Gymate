using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Gymate.App.Abstract;
using Gymate.App.Concrete;
using Gymate.Domain.Entity;

namespace Gymate.App.Managers
{
    public class ExerciseManager
    {
        private readonly MenuActionService _actionService;
        private IService<Exercise> _exerciseService;
        private InformationProvider _informationProvider;
        private readonly string XmlDestinationPath = @"Gymate.App\Source\exercises.xml";

        public ExerciseManager(MenuActionService actionService, IService<Exercise> exerciseService, InformationProvider informationProvider)
        {
            _exerciseService = exerciseService;
            _actionService = actionService;
            _informationProvider = informationProvider;
        }

        public int AddNewExercise()
        {
            var addNewExerciseMenu = _actionService.GetMenuActionsByMenuName("AddNewExerciseMenu");


            var menuActionsToShow = new List<string>();

            addNewExerciseMenu.ForEach(menuAction => menuActionsToShow.Add($"{menuAction.Id}. {menuAction.Name}"));

            _informationProvider.ShowMultipleInformation(menuActionsToShow);

            bool isValidInput = false;
            int typeId = 0;

            while (!isValidInput)
            {
                _informationProvider.ShowSingleMessage("Please select exercise type:");

                typeId = _informationProvider.GetNumericInputKey();
                                
                isValidInput = typeId != 0 && typeId >= menuActionsToShow.Count;
            }

            isValidInput = false;
            string name = string.Empty;
            while(!isValidInput)
            {
                _informationProvider.ShowSingleMessage("\nPlease insert name for item: ");

                name = _informationProvider.GetInputString();

                isValidInput = !string.IsNullOrWhiteSpace(name);
            }

            var lastId = _exerciseService.GetLastId();

            var exercise = new Exercise(lastId + 1, name, typeId);

            _exerciseService.AddItem(exercise);

            return exercise.Id;
        }

        public void RemoveExercise()
        {
            _informationProvider.ShowSingleMessage("Please enter id for exercise you want to remove");

            ShowAllExercises();

            var id = _informationProvider.GetNumericInputKey();

            var itemToRemove = _exerciseService.Items.Find(i => i.Id == id);

            if (itemToRemove != null)
            {
                _exerciseService.RemoveItem(itemToRemove);
            }
        }


        public void ViewExerciseDetails()
        {
            _informationProvider.ShowSingleMessage("Please enter id for exercise you want to show/add");

            ShowAllExercises();

            var id = _informationProvider.GetNumericValue();

            var exerciseToShow = _exerciseService.GetItem(id);

            if (exerciseToShow != null)
            {
                List<string> informationToShowList = new List<string>();

                informationToShowList.Add($"Exercise id: {exerciseToShow.Id}");
                informationToShowList.Add($"Exercise name: {exerciseToShow.Name}");
                informationToShowList.Add($"Exercise type id: {exerciseToShow.TypeId}");

                _informationProvider.ShowMultipleInformation(informationToShowList);
            }
            else
            {
                _informationProvider.ShowSingleMessage("No exercise to show.");
            }
        }

        public void GetAddedExercicesFromFile()
        {
            string path = GenerateFilePath();

            if (!File.Exists(path)) return;

            string xml = File.ReadAllText(path);

            if (string.IsNullOrEmpty(xml)) return;

            StringReader stringReader = new StringReader(xml);

            var xmlSerializer = InitialiseXmlSerializer();

            _exerciseService.Items = (List<Exercise>)xmlSerializer.Deserialize(stringReader);
        }

        public void ExportToXml()
        {
            string path = GenerateFilePath();

             var xmlSerializer = InitialiseXmlSerializer();

            using StreamWriter sw = new StreamWriter(path);

            xmlSerializer.Serialize(sw, _exerciseService.Items);
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

            root.ElementName = "Exercises";

            root.IsNullable = true;

            return new XmlSerializer(typeof(List<Exercise>), root);
        }

        public void ShowAllExercises()
        {
            var informationToShowList = new List<string>();

            if (_exerciseService.Items.Count != 0)
            {
                _exerciseService.Items.ForEach(exercise => informationToShowList.Add($"{exercise.Id} - {exercise.Name}"));

                _informationProvider.ShowMultipleInformation(informationToShowList);
            }
            else
            {
                _informationProvider.ShowSingleMessage("No exercises added.");
            }
        }

        public void ViewExercisesByTypeId()
        {
            _informationProvider.ShowSingleMessage("Please enter Type id for exercise you want to show:");

            var typeId = _informationProvider.GetNumericInputKey();

            var exercisesToShow = _exerciseService.GetAllItems().FindAll(e => e.TypeId == typeId);

            if (exercisesToShow.Count != 0)
            {
                var informationToShowList = new List<string>();

                exercisesToShow.ForEach(exercise => informationToShowList.Add($"\n{exercise.Id} - {exercise.Name}"));

                _informationProvider.ShowMultipleInformation(informationToShowList);
            }
            else
            {
                _informationProvider.ShowSingleMessage("No exercise to show.");
            }
        }

        public Exercise GetExerciseById()
        {
            _informationProvider.ShowSingleMessage("Please enter exercise id you want to add");

            var id = _informationProvider.GetNumericInputKey();

            return _exerciseService.GetItem(id);
        }
         
        public void UpdateVolumeInExercise()
        {
            ShowAllExercises();

            _informationProvider.ShowSingleMessage("Type id of exercise you want to update: ");

            var id = _informationProvider.GetNumericInputKey();

            var exerciseToUpdate = _exerciseService.GetItem(id);

            if (exerciseToUpdate == null)
            {
                _informationProvider.ShowSingleMessage("Exercise not found");
                return;
            }

            _informationProvider.ShowSingleMessage("How many sets?");

            var sets = _informationProvider.GetNumericValue();

            _informationProvider.ShowSingleMessage("How many reps?");

            var reps = _informationProvider.GetNumericValue();

            _informationProvider.ShowSingleMessage("How much load?");

            var load = _informationProvider.GetNumericValue();

            exerciseToUpdate.Sets = sets;
            exerciseToUpdate.Reps = reps;
            exerciseToUpdate.Load = load;
        }
    }
}