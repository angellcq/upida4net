using iesi = Iesi.Collections.Generic;
using System;
using Upida;
using Upida.Validation;
using UpidaExample.Validation;

namespace UpidaExample.Domain
{
    [Fluent(typeof(OrderSaveValidator), Groups.SAVE)]
    [Fluent(typeof(OrderUpdateValidator), Groups.UPDATE)]
    [Fluent(typeof(OrderUpdateItemsValidator), Groups.UPDATE_ITEMS)]
    public class Order : Dtobase
    {
        [Dto(Rules.ID)]
        public virtual int? Id { get; set; }

        [Dto(Rules.GRID)]
        public virtual DateTime? CreatedOn { get; set; }

        [Dto(Rules.DEEP, Nested=Rules.LOOKUP)]
        public virtual Client Client { get; set; }

        [Dto(Rules.GRID)]
        public virtual string ShipCountry { get; set; }

        [Dto(Rules.GRID)]
        public virtual string ShipCity { get; set; }

        [Dto(Rules.GRID)]
        public virtual string ShipZip { get; set; }

        [Dto(Rules.GRID)]
        public virtual string ShipAddress { get; set; }

        [Dto(Rules.GRID)]
        public virtual float? Total { get; set; }

        [Dto(Rules.FULL, Nested=Rules.LOOKUP)]
        public virtual iesi.ISet<OrderItem> OrderItems { get; set; }

        public override bool Equals(object obj)
        {
            bool areEquals = false;
            if (obj is Order)
            {
                areEquals = Util.AreSame(this.Id, ((Order)obj).Id);
            }

            return areEquals;
        }
    }
}