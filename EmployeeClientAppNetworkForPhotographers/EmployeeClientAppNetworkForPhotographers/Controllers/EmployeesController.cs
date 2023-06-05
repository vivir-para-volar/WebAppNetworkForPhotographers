﻿using EmployeeClientAppNetworkForPhotographers.Models.Lists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeClientAppNetworkForPhotographers.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class EmployeesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}