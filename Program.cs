namespace WorldSimulation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Human adam = new Human("Adam", Gender.Male, "Garden of Eden", true);
            Human eve = new Human("Eva", Gender.Female, "Garden of Eden", true);

            List<Human> Society = new List<Human> { adam, eve };

            Console.WriteLine("The beginning of himanity ...");

            adam.Age = 35;
            eve.Age = 33;

            // Children of Adam and Eve
            for (int i = 0; i < 4; i++)
            {
                Society.Add(adam.MakeChild(eve));
            }

            // Age up one of the children
            for (int i = 0; i < 10; i++)
            {
                Society[2].GetOlder();
            }

            // Add 10 new humans to society
            Society.Add(new Human("Noah", Gender.Male, "Mesopotamia", true) { Age = 40 });
            Society.Add(new Human("Abraham", Gender.Male, "Ur", true) { Age = 50 });
            Society.Add(new Human("Sarah", Gender.Female, "Ur", true) { Age = 45 });
            Society.Add(new Human("Isaac", Gender.Male, "Canaan", true) { Age = 25 });
            Society.Add(new Human("Rebecca", Gender.Female, "Canaan", true) { Age = 23 });
            Society.Add(new Human("Jacob", Gender.Male, "Canaan", true) { Age = 20 });
            Society.Add(new Human("Rachel", Gender.Female, "Canaan", true) { Age = 19 });
            Society.Add(new Human("Joseph", Gender.Male, "Egypt", true) { Age = 17 });
            Society.Add(new Human("Leah", Gender.Female, "Canaan", true) { Age = 22 });
            Society.Add(new Human("Benjamin", Gender.Male, "Egypt", true) { Age = 15 });

            // Display population and details
            Console.WriteLine($"Population: {Human.PopulationCounter}\n");
            foreach (Human human in Society)
            {
                Console.WriteLine(human);
            }

            // List all female humans, LINQ
            var females = Society.Where(f => f.Gender == Gender.Female).ToList();
            Console.WriteLine("\nOnly females in the society:");
            foreach (Human female in females)
            {
                Console.WriteLine(female);
            }

            // List all males older than 20, LINQ
            var maleOver20 = Society
                .Where(m => m.Gender == Gender.Male).ToList()
                .Where(m => m.Age >= 20);
            Console.WriteLine("\nOnly male over 20 y.o. in the society");
            foreach (Human male in maleOver20)
            {
                Console.WriteLine(male);
            }

            // Counting how many children Adam and Eve have
            var childrenOfAdamEve = Society
                .Where(c => c.HomeLocation == "Garden of Eden")
                .Where(c => c.Age < 20);
            Console.WriteLine("\nChildren of Adam and Eve are:");
            foreach(Human child in childrenOfAdamEve)
            {
                Console.WriteLine(child);
            }

            // Print the oldest human
            var oldestHuman = Society
                .OrderByDescending(o => o.Age)
                .First();
            Console.WriteLine($"\nThe oldest human is: {oldestHuman}");

            // 




            //var Saras = Society.Where(h => h.Name == "Sara").ToList();

            //Console.WriteLine(Society[0].ToString());

            //var peopleOver10 = Society.Where(h => h.Age >= 9).ToList();
            //foreach (Human h in peopleOver10)
            //{
            //    Console.WriteLine(h);
            //}


            //Console.WriteLine("Only males in the list");
            //foreach (Human human in Society)
            //{
            //    if(human.Gender == Gender.Male)
            //    {
            //        Console.WriteLine(human);
            //    }
            //}

            //Console.WriteLine("============================");

            //var females = Society.Where(f => f.Gender == Gender.Female).ToList();
            //Console.WriteLine("Only females in the list");
            //foreach (Human female in females)
            //{
            //    Console.WriteLine(female);
            //}

            //List <int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //List<int> evenNumbers = new List<int>();
            //foreach (int number in numbers) 
            //{
            //    if (number % 2 == 0) 
            //    {
            //        evenNumbers.Add(number);
            //    }
            //}

            //var evenNumber2 = numbers.Where(n => n % 2 == 0).ToList();
            //foreach (int number in evenNumber2)
            //{
            //    Console.WriteLine(number);
            //}
        }
    }
}
