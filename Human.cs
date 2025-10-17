using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldSimulation
{
    public enum Gender
    {
        Male,
        Female,
        Other
    }

    class Human
    {
        public static long PopulationCounter { get; set; } = 0;

        // Core identity attributes
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public Guid DNA { get; set; }
        public bool IsAlive { get; set; }
        
        // Biological and physical attributes
        public double Health { get; set; } = 100.0;
        public double Height { get; set; }
        public double Weight { get; set; }
        public double EnergyLevel { get; set; }
        public double Strength { get; set; }
        public double Endurance { get; set; }
        public double ImmuneSystemStrength { get; set; }
        public double SleepQuality { get; set; }
        public double HungerLevel { get; set; } = 0.0;
        public double ThirstLevel { get; set; } = 0.0;
        public string BloodType { get; set; }

        // Mental and emotional attributes
        public double Intelligence { get; set; }
        public double Empathy { get; set; }
        public double Creativity { get; set; }
        public double MemoryRetention { get; set; }
        public double Focus { get; set; }
        public double Wisdom { get; set; }
        public double Happiness { get; set; }
        public double StressLevel { get; set; }
        
        // Social and cultural attributes
        public List<string> SocialConnections { get; set; }
        public List<string> Skills { get; set; }
        public double Reputation { get; set; }
        public double Integrity { get; set; }

        // Economic and survival attributes
        public double Wealth { get; set; }
        public string Occupation { get; set; }
        public string EducationLevel { get; set; }
        public string HomeLocation { get; set; }

        // Existential attributes
        public string Purpose { get; set; }
        public string Religion { get; set; }
        public double FaithLevel { get; set; }
        public double MoralLevel { get; set; }
        public double LegacyLevel { get; set; }


        public Human(string name, Gender gender, string birthLocation, bool isAlive)
        {
            Name = name;
            Gender = gender;
            HomeLocation = birthLocation;
            Age = 0;
            Intelligence = 100.0;
            Empathy= 50.0;
            Creativity = 50.0;
            DNA = Guid.NewGuid();
            IsAlive = true;
            PopulationCounter++;
        }

        public Human MakeChild(Human partner)
        {
            Random rand = new Random();
            string[] maleName = { "John", "Michael", "David", "James", "Robert", "William", "Mark", "Richard", "Thomas", "Charles" };
            string[] femaleName = { "Mary", "Patricia", "Linda", "Barbara", "Elizabeth", "Jennifer", "Maria", "Susan", "Margaret", "Dorothy", "Sara"};

            Gender childGender;
            string childName;
            if (rand.Next(0, 2) == 0)
            {
                childGender = Gender.Male;
                childName = maleName[rand.Next(0, maleName.Length)];
            }
            else
            {
                childGender = Gender.Female;
                childName = femaleName[rand.Next(0, femaleName.Length)];
            }

            Human child = new Human(childName, childGender, partner.HomeLocation, true);
            return child;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Gender: {Gender}, Age: {Age}, Home: {HomeLocation}";
        }

        public void GetOlder()
        {
            Age++;
        }
    }
}
