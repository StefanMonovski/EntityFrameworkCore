namespace FastFood.Core.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using FastFood.Models;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Orders;

    public class OrdersController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public OrdersController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            var ItemsId = this.context.Items.OrderBy(x => x.Id).Select(x => x.Id).ToList();
            var ItemsNames = this.context.Items.OrderBy(x => x.Id).Select(x => x.Name).ToList();
            var EmployeesId = this.context.Employees.OrderBy(x => x.Id).Select(x => x.Id).ToList();
            var EmployeesNames = this.context.Employees.OrderBy(x => x.Id).Select(x => x.Name).ToList();

            var viewOrder = new CreateOrderViewModel
            {
                Items = ItemsId.Zip(ItemsNames, (x, y) => new { Key = x, Value = y }).ToDictionary(x => x.Key, x => x.Value),
                Employees = EmployeesId.Zip(EmployeesNames, (x, y) => new { Key = x, Value = y }).ToDictionary(x => x.Key, x => x.Value),
            };

            return this.View(viewOrder);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home");
            }

            var order = this.mapper.Map<Order>(model);
            order.DateTime = DateTime.UtcNow;

            this.context.Orders.Add(order);

            this.context.SaveChanges();

            var orderItem = new OrderItem
            {
                OrderId = order.Id,
                ItemId = model.ItemId,
                Quantity = model.Quantity
            };

            this.context.OrderItems.Add(orderItem);

            this.context.SaveChanges();

            return this.RedirectToAction("All", "Orders");
        }

        public IActionResult All()
        {
            var orders = this.context.Orders
                .OrderBy(x => x.Id)
                .ProjectTo<OrderAllViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return this.View(orders);
        }
    }
}
