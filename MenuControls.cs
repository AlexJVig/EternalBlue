using System;
namespace EternalBlue
{
    public class MenuControls
    {
        private const string all = "<All>";

        public MatchCriteria mc { get; set; }

        public void Launch()
        {
            Core();
        }

        public void Core()
        {
            bool loop = true;

            while (loop)
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
                    case ConsoleKey.S:
                        Search();
                        break;
                    case ConsoleKey.RightArrow:
                        SwipeRight();
                        break;
                    case ConsoleKey.LeftArrow:
                        SwipeLeft();
                        break;
                    case ConsoleKey.Q:
                        loop = false;
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine("--- Application aborted. ---");
        }

        private void SwipeRight()
        {
            Console.WriteLine("Implement SwipeRight");
        }

        private void SwipeLeft()
        {
            Console.WriteLine("Implement SwipeLeft");
        }

        private void Search()
        {
            Console.WriteLine("Implement Search");
        }

        private void ChangeTechnology()
        {
            Console.WriteLine("Implement ChangeTechnology");
        }

        private void ChangeYearsOfExperience()
        {
            Console.Write("How many years of experience? ");

            mc.YearsOfExperience = int.Parse(Console.ReadLine());
        }

        private void PrintMainMenu()
        {
            Console.Clear();

            string selectedTechnology = mc.Technology == string.Empty ? all : mc.Technology;

            Console.WriteLine(
                        $"Match criteria: [t]echnology: { selectedTechnology } [y]ears of experience: { mc.YearsOfExperience }."
                        + Environment.NewLine
                        + "[S]earch [Q]uit. To swipe: [<-] & [->]"
                        );
        }

        public MenuControls()
        {
            mc = new MatchCriteria();
        }
    }
}
