using System.Collections.Generic;
using System.Web.Mvc;
using Upida;
using Upida.Validation;
using UpidaExampleStraight.Business;
using UpidaExampleStraight.Domain;

namespace UpidaExampleStraight.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly OrderBusiness orderBusiness;

        public OrderController(FormParser formParser, Validator validator, OrderBusiness orderBusiness)
            : base(formParser, validator)
        {
            this.orderBusiness = orderBusiness;
        }

        public ActionResult Index(int clientId)
        {
            IList<Order> items = this.orderBusiness.GetByClient(clientId);
            ViewResult view = View(items);
            view.ViewBag.ClientId = clientId;
            return view;
        }

        [HttpGet]
        public ActionResult Create(int clientId)
        {
            ViewResult view = View(this.EmptyOrder(clientId));
            view.ViewBag.ClientId = clientId;
            view.ViewBag.Products = this.GetProducts();
            return view;
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Save(int clientId)
        {
            Order item = this.formParser.Parse<Order>(this.Request.Form);
            bool success = this.validator.ValidateAndPublish(item, Groups.SAVE, this.ModelState);
            if (!success)
            {
                ViewResult view = View(item);
                view.ViewBag.ClientId = clientId;
                view.ViewBag.Products = this.GetProducts();
                return view;
            }

            this.orderBusiness.Save(item);
            return this.RedirectToAction("index", new {clientId = clientId});
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Order item = this.orderBusiness.GetFull(id);
            ViewResult view = View(item);
            view.ViewBag.ClientId = item.Client.Id.Value;
            view.ViewBag.Products = this.GetProducts();
            return view;
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Update(int id, int clientId)
        {
            Order item = this.formParser.Parse<Order>(this.Request.Form);
            bool success = this.validator.ValidateAndPublish(item, Groups.UPDATE, this.ModelState);
            if (!success)
            {
                ViewResult view = View(item);
                view.ViewBag.ClientId = clientId;
                view.ViewBag.Products = this.GetProducts();
                return view;
            }

            this.orderBusiness.Update(item);
            return this.RedirectToAction("index", new { clientId = clientId });
        }

        [HttpGet]
        public ActionResult EditItems(int id)
        {
            Order item = this.orderBusiness.GetFull(id);
            ViewResult view = View(item);
            view.ViewBag.ClientId = item.Client.Id.Value;
            view.ViewBag.Products = this.GetProducts();
            return view;
        }

        [HttpPost]
        [ActionName("EditItems")]
        public ActionResult UpdateItems(int id, int clientId)
        {
            Order item = this.formParser.Parse<Order>(this.Request.Form);
            bool success = this.validator.ValidateAndPublish(item, Groups.UPDATE_A, this.ModelState);
            if (!success)
            {
                ViewResult view = View(item);
                view.ViewBag.ClientId = clientId;
                view.ViewBag.Products = this.GetProducts();
                return view;
            }

            this.orderBusiness.Update(item);
            return this.RedirectToAction("index", new { clientId = clientId });
        }

        public ActionResult Show(int id)
        {
            Order item = this.orderBusiness.GetFull(id);
            ViewResult view = View(item);
            view.ViewBag.ClientId = item.Client.Id.Value;
            return view;
        }

        private Order EmptyOrder(int clientId)
        {
            Order item = new Order();
            item.Client = new Client();
            item.Client.Id = clientId;
            item.OrderItems = new ListAndSet<OrderItem>();
            item.OrderItems.Add(this.EmptyOrderItem());
            item.OrderItems.Add(this.EmptyOrderItem());
            return item;
        }

        private OrderItem EmptyOrderItem()
        {
            OrderItem item = new OrderItem();
            item.Count = 0;
            item.Price = 0.0f;
            item.ProductId = null;
            return item;
        }

        private List<KeyValuePair<int, string>> GetProducts()
        {
            List<KeyValuePair<int, string>> products = new List<KeyValuePair<int, string>>();
            products.Add(new KeyValuePair<int, string>(1, "product A"));
            products.Add(new KeyValuePair<int, string>(2, "product B"));
            products.Add(new KeyValuePair<int, string>(3, "product C"));
            products.Add(new KeyValuePair<int, string>(4, "product D"));
            products.Add(new KeyValuePair<int, string>(5, "product E"));
            return products;
        }
    }
}