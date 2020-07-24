using System;
using System.Collections.Generic;

using MenuGenerator;

namespace menu_demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var coffeOptions = new List<Option>
            {
                new Option { Id = 1 , Value = "Neagra" },
                new Option { Id = 2, Value = "Espresso"},
                new Option { Id = 3, Value = "Ice coffe"},
                new Option { Id = 4, Value = "Latte"},
            };

            var coffeMenu = new MenuInteraction(SelectionType.SingleSelection, coffeOptions);
            var coffeSelection = coffeMenu.GetSelection();
            
            var extraOptions = new List<Option>
            {
                new Option { Id = 1 , Value = "Lapte" },
                new Option { Id = 2, Value = "Indulcitor artificial"},
                new Option { Id = 3, Value = "Servetel"},
                new Option { Id = 4, Value = "Lingurita"},
                new Option { Id = 5, Value = "Zahar brun"},
                new Option { Id = 6, Value = "Zahar alb"},
            };
            var extraMenu = new MenuInteraction(SelectionType.MultipleSelection, extraOptions);
            var extraSelection = extraMenu.GetSelection();

        }
    }
}
