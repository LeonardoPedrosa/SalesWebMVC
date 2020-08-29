using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
  public class Service : IService
  {
    private readonly SalesWebMVCContext _context;

    public Service(SalesWebMVCContext context)
    {
      _context = context;
    }
    public void Add<T>(T entity) where T : class
    {
      _context.Add(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
      _context.Remove(entity);
    }
    public void Update<T>(T entity) where T : class
    {
      _context.Update(entity);
    }

    public async Task<bool> SaveChangeAsync()
    {
      return (await _context.SaveChangesAsync()) > 0;
    }

    public async Task<Seller[]> GetSellers()
    {
      IQueryable<Seller> query = _context.Seller
        .Include(s => s.department);

      return await query.ToArrayAsync();
    }

    public async Task<Seller> GetSellerById(int id)
    {
      return await _context.Seller.Include(d => d.department).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<SelectListItem>> FindAllDept()
    {
      var items = _context.Department.ToList();
      var departments = items.Select(c => new SelectListItem
      {
        Value = c.Id.ToString(),
        Text = c.Name
      }).ToList();

      return departments;
    }

    public async Task<Department[]> GetDepartments()
    {
      IQueryable<Department> query = _context.Department;

      return await query.ToArrayAsync();
    }

    public async Task<Department> GetSelDepartmentsById(int id)
    {
      return await _context.Department.FirstOrDefaultAsync(d => d.Id == id);
    }

    public bool DepartmentExists(int id)
    {
      return _context.Department.Any(d => d.Id == id);
    }
  }
}
