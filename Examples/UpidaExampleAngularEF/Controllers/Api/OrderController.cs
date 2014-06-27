using System.Collections.Generic;
using System.Web.Http;
using Upida.Validation;
using UpidaExampleAngularEF.Business;
using UpidaExampleAngularEF.Domain;

namespace UpidaExampleAngularEF.Controllers.Api
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

		[HttpPost]
		public void Delete(int id)
		{
			this.orderBusiness.Delete(id);
		}
	}
}