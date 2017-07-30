using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechTime.ViewModels.Api
{
    public class XEditableViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Pk { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
