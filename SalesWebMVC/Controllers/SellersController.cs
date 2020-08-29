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
  //DI
  public class SellersController : Controller
  {
    private readonly IService _service;

    public SellersController(IService service)
    {
      _service = service;
    }

    public async Task<IActionResult> Index()
    {
      var list = await _service.GetSellers();
      return View(list);
    }

    public async Task<IActionResult> Create()
    {
      ViewBag.Departments = await _service.FindAllDept();
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Seller seller)
    {
      _service.Add(seller);
      if (await _service.SaveChangeAsync())
      {
        return RedirectToAction(nameof(Index));
      }
      return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      var seller = await _service.GetSellerById(id.Value);

      if (seller == null)
      {
        return NotFound();
      }
      return View(seller);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, Seller seller)
    {
      _service.Delete(seller);
      if (await _service.SaveChangeAsync())
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
      var seller = await _service.GetSellerById(id.Value);

      if (seller == null)
      {
        return NotFound();
      }
      return View(seller);
    }

    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var seller = await _service.GetSellerById(id.Value);

      if (seller == null)
      {
        return NotFound();
      }

      ViewBag.Departments = await _service.FindAllDept();
      return View(seller);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Seller seller)
    {
      if (id != seller.Id)
      {
        return NotFound();
      }

      try
      {
        _service.Update(seller);

        await _service.SaveChangeAsync();

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