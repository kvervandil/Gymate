using System;
using System.Collections.Generic;
using System.Text;

namespace Gymate
{
    public class InformationProvider
    {
        public virtual int GetNumericInputKey()
        {
            int key;

            int.TryParse(Console.ReadKey().KeyChar.ToString(), out key);

            return key;
        }

        public virtual string GetInputString()
        {
            return Console.ReadLine();
        }

        public virtual void ShowMultipleInformation(List<string> informationList)
        {
            informationList.ForEach(i => Console.WriteLine(i));
        }

        public virtual void ShowSingleMessage(string message)
        {
            Console.WriteLine(message);
        }

        public virtual int GetNumericValue()
        {
            int value;

            int.TryParse(Console.ReadLine(), out value);

            return value;
        }
    }
}
