using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenifitCostDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BenifitCostDemo.Controllers
{
    public class EmployeeBenifitCostController : Controller
    {
        private readonly HRMSContext _context;
        public EmployeeBenifitCostController(HRMSContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<EmployeeBenifitCost> lstEmpCost = new List<EmployeeBenifitCost>();          
            lstEmpCost = _context.employeeBenifitCosts.FromSqlRaw($"GetEmployeeCostDetails").ToList();
            return View(lstEmpCost);
           
        }
    }
}