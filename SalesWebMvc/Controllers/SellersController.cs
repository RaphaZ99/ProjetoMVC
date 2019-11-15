using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exception;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
       
private readonly SellerService _sellerService;
private readonly DepartamentService _departamentService;

        public SellersController(SellerService sellerService, DepartamentService departamentService )
        {
            _sellerService = sellerService;
            _departamentService = departamentService;

        }
        public IActionResult Index()
        {

            var list = _sellerService.FindALL();

            return View(list);
        }


        //IActionResult o tipo de retorno de todas as ações
        //Metodo para abrir a tela de cadastro de vendedores
        public IActionResult Create()
        {

            var departaments = _departamentService.FindAll();
            var viewModel = new SellerFormViewModel { Departaments = departaments };
            return View(viewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(Seller seller)
        {

            
            _sellerService.Insert(seller);

            return RedirectToAction(nameof(Index));


        }


        public IActionResult Delete(int? id)
        {

            if (id == null)
            {

                return NotFound();

            }

            var obj = _sellerService.FindbyId(id.Value);

            if(obj == null)
            {
                return NotFound();

            }

            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Details(int? id)
        {


            if (id == null)
            {

                return RedirectToAction(nameof(Error), new { message = "ID Não Foi Fornecido" });

            }

            var obj = _sellerService.FindbyId(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID Não Encontrado" });

            }

            return View(obj);

        }

        public IActionResult Edit(int? id)
        {
            if(id == null)
            {


                return RedirectToAction(nameof(Error), new { message = "ID Não Fornecido" });

            }

            var obj = _sellerService.FindbyId(id.Value);
            if(obj == null)
            {

                return RedirectToAction(nameof(Error), new { message = "ID Não Encontrado" });
            }

            List<Departament> departaments = _departamentService.FindAll();

            SellerFormViewModel viewmodel = new SellerFormViewModel { Seller = obj, Departaments = departaments };

            return View(viewmodel);

        }

        [HttpPost]
        public IActionResult Edit(int id, Seller seller)
        {

            if(id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não correspondem" });

            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            } catch(DllNotFoundException e)
            {

                return RedirectToAction(nameof(Error), new { message = e.Message });

            } catch (DbConcurrencyException e)
            {

                return RedirectToAction(nameof(Error), new { message = e.Message});
            }
        }
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current ?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}