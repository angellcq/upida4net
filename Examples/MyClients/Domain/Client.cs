using System.Collections.Generic;
using Upida;

namespace MyClients.Domain
{
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

        [Dto(Levels.GRID, Levels.LOOKUP)]
        public virtual ISet<Login> Logins { get; set; }
    }
}