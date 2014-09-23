using System.Collections.Generic;
using Upida;
using Upida.Validation;
using UpidaExampleKnockout.Validation;

namespace UpidaExampleKnockout.Domain
{
	[ValidateWith(typeof(ClientSaveValidator), Groups.SAVE)]
	[ValidateWith(typeof(ClientReferenceValidator), Groups.REFERENCE)]
	public class Client : Dtobase
	{
		[Dto(Levels.ID)]
		public virtual int? Id { get; set; }

		[Dto(Levels.LOOKUP)]
		public virtual string Name { get; set; }

		[Dto(Levels.FULL, Nested = Levels.ID)]
		public virtual ISet<Order> Orders { get; set; }

		public override bool Equals(object obj)
		{
			if (!(obj is Client)) return false;
			return Util.AreSame(this.Id, (obj as Client).Id);
		}
	}
}