using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Upida;
using Upida.Validation;
using UpidaExample.Business;
using UpidaExample.Domain;

namespace UpidaExample.Controllers.Api
{
    public class OrderController : ControllerBase
    {
        private readonly OrderBusiness orderBusiness;

        public OrderController(IJsonParser jsonParser, IValidator validator, OrderBusiness orderBusiness)
            : base(jsonParser, validator)
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

        public void Save(JToken data)
        {
            Order item = this.jsonParser.Parse<Order>(data);
            this.validator.ValidateAndThrow(item, Groups.SAVE);
            this.orderBusiness.Save(item);
        }

        public void Update(JToken data)
        {
            Order item = this.jsonParser.Parse<Order>(data);
            this.validator.ValidateAndThrow(item, Groups.UPDATE);
            this.orderBusiness.Update(item);
        }

        public void UpdateItems(JToken data)
        {
            Order item = this.jsonParser.Parse<Order>(data);
            this.validator.ValidateAndThrow(item, Groups.UPDATE_A);
            this.orderBusiness.Update(item);
        }
    }
}