﻿using CasgemUow.BusinessLayer.Abstract;
using CasgemUow.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CasgemUow.PresentationLayer.Controllers
{
    public class DefaultController : Controller
    {
        private readonly ICustomerProcessService _customerProcessService;
        private readonly ICustomerService _customerService;

        public DefaultController(ICustomerProcessService customerProcessService, ICustomerService customerService)
        {
            _customerProcessService = customerProcessService;
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(CustomerProcess customerProcess)
        {
            var valueSender = _customerService.TGetByID(customerProcess.SenderID);
            var valueReeciver = _customerService.TGetByID(customerProcess.ReceiverID);

            valueReeciver.CustomerBalance += customerProcess.Amount;
            valueReeciver.CustomerBalance += customerProcess.Amount;

            List<Customer> modifiedCustomer = new List<Customer>()
            {
                valueSender,
                valueReeciver
            };

            _customerService.TMultiUpdate(modifiedCustomer);
            _customerProcessService.TInsert(customerProcess);
            return RedirectToAction("CustomerProcessList");
        }
    }
}
