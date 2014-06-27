using System.Collections.Generic;
using Upida;
using Upida.Validation;
using UpidaExampleAngularEF.Validation;

namespace UpidaExampleAngularEF.Domain
{
	[ValidateWith(typeof(ClientSaveValidator), Groups.SAVE)]
	[ValidateWith(typeof(ClientReferenceValidator), Groups.REFERENCE)]
	public class Client : Dtobase
	{
		public Client()
		{
			this.Orders = new HashSet<Order>();
		}

		[Dto(Levels.ID)]
		public virtual int? Id { get; set; }

		[Dto(Levels.LOOKUP)]
		public virtual string Name { get; set; }

		[Dto(Levels.FULL, Nested = Levels.ID)]
		public virtual ICollection<Order> Orders { get; set; }

		public override bool Equals(object obj)
		{
			if (!(obj is Client)) return false;
			return Util.AreSame(this.Id, (obj as Client).Id);
		}
	}
}