using System;
using System.Collections.Generic;
using System.Linq;

namespace MenuGenerator
{
    public class MenuInteraction
    {
        private readonly SelectionType _selectionType;
        private readonly List<Option> _options;

        public MenuInteraction(SelectionType selectionType, List<Option> options)
        {
            _selectionType = selectionType;
            _options = options;
        }

        public List<Option> GetSelection()
        {
            var selectionList = new List<Option>();

            //TODO: List all options
            Console.WriteLine("Available options:");
            foreach (var option in _options)
            {
                Console.WriteLine($"{option.Id} - {option.Value}.");
            }
    
            if(_selectionType == SelectionType.MultipleSelectionAllowNoSelection || _selectionType == SelectionType.MultipleSelectionAllowNoSelection)
                Console.WriteLine("Enter N - No selection");

            //TODO: Wait for user input
            if (_selectionType == SelectionType.SingleSelection)
                Console.WriteLine("Enter Id of selection:");
            else
                Console.WriteLine("Enter Ids of selection (eg. \"1,4,6\":");

            var selectedIds = GetUserInput();
            while(selectedIds == null)
            {
                selectedIds = GetUserInput();
            }

            foreach (var selectedId in selectedIds)
            {
                if(!int.TryParse(selectedId,out var id))
                    continue;

                var selectedOption = _options.FirstOrDefault(option => option.Id == id);
                if(selectedOption == null)
                    continue;

                selectionList.Add(selectedOption);
            }

            return selectionList;
        }

        private IEnumerable<string> GetUserInput()
        {
            var inputFromUser = Console.ReadLine();
            if (string.IsNullOrEmpty(inputFromUser))
                return null;
            
            if((_selectionType == SelectionType.MultipleSelectionAllowNoSelection || _selectionType == SelectionType.MultipleSelectionAllowNoSelection) &&
               (inputFromUser.Equals("n", StringComparison.CurrentCultureIgnoreCase)))
                return new List<string>();

            if (_selectionType == SelectionType.SingleSelection)
                return new List<string> { inputFromUser };

            return inputFromUser.Contains(',') ? inputFromUser.Split(',') : null;
        }
    }
}
