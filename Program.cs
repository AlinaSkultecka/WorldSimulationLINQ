namespace WorldSimulation
{
    internal class Program
    {
        static List<HumanProperties> Society = new List<HumanProperties>();
        static void Main(string[] args)
        {
            HumanProperties adam = new HumanProperties("Adam", Gender.Male, "Garden of Eden", true);
            HumanProperties eve = new HumanProperties("Eva", Gender.Female, "Garden of Eden", true);

            List<HumanProperties> Society = new List<HumanProperties> { adam, eve };
            
            adam.Age = 35;
            eve.Age = 33;

            Console.WriteLine("The beginning of himanity ...");

            Console.WriteLine($"In the world live {HumanProperties.PopulationCounter} people, it is:");
            foreach (HumanProperties human in Society)
                Console.WriteLine(human);

            Console.WriteLine($"\nThen {adam.Name} and {eve.Name} have children:");
            // Children of Adam and Eve
            for (int i = 0; i < 4; i++)
                Society.Add(adam.MakeChild(eve));

            Console.WriteLine($"{Society[2].Name} got older by 10 years");
            // Age up one of the children
            for (int i = 0; i < 10; i++)
                Society[2].GetOlder();

            Console.WriteLine("\nNew 4 people were found in the world");
            // Add 4 new humans to society
            Society.Add(new HumanProperties("Noah", Gender.Male, "Mesopotamia", true) { Age = 40 });
            Society.Add(new HumanProperties("Abraham", Gender.Male, "Ur", true) { Age = 50 });
            Society.Add(new HumanProperties("Sarah", Gender.Female, "Ur", true) { Age = 45 });
            Society.Add(new HumanProperties("Isaac", Gender.Male, "Canaan", true) { Age = 25 });

            Console.WriteLine("\nNew 10 people were created in the world");
            // Add 10 randome humans to society
            var newHuman = new List<HumanProperties>();
            for (int i = 0; i < 6; i++)
                Society.Add(HumanProperties.HumanGenerator());

            // Display population and details
            Console.WriteLine($"\nThe curent population is: {HumanProperties.PopulationCounter}");
            foreach (HumanProperties human in Society)
                Console.WriteLine(human);





            // Create a Simulation object
            Simulation sim = new Simulation();
            sim.PrintRandomMale();

            //sim.GenerateOneGeneration();

            //Console.WriteLine("\nFive years passed since the world was created, new people were born, and some died");

            ////  Run multiple years
            //for (int year = 1; year <= 5; year++)
            //{
            //    Console.WriteLine($"--- YEAR {year} ---");
            //    sim.SimulateYear();
            //}



            //// List all female humans, LINQ
            //var females = Society.Where(f => f.Gender == Gender.Female).ToList();
            //Console.WriteLine("\nOnly females in the society:");
            //foreach (HumanProperties female in females)
            //    Console.WriteLine(female);


            //// List all males older than 20, LINQ
            //var maleOver20 = Society
            //    .Where(m => m.Gender == Gender.Male).ToList()
            //    .Where(m => m.Age >= 20);
            //Console.WriteLine("\nOnly male over 20 y.o. in the society");
            //foreach (HumanProperties male in maleOver20)
            //    Console.WriteLine(male);


            //// Counting how many children Adam and Eve have
            //var childrenOfAdamEve = Society
            //    .Where(c => c.HomeLocation == "Garden of Eden")
            //    .Where(c => c.Age < 20);
            //Console.WriteLine("\nChildren of Adam and Eve are:");
            //foreach(HumanProperties child in childrenOfAdamEve)
            //    Console.WriteLine(child);


            //// Print the oldest human
            //var oldestHuman = Society
            //    .OrderByDescending(o => o.Age)
            //    .First();
            //Console.WriteLine($"\nThe oldest human is: {oldestHuman}");


            //// Make all humans older
            //foreach(HumanProperties human in Society)
            //    human.GetOlder();


            //// Print average age 
            //var averageAge = Society.Average(h => h.Age);
            //Console.WriteLine("The average age of all people is: " + Math.Round(averageAge).ToString());

            //// To kill one human
            //Console.WriteLine("\n--- A tragic event occurs ---");
            //Society[5].Die();

            //// Make Adam hungry and feed him
            //Society[0].HungerLevel = 50;
            //Society[0].EnergyLevel = 60;
            //Society[0].Happiness = 70;
            //Society[0].Eat();

            //// Sort humans by age
            //var sortedHumans = Society.OrderByDescending(h => h.Age).ToList();
            //Console.WriteLine("\nSorted population by age:");
            //foreach (HumanProperties human in sortedHumans)
            //    Console.WriteLine(human);

            //// Group humans by gender
            //var genderGroups = Society.GroupBy(h => h.Gender);
            //foreach (var group in genderGroups)
            //{
            //    Console.WriteLine($"\nGender: {group.Key}");
            //    foreach (HumanProperties human in group)
            //    {
            //        Console.WriteLine(human);
            //    }
            //}




        }
    }
}
