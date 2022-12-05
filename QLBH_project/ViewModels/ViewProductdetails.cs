using Microsoft.AspNetCore.Http;
using QLBH_project.IRepositories;
using QLBH_project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace QLBH_project.ViewModels
{
    public class ViewProductdetails : productdetails
    {
        public IFormFile ProductdtImage { get; set; }
    }
}