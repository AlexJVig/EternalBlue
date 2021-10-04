using System;
namespace EternalBlue
{
    public class MenuControls
    {
        public MatchCriteria mc { get; set; }

        public void Launch()
        {
            Core();
        }

        public void Core()
        {
            PrintMainMenu();

            while (Console.ReadKey(true).Key != ConsoleKey.Q)
            {
                PrintMainMenu();

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.T:
                        ChangeTechnology();
                        break;
                    case ConsoleKey.Y:
                        ChangeYearsOfExperience();
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine("--- Application aborted. ---");
        }

        private void ChangeTechnology()
        {
            Console.WriteLine("Implement ChangeTechnology");
        }

        private void ChangeYearsOfExperience()
        {
            Console.WriteLine("Implement ChangeYearsOfExperience");
        }

        public void PrintMainMenu()
        {
            const string all = "<All>";

            string selectedTechnology = mc.Technology == string.Empty ? all : mc.Technology;

            Console.WriteLine(
                        $"Match criteria: [t]echnology: { selectedTechnology } [y]ears of experience: { mc.YearsOfExperience }. [Q]uit"
                        );
        }

        public MenuControls()
        {
            mc = new MatchCriteria();
        }
    }
}
