using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
      private readonly SellerService _sellerService;

    public SellersController(SellerService sellerService)
    {
        this._sellerService = sellerService ?? throw new ArgumentNullException(nameof(sellerService));
    }
      public async Task<IActionResult> Index()
      {
          var list = await _sellerService.FindAll(); 
          return View(list);
      }

      public async Task<IActionResult> Create()
      {
        ViewBag.Departments = await _sellerService.FindAllDept();
        return View();
      }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Seller seller)
    {       
      _sellerService.Insert(seller);
      if (await _sellerService.SaveChangesAsync())
      {
        return RedirectToAction(nameof(Index));
      }
    return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
      if(id == null)
      {
        return NotFound();
      }
      var seller = await _sellerService.FindById(id.Value);

      if (seller == null)
      {
        return NotFound();
      }
      return View(seller);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
      _sellerService.Delete(id);
      if(await _sellerService.SaveChangesAsync())
      {
        return RedirectToAction(nameof(Index));
      }
      return NotFound();
    }

    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      var seller = await _sellerService.FindById(id.Value);

      if (seller == null)
      {
        return NotFound();
      }
      return View(seller);
    }

    public async Task<IActionResult> Edit(int? id)
    {
      if(id == null)
      {
        return NotFound();
      }

      var seller = await _sellerService.FindById(id.Value);

      if(seller == null)
      {
        return NotFound();
      }

      ViewBag.Departments = await _sellerService.FindAllDept();
      return View(seller);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Seller seller) 
    { 
      if(id != seller.Id)
      {
        return NotFound();
      }

      try
      {
        _sellerService.Update(seller);

        await _sellerService.SaveChangesAsync();
          
        return RedirectToAction(nameof(Index));
          
      }
      catch (NotFoundException)
      {
        return NotFound();
      }
      catch (DbConcurrencyException)
      {
        return NotFound();
      }
    }
  }
}