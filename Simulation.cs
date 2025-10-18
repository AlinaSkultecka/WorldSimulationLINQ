using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldSimulation
{
    public class Simulation
    {
        List<HumanProperties> Society = new List<HumanProperties> { };
        
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

        
        
        HumanProperties GetRandomFemale(List<HumanProperties> Society)
        {
            Random rand = new Random();
            var females = Society.Where(h => h.Gender == Gender.Female && h.IsAlive && h.Age >= 18 && h.Age <= 50).ToList();

            if (females.Count == 0) return null;

            return females[rand.Next(females.Count)];
        }

        HumanProperties GetRandomMale(List<HumanProperties> Society)
        {
            Random rand = new Random();
            var males = Society.Where(h => h.Gender == Gender.Male && h.IsAlive && h.Age >= 18 && h.Age <= 50).ToList();

            if (males.Count == 0) return null;

            return males[rand.Next(males.Count)];
        }

        
        
        
        public void GenerateOneGeneration()
        {
            while (Society.Count(h => h.Age >= 18 && h.Age <= 50) < 2)
            {
                Society.Add(HumanProperties.HumanGenerator());
            }
            Console.WriteLine("\n--- Generating new generation ---");

            Random rand = new Random();
            int childrenCreated = 0;

            for (int i = 0; i < 15; i++)
            {
                var father = GetRandomMale(Society);
                var mother = GetRandomFemale(Society);

                if (father == null || mother == null)
                {
                    Console.WriteLine("Not enough adults to generate a new generation.");
                    break;
                }

                var child = father.MakeChild(mother);
                if (child != null)
                {
                    Society.Add(child);
                    childrenCreated++;
                }
            }

            Console.WriteLine($"\nCreated {childrenCreated} new children.");
            PrintSocietyStats();
        }
    }
}
