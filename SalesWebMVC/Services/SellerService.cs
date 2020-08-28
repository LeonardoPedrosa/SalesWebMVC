using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
  public class SellerService
  {
    private readonly SalesWebMVCContext _context;

    public SellerService(SalesWebMVCContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<Seller>> FindAll()
    {
      return _context.Seller.ToList();
    }

    public void Insert(Seller seller)
    {
      _context.Add(seller);
    }

    public async Task<bool> SaveChangesAsync()
    {
      return (await _context.SaveChangesAsync()) > 0;
    }

    public async Task<List<SelectListItem>> FindAllDept()
    {
      var items = _context.Department.ToList();
      var departments = items.Select(c => new SelectListItem
      {
        Value = c.Id.ToString(),
        Text =  c.Name
      }).ToList();

      return departments;
    }

    public async Task<Seller> FindById(int id)
    {
      return await _context.Seller.Include(d => d.department).FirstOrDefaultAsync(s => s.Id == id);
    }

    public void Delete(int id)
    {
      var seller = _context.Seller.Find(id);
      _context.Seller.Remove(seller);
    }

    public void Update(Seller seller)
    {
      if(!_context.Seller.Any(s => s.Id == seller.Id))
      {
        throw new NotFoundException("Seller not found");
      }
      try
      {
        _context.Update(seller);
      }
      catch(DbUpdateConcurrencyException e)
      {
        throw new DbConcurrencyException(e.Message);
      }
    }
  }
}
