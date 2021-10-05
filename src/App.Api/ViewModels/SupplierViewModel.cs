using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Api.ViewModels
{
    public class SupplierViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
        public string Document { get; set; }

        public int SupplierType { get; set; }
        
        public bool Active { get; set; }

        public AddressViewModel Address { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
