using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldSimulation
{
    public class Simulation
    {
        public List<HumanProperties> Society { get; set; }

        public Simulation(List<HumanProperties> existingSociety)
        {
            Society = existingSociety;
        }

        public void SimulateYear()
        {
            Random rand = new Random();

            foreach (HumanProperties human in Society.ToList()) // .ToList() avoids collection issues
            {
                if (!human.IsAlive) continue;

                human.GetOlder();

                double healthAdjustment = rand.Next(-25, 21);    // -25 to +20
                double happinessAdjustment = rand.Next(-25, 21); // -25 to +20

                human.Health += healthAdjustment;
                if (human.Health > 100) human.Health = 100;
                if (human.Health < 0)
                {
                    human.Health = 0;
                    human.Die();
                }

                human.Happiness += happinessAdjustment;
                if (human.Happiness > 100) human.Happiness = 100;
                if (human.Happiness < 0) human.Happiness = 0;
            }

            // Births (0–2 babies per year)
            int births = rand.Next(0, 3);
            for (int i = 0; i < births; i++)
            {
                HumanProperties newHuman = HumanProperties.HumanGenerator(); // assuming static method
                Society.Add(newHuman);
                Console.WriteLine($"A new human is born: {newHuman.Name} ({newHuman.Gender}) from {newHuman.HomeLocation}");
            }

            // Random deaths (0–1)
            int deaths = rand.Next(0, 2);
            for (int i = 0; i < deaths && Society.Count > 0; i++)
            {
                var randomIndex = rand.Next(Society.Count);
                var unlucky = Society[randomIndex];
                if (unlucky.IsAlive)
                    unlucky.Die();
            }

            Console.WriteLine($"Year complete. Population: {HumanProperties.PopulationCounter}\n");
        }


        
       
        public void PrintSocietyStats()
        {
            double avgAge = Society.Average(h => h.Age);
            double avgHappiness = Society.Average(h => h.Happiness);
            double avgHealth = Society.Average(h => h.Health);

            Console.WriteLine($"\n--- Society Stats ---");
            Console.WriteLine($"Population: {HumanProperties.PopulationCounter}");
            Console.WriteLine($"Average Age: {avgAge:F1}");
            Console.WriteLine($"Average Happiness: {avgHappiness:F1}");
            Console.WriteLine($"Average Health: {avgHealth:F1}\n");
        }





        public HumanProperties GetRandomMale()
        {
            var males = Society
                .Where(m => m.Gender == Gender.Male && m.IsAlive && m.Age >= 18 && m.Age <= 50)
                .ToList();

            if (males.Count == 0)
                return null;

            Random rand = new Random();
            return males[rand.Next(males.Count)];
        }


        public HumanProperties GetRandomFemale()
        {
            var females = Society
                .Where(f => f.Gender == Gender.Female && f.IsAlive && f.Age >= 18 && f.Age <= 50)
                .ToList();

            if (females.Count == 0)
                return null;

            Random rand = new Random();
            return females[rand.Next(females.Count)];
        }



        public void GenerateOneGeneration()
        {
            Console.WriteLine("\n--- New generation ---");

            int childrenCreated = 0;

            for (int i = 0; i < 15; i++)
            {
                var father = GetRandomMale();
                var mother = GetRandomFemale();

                var child = father.MakeChild(mother);

                if (child != null)
                {
                    Society.Add(child);
                    childrenCreated++;
                    Random rand = new Random();
                    int childAge = rand.Next(0, 8);
                }
            }

            Console.WriteLine($"\nCreated {childrenCreated} new children.");
            PrintSocietyStats();
        }
    }
}
