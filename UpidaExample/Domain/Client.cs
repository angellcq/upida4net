using Upida;
using Upida.Validation;
using UpidaExample.Validation;
using iesi = Iesi.Collections.Generic;

namespace UpidaExample.Domain
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
            return Util.AreSame(this.Id, ((Client)obj).Id);
        }
    }
}