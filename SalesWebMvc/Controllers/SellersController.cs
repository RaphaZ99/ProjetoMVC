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
        public  async Task <IActionResult> Index()
        {

            var list = await _sellerService.FindAllAsync();

            return View(list);

        }


        //IActionResult o tipo de retorno de todas as ações
        //Metodo para abrir a tela de cadastro de vendedores
        public async Task <IActionResult> Create()
        {

            var departaments =  await _departamentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departaments = departaments };
            return View(viewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task <IActionResult> Create(Seller seller)
        {

            
           await  _sellerService.InsertAsync(seller);

            return RedirectToAction(nameof(Index));


        }


        public async Task <IActionResult> Delete(int? id)
        {

            if (id == null)
            {

                return NotFound();

            }

            var obj = await _sellerService.FindbyIdAsync(id.Value);

            if(obj == null)
            {
                return NotFound();

            }

            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));

            }catch(IntegrityException e)
            {

                return RedirectToAction(nameof(Error), new { message = e.Message });

            }

        }

        public async Task <IActionResult >Details(int? id)
        {
          

            if (id == null)
            {

                return RedirectToAction(nameof(Error), new { message = "ID Não Foi Fornecido" });

            }

            var obj = await _sellerService.FindbyIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID Não Encontrado" });

            }

            return View(obj);

        }

        public async Task <IActionResult> Edit(int? id)
        {
            if(id == null)
            {


                return RedirectToAction(nameof(Error), new { message = "ID Não Fornecido" });

            }

            var obj = await _sellerService.FindbyIdAsync(id.Value);
            if(obj == null)
            {

                return RedirectToAction(nameof(Error), new { message = "ID Não Encontrado" });
            }

            List<Departament> departaments = await _departamentService.FindAllAsync();

            SellerFormViewModel viewmodel = new SellerFormViewModel { Seller = obj, Departaments = departaments };

            return View(viewmodel);

        }

        [HttpPost]
        public  async Task <IActionResult> Edit(int id, Seller seller)
        {

            if(id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não correspondem" });

            }
            try
            {
              await  _sellerService.UpdateAsync(seller);
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