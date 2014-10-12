using System;
using Upida;
using Upida.Validation;
using UpidaExampleAngularEF.Validation;

namespace UpidaExampleAngularEF.Domain
{
	[ValidateWith(typeof(OrderItemSaveValidator), Groups.SAVE)]
	[ValidateWith(typeof(OrderItemMergeValidator), Groups.MERGE)]
	public partial class OrderItem : Dtobase, IChildEx
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

		[Dto(Levels.NEVER)]
		public Nullable<int> Order_Id { get; set; }

		public override bool Equals(object obj)
		{
			if (!(obj is OrderItem)) return false;
			return Util.AreSame(this.Id, (obj as OrderItem).Id);
		}

		public virtual void ConnectToParent(object parent)
		{
			if (parent is Order)
			{
				Order order = (Order)parent;
				this.Order_Id = order.Id;
			}
		}

		public virtual void DisconnectFromParent()
		{
			this.Order = null;
			this.Order_Id = null;
		}
	}
}