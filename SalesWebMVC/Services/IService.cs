using Microsoft.AspNetCore.Mvc.Rendering;
using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
  public interface IService
  {
    void Add<T>(T entity) where T : class;

    void Update<T>(T entity) where T : class;

    void Delete<T>(T entity) where T : class;

    Task<bool> SaveChangeAsync();

    Task<Seller[]> GetSellers();
    Task<Seller> GetSellerById(int id);
    Task<List<SelectListItem>> FindAllDept();

    Task<Department[]> GetDepartments();
    Task<Department> GetSelDepartmentsById(int id);
    bool DepartmentExists(int id);
  }
}
