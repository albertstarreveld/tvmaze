using System;

namespace Rtl.Domain
{
    public class Actor
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public DateTime? Birthdate { get; private set; }

        public Actor(int id, string name, DateTime? birthdate)
        {
            Id = id;
            Name = name;
            Birthdate = birthdate;
        }
    }
}