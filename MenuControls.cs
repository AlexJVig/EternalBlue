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

        public Candidate Current { get; set; }

        public bool SwipingOn { get; set; } = false;

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

        private void PrintCandidate()
        {
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

                Filtered.Remove(Current);
            }
            else
            {
                Console.WriteLine("--- Out of candidates ---");
            }
        }

        private void StartSwiping()
        {
            Filtered = Candidates.Where(c =>
                c.Experience.All(e =>
                e.TechnologyId == MatchCriteria.TechnologyId && e.YearsOfExperience >= MatchCriteria.YearsOfExperience))
                .ToList();

            SwipingOn = true;
        }

        private void ChangeTechnology()
        {
            for (int i = 0; i < Technologies.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, Technologies[i].Name);
            }
            Console.Write("Choose a technology by its number: ");

            int techNum = int.Parse(Console.ReadLine()) - 1;

            MatchCriteria.TechnologyId = Technologies[techNum].Guid;
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
                        $@"Match criteria: [t]echnology: { techName } [y]ears of experience: { MatchCriteria.YearsOfExperience }.
[S]tart swiping! [Q]uit. To swipe: [<-] & [->]"
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

            Task.WaitAll(Init());
        }
    }
}
