using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalBlue
{
    public class MenuControls
    {
        public Experience MatchCriteria { get; set; }

        public Services svc { get; set; }

        public List<Candidate> Candidates { get; set; }

        public List<Technology> Technologies { get; set; }

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

            MatchCriteria.YearsOfExperience = int.Parse(Console.ReadLine());
        }

        private void PrintMainMenu()
        {
            Console.Clear();

            string techName = Technologies.Where(t => t.Guid == MatchCriteria.TechnologyId).FirstOrDefault().Name;

            Console.WriteLine(
                        $"Match criteria: [t]echnology: { techName } [y]ears of experience: { MatchCriteria.YearsOfExperience }."
                        + Environment.NewLine
                        + "[S]earch [Q]uit. To swipe: [<-] & [->]"
                        );
        }

        private async void Init()
        {
            Candidates = await svc.GetCandidates();
            Technologies = await svc.GetTechnologies();
        }

        public MenuControls()
        {
            svc = new Services();

            Init();
        }
    }
}
