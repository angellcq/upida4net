using Upida;
using Upida.Validation;
using MyClients.Validation;
using iesi = Iesi.Collections.Generic;

namespace MyClients.Domain
{
    [ValidateWith(typeof(ClientSaveValidator), Groups.SAVE)]
    public class Client : Dtobase
    {
        [Dto(Levels.ID)]
        public virtual int? Id { get; set; }

        [Dto(Levels.LOOKUP)]
        public virtual string Name { get; set; }

        [Dto(Levels.LOOKUP)]
        public virtual string Lastname { get; set; }

        [Dto(Levels.GRID)]
        public virtual int? Age { get; set; }

        [Dto(Levels.FULL, Nested = Levels.ID)]
        public virtual iesi.ISet<Login> Logins { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Client)) return false;
            return Util.AreSame(this.Id, (obj as Client).Id);
        }
    }
}