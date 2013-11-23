using Upida;
using Upida.Validation;
using UpidaExampleAngular.Validation;

namespace UpidaExampleAngular.Domain
{
	[ValidateWith(typeof(OrderItemSaveValidator), Groups.SAVE)]
	[ValidateWith(typeof(OrderItemMergeValidator), Groups.MERGE)]
	public class OrderItem : Dtobase, IChild
	{
		[Dto(Levels.ID)]
		public virtual int? Id { get; set; }

		[Dto(Levels.LOOKUP)]
		public virtual int? ProductId { get; set; }

		[Dto(Levels.LOOKUP)]
		public virtual int? Count { get; set; }

		[Dto(Levels.LOOKUP)]
		public virtual float? Price { get; set; }

		[Dto(Levels.FULL, Nested = Levels.ID)]
		public virtual Order Order { get; set; }

		public override bool Equals(object obj)
		{
			if (!(obj is OrderItem)) return false;
			return Util.AreSame(this.Id, (obj as OrderItem).Id);
		}

		public virtual void ConnectToParent(object parent)
		{
			if (parent is Order)
			{
				this.Order = (Order)parent;
			}
		}
	}
}