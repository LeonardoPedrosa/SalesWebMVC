﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models
{
  public class Seller
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public decimal BaseSalary { get; set; }
    public DateTime BirthDate { get; set; }
    public Department department { get; set; }
    public int departmentId { get; set; }
    public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

    public Seller() 
    { 

    }
    //public Seller(string name, string email, decimal baseSalary, DateTime birthDate, Department department)
    //{
    //  Name = name;
    //  Email = email;
    //  BaseSalary = baseSalary;
    //  BirthDate = birthDate;
    //  this.department = department;
    //}

    public void AddSales(SalesRecord sr)
    {
      Sales.Add(sr);
    }

    public void RemoveSales(SalesRecord sr)
    {
      Sales.Remove(sr);
    }

    public double TotalSales(DateTime initial, DateTime final)
    {
      return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
    }
  }
}
