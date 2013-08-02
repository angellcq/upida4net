using Upida;
using Upida.Validation;
using UpidaExampleStraight.Validation;
using iesi = Iesi.Collections.Generic;

namespace UpidaExampleStraight.Domain
{
    [Fluent(typeof(ClientSaveValidator), Groups.SAVE)]
    public class Client : Dtobase
    {
        [Dto(Rules.ID)]
        public virtual int? Id { get; set; }

        [Dto(Rules.LOOKUP)]
        public virtual string Name { get; set; }

        [Dto(Rules.FULL, Nested=Rules.ID)]
        public virtual iesi.ISet<Order> Orders { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Client)) return false;
            return Util.AreSame(this.Id, (obj as Client).Id);
        }
    }
}