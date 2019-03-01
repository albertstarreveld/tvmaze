using System.Collections.Generic;
using System.Linq;
using Rtl.Domain;

namespace Rtl.Data.Sql.Builders
{
    internal class ShowCollectionBuilder
    {
        private readonly Dictionary<int, string> _shows = new Dictionary<int, string>();
        private readonly List<Actor> _actors = new List<Actor>();
        private readonly Dictionary<int, List<int>> _cast = new Dictionary<int, List<int>>();

        public ShowCollectionBuilder WithShow(int show, string name)
        {
            _shows.Add(show, name);
            return this;
        }

        public ShowCollectionBuilder WithCast(int show, int actor)
        {
            if (!_cast.ContainsKey(show))
            {
                _cast.Add(show, new List<int>());
            }

            _cast[show].Add(actor);
            return this;
        }

        public ShowCollectionBuilder WithActor(Actor actor)
        {
            if (_actors.Any(x => x.Id == actor.Id))
            {
                return this;
            }

            _actors.Add(actor);
            return this;
        }

        public IEnumerable<Show> Build()
        {
            var results = new List<Show>();
            foreach (var keyValuePair in _shows)
            {
                if (!_cast.TryGetValue(keyValuePair.Key, out var actorsIds))
                {
                    actorsIds = new List<int>();
                }

                var actors = _actors
                    .Where(x => actorsIds.Contains(x.Id))
                    .OrderByDescending(x => x.Birthdate);

                var show = new Show(keyValuePair.Key, keyValuePair.Value, actors);
                results.Add(show);
            }

            return results;
        }
    }
}