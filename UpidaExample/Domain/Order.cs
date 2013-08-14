using System;
using Upida;
using Upida.Validation;
using UpidaExample.Validation;
using iesi = Iesi.Collections.Generic;

namespace UpidaExample.Domain
{
    [ValidateWith(typeof(OrderSaveValidator), Groups.SAVE)]
    [ValidateWith(typeof(OrderUpdateValidator), Groups.UPDATE)]
    [ValidateWith(typeof(OrderUpdateItemsValidator), Groups.UPDATE_A)]
    public class Order : Dtobase
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

        [Dto(Levels.FULL, Nested = Levels.LOOKUP)]
        public virtual iesi.ISet<OrderItem> OrderItems { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Order)) return false;
            return Util.AreSame(this.Id, (obj as Order).Id);
        }
    }
}