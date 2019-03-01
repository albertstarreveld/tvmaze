using System.Collections.Generic;

namespace Rtl.Data.TvMaze.Proxy
{
    public class Show
    {
        public int id { get; set; }

        public string name { get; set; }

        public IEnumerable<Character> Cast { get; set; }
    }
}