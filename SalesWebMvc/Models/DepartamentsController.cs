
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SalesWebMvc.Models;

namespace SalesWebMvc.Models
{
    public class DepartamentsController : Controller
    {
        public IActionResult Index()
        {


            List<Departament> list = new List<Departament>();
            list.Add(new Departament { Id = 1, Name = "Eletronicos" });
            list.Add(new Departament { Id = 2, Name = "Fashion" });

            return View(list);
        }
    }
}