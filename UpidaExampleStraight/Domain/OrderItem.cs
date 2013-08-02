using Upida;
using Upida.Validation;
using UpidaExampleStraight.Validation;

namespace UpidaExampleStraight.Domain
{
    [Fluent(typeof(OrderItemSaveValidator), Groups.SAVE)]
    [Fluent(typeof(OrderItemSaveOrUpdateValidator), Groups.SAVE_OR_UPDATE)]
    public class OrderItem : Dtobase, IChild
    {
        [Dto(Rules.ID)]
        public virtual int? Id { get; set; }

        [Dto(Rules.LOOKUP)]
        public virtual int? ProductId { get; set; }

        [Dto(Rules.LOOKUP)]
        public virtual int? Count { get; set; }

        [Dto(Rules.LOOKUP)]
        public virtual float? Price { get; set; }

        [Dto(Rules.FULL, Nested=Rules.ID)]
        public virtual Order Order { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is OrderItem)) return false;
            return Util.AreSame(this.Id, (obj as OrderItem).Id);
        }

        public virtual void connectToParent(object parent)
        {
            if (parent is Order)
            {
                this.Order = (Order)parent;
            }
        }
    }
}