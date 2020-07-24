using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

            if (_selectionType == SelectionType.MultipleSelectionAllowNoSelection || _selectionType == SelectionType.MultipleSelectionAllowNoSelection)
                Console.WriteLine("Enter N - No selection");

            //TODO: Wait for user input
            Console.WriteLine(_selectionType == SelectionType.SingleSelection
                ? "Enter Id of selection:"
                : "Enter Ids of selection (eg. \"1,4,6\":");

            var selectedIds = GetUserInput();
        notValid:
            while (selectedIds == null)
            {
                Console.WriteLine(_selectionType == SelectionType.SingleSelection
                    ? "Please introduce a valid selection: "
                    : "Please introduce valid selections (eg. \"1,4,6\":");
                selectedIds = GetUserInput();
            }

            foreach (var selectedId in selectedIds)
            {
                if (!int.TryParse(selectedId, out var id))
                    continue;

                var selectedOption = _options.FirstOrDefault(option => option.Id == id);
                if (selectedOption == null)
                {
                    selectedIds = null;
                    selectionList.Clear();
                    goto notValid; //case in which the options entered were not available(i.e: selected 5 but 5 didn't exist) so need to reselect
                }
                selectionList.Add(selectedOption);
            }
            return selectionList;
        }

        private IEnumerable<string> GetUserInput()
        {
            var inputFromUser = Console.ReadLine();
            if (string.IsNullOrEmpty(inputFromUser))
                return null;
            if ((_selectionType == SelectionType.MultipleSelectionAllowNoSelection || _selectionType == SelectionType.MultipleSelectionAllowNoSelection) &&
               (inputFromUser.Equals("n", StringComparison.CurrentCultureIgnoreCase)))
                return new List<string>();
            if (_selectionType == SelectionType.SingleSelection && Regex.IsMatch(inputFromUser, "[1-9]\\d*"))
                return new List<string> { inputFromUser };
            return (_selectionType == SelectionType.MultipleSelection && Regex.IsMatch(inputFromUser, "([1-9]\\d*[,]?)+")) ? inputFromUser.Split(',') : null;
        }
    }
}
