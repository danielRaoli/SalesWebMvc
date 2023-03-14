using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc1.Models.Entities;
using SalesWebMvc1.Models.Entities.Exceptions;
using SalesWebMvc1.Models.ViewModels;
using SalesWebMvc1.Services;
using SalesWebMvc1.Services.Exceptions;
using System.Data;
using System.Diagnostics;

namespace SalesWebMvc1.Controllers
{
    public class SellerController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public SellerController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var sellers = await _sellerService.FindAll();
            return View(sellers);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            _sellerService.AddSeller(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            }
            var obj = await _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new
                {
                    Message = "Id not Found"
                });
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Seller seller)
        {
            try
            {
                await _sellerService.DeletSeller(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { Message = e.Message });
            }
            catch (DbUpdateException e)
            {
                return RedirectToAction(nameof(Error), new { Message = e.Message });
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            }
            var obj = await _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new
                {
                    Message = "Id not Found"
                });
            }
            SellerFormViewModel view = new SellerFormViewModel { Seller = obj, Departments = await _departmentService.FindAll() };
            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new
                {
                    Message = "Id MisMacth"
                });
            }
            try
            {
                await _sellerService.Update(seller);
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new
                {
                    Message = e.Message
                });
            }
            catch (DBConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new
                {
                    Message = e.Message
                });
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            }
            var obj = await _sellerService.FindById(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new
                {
                    Message = "Id not Found"
                });
            }
            return View(obj);
        }

        public IActionResult Error(string message)
        {
            ErrorViewModel view = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier

            };

            return View(view);
        }


    }
}

