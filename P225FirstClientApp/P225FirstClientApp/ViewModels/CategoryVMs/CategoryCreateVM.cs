using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P225FirstClientApp.ViewModels.CategoryVMs
{
    public class CategoryCreateVM
    {
        public string Ad { get; set; }
        public bool Esasdirmi { get; set; }
        public string Sekil { get; set; }
        public IFormFile File { get; set; }
        public Nullable<int> AidOlduguKategoriyaninIdsi { get; set; }
    }
}
