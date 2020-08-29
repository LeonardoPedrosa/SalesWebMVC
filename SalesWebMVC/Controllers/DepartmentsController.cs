using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
  public class DepartmentsController : Controller
  {
    private readonly IService _service;
    public DepartmentsController(IService service)
    {
      _service = service;
    }

    // GET: Departments
    public async Task<IActionResult> Index()
    {
      return View(await _service.GetDepartments());
    }

    // GET: Departments/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var department = await _service.GetSelDepartmentsById(id.Value);

      if (department == null)
      {
        return NotFound();
      }

      return View(department);
    }

    // GET: Departments/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Departments/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] Department department)
    {
      if (ModelState.IsValid)
      {
        _service.Add(department);
        await _service.SaveChangeAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(department);
    }

    // GET: Departments/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var department = await _service.GetSelDepartmentsById(id.Value);

      if (department == null)
      {
        return NotFound();
      }
      return View(department);
    }

    // POST: Departments/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Department department)
    {
      if (id != department.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _service.Update(department);
          await _service.SaveChangeAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!_service.DepartmentExists(department.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(department);
    }

    // GET: Departments/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var department = await _service.GetSelDepartmentsById(id.Value);

      if (department == null)
      {
        return NotFound();
      }

      return View(department);
    }

    // POST: Departments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var department = await _service.GetSelDepartmentsById(id);
      _service.Delete(department);
      await _service.SaveChangeAsync();
      return RedirectToAction(nameof(Index));
    }

    //private bool DepartmentExists(int id)
    //{
    //    return _service.Department.Any(e => e.Id == id);
    //}
  }
}
