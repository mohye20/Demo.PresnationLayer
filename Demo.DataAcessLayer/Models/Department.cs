using System;
using System.ComponentModel.DataAnnotations;

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
    }
}