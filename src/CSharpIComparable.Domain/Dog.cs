using System;

namespace CSharpIComparable.Domain
{
    public class Dog : IComparable
    {
        public Dog(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public int CompareTo(object obj)
        {
            return Name.CompareTo((obj as Dog).Name);
        }

        public override string ToString() => Name;
    }
}