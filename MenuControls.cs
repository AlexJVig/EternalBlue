using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EternalBlue
{
    public class MenuControls
    {
        public Experience MatchCriteria { get; set; }

        public Services svc { get; set; }

        public List<Candidate> Candidates { get; set; }

        public List<Technology> Technologies { get; set; }

        public List<Candidate> Filtered { get; set; }

        public List<Candidate> ChosenOnes { get; set; }

        public List<Candidate> Rejects { get; set; }

        public Candidate Current { get; set; }

        public bool SwipingOn { get; set; } = false;

        public void Launch()
        {
            Core();
        }

        public void Core()
        {
            bool loop = true;

            PrintMainMenu();

            while (loop)
            {
                if (SwipingOn)
                    PrintCandidate();

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.T:
                        ChangeTechnology();
                        break;
                    case ConsoleKey.Y:
                        ChangeYearsOfExperience();
                        break;
                    case ConsoleKey.S:
                        StartSwiping();
                        break;
                    case ConsoleKey.RightArrow:
                        SwipeRight();
                        break;
                    case ConsoleKey.LeftArrow:
                        SwipeLeft();
                        break;
                    case ConsoleKey.D:
                        DisplayChosenOnes();
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

        private void DisplayChosenOnes()
        {
            SwipingOn = false;
            PrintMainMenu();

            if (ChosenOnes.Count == 0)
                Console.WriteLine("--- No candidates were chosen ---");
            else
            {
                Console.WriteLine(@"
The candidstes whom were chosen:
");
                foreach (var chosen in ChosenOnes)
                    Console.WriteLine(@"
{0}, {1}
Can swim: {2}
---",
                    chosen.FullName,
                    chosen.Gender,
                    chosen.CanSwim);
            }

        }

        private void SwipeRight()
        {
            ChosenOnes.Add(Current);
            StartSwiping();
        }

        private void SwipeLeft()
        {
            Rejects.Add(Current);
            StartSwiping();
        }

        private void PrintCandidate()
        {
            PrintMainMenu();

            if (Filtered.Count > 0)
            {
                Current = Filtered.First();

                Console.WriteLine(@"
{0}, {1}
Years of experience: {2}
Can swim: {3}
---",
                Current.FullName,
                Current.Gender,
                Current.Experience.FirstOrDefault(e => e.TechnologyId == MatchCriteria.TechnologyId).YearsOfExperience,
                Current.CanSwim);
            }
            else
            {
                Console.WriteLine("--- Out of candidates ---");
            }
        }

        private void StartSwiping()
        {
            Filtered = Candidates.Except(ChosenOnes).Except(Rejects)
                .Where(c =>
                    c.Experience.All(e =>
                        e.TechnologyId == MatchCriteria.TechnologyId &&
                        e.YearsOfExperience >= MatchCriteria.YearsOfExperience))
                .ToList();

            SwipingOn = true;
            PrintMainMenu();
        }

        private void ChangeTechnology()
        {
            for (int i = 0; i < Technologies.Count; i++)
                Console.WriteLine("{0}. {1}", i + 1, Technologies[i].Name);

            Console.Write("Choose a technology by its number: ");

            string techNumStr = Console.ReadLine();

            int.TryParse(techNumStr, out int techNum);

            if (techNum <= 0)
            {
                PrintMainMenu();
                Console.WriteLine("Must type a positive integer!");
            }
            else
            {
                techNum -= 1;
                MatchCriteria.TechnologyId = Technologies[techNum].Guid;
                PrintMainMenu();
            }
        }

        private void ChangeYearsOfExperience()
        {
            Console.Write("How many years of experience? ");

            string years = Console.ReadLine();

            int.TryParse(years, out int yearsOfExperience);

            if (yearsOfExperience <= 0)
            {
                PrintMainMenu();
                Console.WriteLine("Must type a positive integer!");
            }
            else
            {
                MatchCriteria.YearsOfExperience = yearsOfExperience;
                PrintMainMenu();
            }
        }

        private void PrintMainMenu()
        {
            Console.Clear();

            string techName = Technologies.Where(t => t.Guid == MatchCriteria.TechnologyId).FirstOrDefault().Name;

            Console.WriteLine(
                        $@"Match criteria: [t]echnology: { techName } [y]ears of experience: { MatchCriteria.YearsOfExperience }.
[S]tart swiping! [Q]uit. To swipe: [<-] & [->]. [D]isplay chosen candidates."
                        );
        }

        private async Task Init()
        {
            Candidates = await svc.GetCandidates();
            Technologies = await svc.GetTechnologies();

            Technology tech = Technologies.FirstOrDefault();

            MatchCriteria = new Experience()
            {
                TechnologyId = tech.Guid,
                YearsOfExperience = 0
            };
        }

        public MenuControls()
        {
            svc = new Services();
            ChosenOnes = new List<Candidate>();
            Rejects = new List<Candidate>();

            Task.WaitAll(Init());
        }
    }
}
