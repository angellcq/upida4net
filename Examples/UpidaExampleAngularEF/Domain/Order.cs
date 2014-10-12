using System;
using System.Collections.Generic;
using Upida;
using Upida.Validation;
using UpidaExampleAngularEF.Validation;

namespace UpidaExampleAngularEF.Domain
{
	[ValidateWith(typeof(OrderSaveValidator), Groups.SAVE)]
	[ValidateWith(typeof(OrderUpdateValidator), Groups.UPDATE)]
	[ValidateWith(typeof(OrderUpdateItemsValidator), Groups.UPDATE_A)]
	public partial class Order : Dtobase
	{
		[Dto(Levels.ID)]
		public virtual int? Id { get; set; }

		[Dto(Levels.GRID)]
		public virtual DateTime? CreatedOn { get; set; }

		[Dto(Levels.DEEP, Nested = Levels.LOOKUP)]
		public virtual Client Client { get; set; }

		[Dto(Levels.GRID)]
		public virtual string ShipCountry { get; set; }

		[Dto(Levels.GRID)]
		public virtual string ShipCity { get; set; }

		[Dto(Levels.GRID)]
		public virtual string ShipZip { get; set; }

		[Dto(Levels.GRID)]
		public virtual string ShipAddress { get; set; }

		[Dto(Levels.GRID)]
		public virtual float? Total { get; set; }

		[Dto(Levels.NEVER)]
		public Nullable<int> Client_Id { get; set; }

		[Dto(Levels.FULL, Nested = Levels.LOOKUP)]
		public virtual ICollection<OrderItem> OrderItems { get; set; }
	}
}