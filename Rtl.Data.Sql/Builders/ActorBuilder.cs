using System;
using Rtl.Domain;

namespace Rtl.Data.Sql.Builders
{
    internal class ActorBuilder
    {
        private int _id;
        private string _name;
        private DateTime? _birthDate;

        public ActorBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public ActorBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ActorBuilder WithBirthDate(DateTime? birthdate)
        {
            _birthDate = birthdate;
            return this;
        }

        public Actor Build()
        {
            return new Actor(_id, _name, _birthDate);
        }
    }
}