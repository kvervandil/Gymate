using Gymate.App.Abstract;
using Gymate.App.Concrete;
using Gymate.App.Managers;
using Gymate.Domain.Entity;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Gymate.App.Helpers
{
    public class FileManager
    {
        private readonly string RoutineDestinationPath = @"Gymate.App\Source\routine.xml";
        private readonly string ExerciseDestinationPath = @"Gymate.App\Source\exercises.xml";

        public object GetAddedObjectsFromFile(object objectType)
        {
            string path = GenerateFilePath(objectType);

            if (!File.Exists(path)) return null;

            string xml = File.ReadAllText(path);

            if (string.IsNullOrEmpty(xml)) return null;

            StringReader stringReader = new StringReader(xml);
            XmlRootAttribute root = new XmlRootAttribute();
            root.IsNullable = true;

            XmlSerializer xmlSerializer;

            if (objectType is RoutineManager)
            {
                root.ElementName = "Routine";

                xmlSerializer = new XmlSerializer(typeof(List<Routine>), root);
            }
            else
            {
                root.ElementName = "Exercises";

                xmlSerializer = new XmlSerializer(typeof(List<Exercise>), root);

            }
            return xmlSerializer.Deserialize(stringReader);
        }

        public void ExportExercisesToXml(IService<Exercise> service)
        {
            string path = GenerateFilePath(service);

            XmlRootAttribute root = new XmlRootAttribute();
            root.IsNullable = true;     
            root.ElementName = "Exercises";
            var xmlSerializer = new XmlSerializer(typeof(List<Exercise>), root);
            
            using StreamWriter sw = new StreamWriter(path);

            xmlSerializer.Serialize(sw, service.Items);
        }

        public void ExportRoutineToXml(IService<Routine> service)
        {
            string path = GenerateFilePath(service);

            XmlRootAttribute root = new XmlRootAttribute();
            root.IsNullable = true;     
            root.ElementName = "Routine";
            var xmlSerializer = new XmlSerializer(typeof(List<Routine>), root);
            
            using StreamWriter sw = new StreamWriter(path);

            xmlSerializer.Serialize(sw, service.Items);
        }

        private string GenerateFilePath(object objectType)
        {
            string path = Directory.GetCurrentDirectory();

            for (int i = 0; i < 5; i++)
            {
                path = Directory.GetParent(path).FullName;
            }

            return objectType is RoutineManager
                || objectType is RoutineService ? Path.Combine(path, RoutineDestinationPath)
                : Path.Combine(path, ExerciseDestinationPath);
        }
    }
}
