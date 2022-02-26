using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGames.Models.ViewModels
{
    public class GameVM
    {
        public Game Game { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }

    }
}