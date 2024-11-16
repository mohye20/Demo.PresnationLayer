using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.DataAcessLayer.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code is Required")]
        public string Code { get; set; }

        public DateTime DateOfCreation { get; set; }
        [InverseProperty("Department")]

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        // Navigtaional property [Many]
    }
}