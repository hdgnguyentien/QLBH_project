using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLBH_project.Models
{
    public class employees
    {
        public Guid Id { get; set; }
        public Guid roleID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
        public roles roles { get; set; }
        public List<orders> orders { get; set; }
    }
}
