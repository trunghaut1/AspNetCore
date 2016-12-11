using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models.ViewModels
{
    public class ProductFormViewModel
    {
        public Product product { get; set; }
        public IFormFile img { get; set; }
    }
}
