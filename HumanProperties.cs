using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
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

    public class HumanProperties
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
        public double EnergyLevel { get; set; } = 100.0;
        public double Strength { get; set; } 
        public double Endurance { get; set; }
        public double ImmuneSystemStrength { get; set; }
        public double SleepQuality { get; set; } = 100.0;
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
        public double Happiness { get; set; } = 100.0;
        public double StressLevel { get; set; } = 0.0;
        
        // Social and cultural attributes
        public List<string> SocialConnections { get; set; }
        public List<string> Skills { get; set; }
        public double Reputation { get; set; }
        public double Integrity { get; set; }

        // Economic and survival attributes
        public double Wealth { get; set; } = 100.0;
        public string Occupation { get; set; }
        public string EducationLevel { get; set; }
        public string HomeLocation { get; set; }

        // Existential attributes
        public string Purpose { get; set; }
        public string Religion { get; set; }
        public double FaithLevel { get; set; }
        public double MoralLevel { get; set; }
        public double LegacyLevel { get; set; }

        // Relationship attributes
        public HumanProperties Mother {  get; set; }
        public HumanProperties Father {  get; set; }
        public List<HumanProperties> Children { get; set; } = new List<HumanProperties>();



        public HumanProperties(string name, Gender gender, string birthLocation, bool isAlive)
        {
            Name = name;
            Gender = gender;
            HomeLocation = birthLocation;
            Age = 0;
            Intelligence = 100.0;
            Empathy= 50.0;
            Creativity = 50.0;
            DNA = Guid.NewGuid();
            IsAlive = isAlive;
            PopulationCounter++;
        }

        public HumanProperties MakeChild(HumanProperties partner)
        {
            if (!IsAlive || !partner.IsAlive)
            {
                Console.WriteLine($"{Name} or {partner.Name} cannot have a child because one of them is not alive.");
                return null;
            }

            if (Gender == partner.Gender)
            {
                Console.WriteLine($"{Name} and {partner.Name} cannot have a child together — same gender.");
                return null;
            }

            Random rand = new Random();
            string[] maleName = { "John", "Michael", "David", "James", "Robert", "William", "Mark", "Richard", "Thomas", "Charles" };
            string[] femaleName = { "Mary", "Patricia", "Linda", "Barbara", "Elizabeth", "Jennifer", "Maria", "Susan", "Margaret", "Dorothy", "Sara" };

            Gender childGender;
            string childName;

            if (rand.Next(0, 2) == 0)
            {
                childGender = Gender.Male;
                childName = maleName[rand.Next(maleName.Length)];
            }
            else
            {
                childGender = Gender.Female;
                childName = femaleName[rand.Next(femaleName.Length)];
            }

            HumanProperties child = new HumanProperties(childName, childGender, partner.HomeLocation, true);

            // Assign parents
            if (Gender == Gender.Female)
            {
                child.Mother = this;
                child.Father = partner;
            }
            else
            {
                child.Father = this;
                child.Mother = partner;
            }

            // Add child to both parents' Children lists
            this.Children.Add(child);
            partner.Children.Add(child);

            Console.WriteLine($"{Name} and {partner.Name} had a child named {child.Name} ({child.Gender}) in {child.HomeLocation}.");
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

        public void Die()
        {
            if (IsAlive)
            {
                IsAlive = false;
                PopulationCounter--;
                Console.WriteLine($"{Name} has died. Population decreased to {PopulationCounter}.");
            } 
            else
            {
                Console.WriteLine($"{Name} is already dead.");
            }
        }

        public void Eat()
        {
            if (HungerLevel > 0)
            {
                Random rand = new Random();
                double foodValue = rand.Next(5, 21);

                // Reduce hunger
                HungerLevel -= foodValue;
                if (HungerLevel < 0) HungerLevel = 0;

                // Increase energy and happiness slightly
                EnergyLevel += 5;
                if (EnergyLevel > 100) EnergyLevel = 100;

                Happiness += 5;
                if (Happiness > 100) Happiness = 100;

                Console.WriteLine($"{Name} ate some food. Hunger is now {HungerLevel:F1}, Energy: {EnergyLevel:F1}, Happiness: {Happiness:F1}");
            }
            else
            {
                Console.WriteLine($"{Name} is not hungry right now.");
            }
        }

        public void Sleep()
        {
            if (IsAlive)
            {
                EnergyLevel += 5;
                if (EnergyLevel > 100) EnergyLevel = 100;

                SleepQuality += 5;
                if (SleepQuality > 100) SleepQuality = 100;
                Console.WriteLine($"{Name} slept well. Energy: {EnergyLevel:F1}, SleepQuality: {SleepQuality:F1}");
            }
            else
            {
                Console.WriteLine($"{Name} cannot sleep — they are no longer alive.");
            }
        }

        public void Work()
        {
            if (!IsAlive)
            {
                Console.WriteLine($"{Name} cannot work — they are no longer alive.");
                return;
            }
            else
            {
                Wealth += 5;

                EnergyLevel -= 5;
                if (EnergyLevel < 0) EnergyLevel = 0;

                StressLevel += 5;
                if (StressLevel > 100) StressLevel = 100;

                Console.WriteLine($"{Name} worked. Energy: {EnergyLevel}, Stress: {StressLevel}, Wealth: {Wealth}");

            }
        }

        public void Rest()
        {
            StressLevel -= 5;
            if (StressLevel < 0) StressLevel = 0;

            EnergyLevel += 5;
            if (EnergyLevel > 100) EnergyLevel = 100;

            Happiness += 5;
            if ( Happiness > 100) Happiness = 100;

            Console.WriteLine($"{Name} rested. Energy: {EnergyLevel}, Stress: {StressLevel}, Happiness: {Happiness}");
        }

        public void Speak(HumanProperties other)
        {
            if (!IsAlive)
            {
                Console.WriteLine($"{Name} cannot speak — they are no longer alive.");
                return;
            }

            if (other == null)
            {
                Console.WriteLine($"{Name} tries to speak, but no one is there.");
                return;
            }

            if (!other.IsAlive)
            {
                Console.WriteLine($"{Name} says: 'Farewell, {other.Name}...'");
                return;
            }

            Console.WriteLine($"{Name} says to {other.Name}: 'Hello, {other.Name}!'");
        }

        public void PrintSummary()
        {
            Console.WriteLine($"Profile of {Name}");
            Console.WriteLine("=====================================");
            Console.WriteLine($"Is Alive: {IsAlive}");
            Console.WriteLine($"Gender: {Gender}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Energy: {EnergyLevel}");
            Console.WriteLine($"Happiness: {Happiness}");
            Console.WriteLine($"Stress: {StressLevel}");
            Console.WriteLine($"Wealth: {Wealth}");
            Console.WriteLine("=====================================\n");
        }

        public static HumanProperties HumanGenerator()
        {
            Random rand = new Random();
            string[] maleName = { "Ethan", "Liam", "Noah", "Mason", "Lucas", "Alexander", "Benjamin", "Oliver", "Henry", "Jack"  };
            string[] femaleName = { "Emma", "Olivia", "Ava", "Sophia", "Isabella", "Mia", "Amelia", "Harper", "Evelyn", "Ella"  };
            string[] homeLocation = { "Canaan", "Mesopotamia", "Egypt", "Ur", "Babylon", "Jerusalem", "Athens", "Rome", "Sparta" };

            Gender humanGender;
            string humanName;
            string humanHomeLocation;
            int humanAge;

            if (rand.Next(0, 2) == 0)
            {
                humanGender = Gender.Male;
                humanName = maleName[rand.Next(0, maleName.Length)];
                humanHomeLocation = homeLocation[rand.Next(0, homeLocation.Length)];
                humanAge=rand.Next(0,80);
            }
            else
            {
                humanGender = Gender.Female;
                humanName = femaleName[rand.Next(0, femaleName.Length)];
                humanHomeLocation = homeLocation[rand.Next(0, homeLocation.Length)];
                humanAge = rand.Next(0, 80);
            }

            HumanProperties human = new HumanProperties(humanName, humanGender, humanHomeLocation, true);
            human.Age = humanAge;
            return human;
        }
        

        public void GoToSchool()
        {
            if (!IsAlive)
            {
                Console.WriteLine($"{Name} cannot go to school — they are no longer alive.");
                return;
            }

            // Initialize Skills list if it's null
            if (Skills == null)
                Skills = new List<string>();

            // Add "Education" skill if not already there
            if (!Skills.Contains("Education"))
                Skills.Add("Education");

            // Increase Intelligence
            Intelligence += 5;
            if (Intelligence > 200) Intelligence = 200; // cap at 200 for realism

            Console.WriteLine($"{Name} went to school. Intelligence: {Intelligence}, Skills: {string.Join(", ", Skills)}");
        }

        public void LearnSkill(string skill)
        {
            if (!IsAlive)
            {
                Console.WriteLine($"{Name} cannot learn {skill} — they are no longer alive.");
                return;
            }

            // Check if skill already learned
            if (Skills.Contains(skill))
            {
                Console.WriteLine($"{Name} already knows {skill}.");
                return;
            }

            // Add the new skill
            Skills.Add(skill);

            // Slightly improve creativity
            Creativity += 5;
            if (Creativity > 100) Creativity = 100;

            Console.WriteLine($"{Name} learned a new skill: {skill}. Creativity: {Creativity}");
        }

        
    }
}
