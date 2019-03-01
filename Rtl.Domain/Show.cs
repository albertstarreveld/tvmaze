using System.Collections.Generic;

namespace Rtl.Domain
{
    public class Show
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public IEnumerable<Actor> Cast { get; private set; }

        public Show(int id, string name, IEnumerable<Actor> cast)
        {
            Id = id;
            Name = name;
            Cast = cast;
        }
    }
}