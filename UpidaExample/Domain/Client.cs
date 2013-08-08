using Upida;
using Upida.Validation;
using UpidaExample.Validation;
using iesi = Iesi.Collections.Generic;

namespace UpidaExample.Domain
{
    [Fluent(typeof(ClientSaveValidator), Groups.SAVE)]
    public class Client : Dtobase
    {
        [Dto(Levels.ID)]
        public virtual int? Id { get; set; }

        [Dto(Levels.LOOKUP)]
        public virtual string Name { get; set; }

        [Dto(Levels.FULL, Nested = Levels.ID)]
        public virtual iesi.ISet<Order> Orders { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Client)) return false;
            return Util.AreSame(this.Id, (obj as Client).Id);
        }
    }
}