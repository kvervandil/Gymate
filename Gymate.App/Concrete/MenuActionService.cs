using System;
using System.Collections.Generic;
using System.Text;
using Gymate.App.Common;
using Gymate.Domain.Entity;

namespace Gymate.App.Concrete
{
    public class MenuActionService : BaseService<MenuAction>
    {
        public MenuActionService()
        {
            Initialize();
        }

        public List<MenuAction> GetMenuActionsByMenuName(string menuName)
        {
            List<MenuAction> result = new List<MenuAction>();

            foreach (var menuAction in Items)
            {
                if (menuAction.MenuName == menuName)
                {
                    result.Add(menuAction);
                }
            }

            return result;
        }

        private void Initialize()
        {
            AddItem(new MenuAction(1, "Add exercise", "Main"));
            AddItem(new MenuAction(2, "Remove exercise", "Main"));
            AddItem(new MenuAction(3, "Show all added exercises", "Main"));
            AddItem(new MenuAction(4, "Show exercise by id", "Main"));
            AddItem(new MenuAction(5, "Filter exercises by type id", "Main"));
            AddItem(new MenuAction(6, "Add exercise to day of a week", "Main"));
            AddItem(new MenuAction(7, "Show your week plan", "Main"));
            AddItem(new MenuAction(8, "Add volume to your exercise", "Main"));
            AddItem(new MenuAction(9, "Export added exercises to xml", "Main"));
            AddItem(new MenuAction(10, "Export routine to xml", "Main"));

            AddItem(new MenuAction(1, "Legs", "AddNewExerciseMenu"));
            AddItem(new MenuAction(2, "Chest", "AddNewExerciseMenu"));
            AddItem(new MenuAction(3, "Shoulders", "AddNewExerciseMenu"));
            AddItem(new MenuAction(4, "Back", "AddNewExerciseMenu"));
            AddItem(new MenuAction(5, "Core", "AddNewExerciseMenu"));
        }
    }
}
