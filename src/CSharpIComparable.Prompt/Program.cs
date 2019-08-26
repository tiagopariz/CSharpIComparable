using System;
using System.Collections.Generic;
using CSharpIComparable.Domain;

namespace CSharpIComparable.Prompt
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a Dog Colection
            var dogs = new List<Dog>()
            {
                new Dog(Guid.NewGuid(), "Doberman"),
                new Dog(Guid.NewGuid(), "Poodle"),
                new Dog(Guid.NewGuid(), "Bulldog"),
                new Dog(Guid.NewGuid(), "Sheltie"),
                new Dog(Guid.NewGuid(), "Beagle")                
            };

            // Show in the prompt
            Console.WriteLine("Unordered Dog list\n");
            foreach (var dog in dogs)
            {
                Console.WriteLine(dog.Name);
            }

            // Sort the list
            dogs.Sort();

            // Show sorted list in the prompt
            Console.WriteLine("\nOrdered Dog list\n");
            dogs.ForEach(Console.WriteLine);

            // Comparing objects
            var dog1 = new Dog(Guid.NewGuid(), "Doberman");
            var dog2 = new Dog(Guid.NewGuid(), "Bulldog");
            var dog3 = new Dog(Guid.NewGuid(), "Beagle");
            var dog4 = new Dog(Guid.NewGuid(), "Doberman");

            Console.WriteLine("\nComparing objects\n");
            Console.WriteLine($"{dog1}.CompareTo({dog2}) : {(dog1.CompareTo(dog2))}" );
            Console.WriteLine($"{dog2}.CompareTo({dog3}) : {(dog2.CompareTo(dog3))}" );
            Console.WriteLine($"{dog3}.CompareTo({dog1}) : {(dog3.CompareTo(dog1))}" );   
            Console.WriteLine($"{dog1}.CompareTo({dog4}) : {(dog1.CompareTo(dog4))}" );
        }
    }
}