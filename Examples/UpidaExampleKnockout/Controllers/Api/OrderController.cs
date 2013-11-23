using System.Collections.Generic;
using Upida.Validation;
using UpidaExampleKnockout.Business;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Controllers.Api
{
    public class OrderController : ControllerBase
    {
        private readonly OrderBusiness orderBusiness;

        public OrderController(IValidationContext validator, OrderBusiness orderBusiness)
            : base(validator)
        {
            this.orderBusiness = orderBusiness;
        }

        public IList<Order> GetByClient(int clientId)
        {
            return this.orderBusiness.GetByClient(clientId);
        }

        public Order Get(int id)
        {
            return this.orderBusiness.Get(id);
        }

        public Order GetFull(int id)
        {
            return this.orderBusiness.GetFull(id);
        }

        public void Save(Order item)
        {
            this.validator.AssertValid(item, Groups.SAVE);
            this.orderBusiness.Save(item);
        }

        public void Update(Order item)
        {
            this.validator.AssertValid(item, Groups.UPDATE);
            this.orderBusiness.Update(item);
        }

        public void UpdateItems(Order item)
        {
            this.validator.AssertValid(item, Groups.UPDATE_A);
            this.orderBusiness.Update(item);
        }
    }
}