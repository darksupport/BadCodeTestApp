using BadCodeTestApp.Commands;
using BadCodeTestApp.Commands.FileCommands;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BadCodeTestApp
{
    class Program
    {
        private static List<ICommand> Commands = new List<ICommand>();

        static Program()
        {
            Register();
        }

        static void Register()
        {
            Commands.Add(new Search());
            Commands.Add(new SearchByExt());
            Commands.Add(new Delete());
            Commands.Add(new Create());
        }
//возможно стоит вынести регистрацию компонетов в отдельный клас. И там создать механизм который это будет делать более независимо
        public static void Main(string[] args)
        {
            string command = args[0];
            string param = args[1];
            foreach (ICommand n in Commands)
            {
                if (Regex.IsMatch(command, n.GetCommandPattern(), RegexOptions.IgnoreCase))
                    //а что если у нас команда попадет под несколько паттернов? 
                    //возможно стоит сделать отдельную операцию валидации или создать класс валидатор с набором правил.
                    //как насчет использования фабрики?
                {
                    n.Execute(command, param); //я думаю есть смысл вообще отказатся от параметров, а передавать все в конструктор класа
                    break;
                }
            }

            Console.ReadLine();
        }
    }
}
