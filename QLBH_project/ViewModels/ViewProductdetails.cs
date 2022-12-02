using Microsoft.AspNetCore.Http;
using QLBH_project.IRepositories;
using QLBH_project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace QLBH_project.ViewModels
{
    public class ViewProductdetails
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CategoriesID { get; set; }
        public string Name { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime DateCreated { get; set; }
        public string LinkImage { get; set; }
        public bool Status { get; set; }
        public IFormFile ProductdtImage { get; set; }
    }

}