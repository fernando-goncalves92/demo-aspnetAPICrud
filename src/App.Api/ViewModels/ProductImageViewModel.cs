using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace App.Api.ViewModels
{
    public class ProductImageViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid SupplierId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "O campo precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]        
        public string Description { get; set; }

        public IFormFile ImageUpload { get; set; }
        
        public string Image { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Value { get; set; }

        [ScaffoldColumn(false)]
        public DateTime RegisterDate { get ; set; }

        public bool Active { get; set; }
        
        [ScaffoldColumn(false)]
        public string SupplierName{ get; set; }
    }
}
