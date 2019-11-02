﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;
namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
       
private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;

        }
        public IActionResult Index()
        {

            var list = _sellerService.FindALL();

            return View(list);
        }


        //IActionResult o tipo de retorno de todas as ações
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(Seller seller)
        {


            _sellerService.Insert(seller);

            return RedirectToAction(nameof(Index));


        }
    }
}