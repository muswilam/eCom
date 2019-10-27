using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCom.Web.ViewModels
{
    public class EditConfigViewModel
    {
        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }
    }
}