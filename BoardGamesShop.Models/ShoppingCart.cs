using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGames.Models
{
   public class ShoppingCart
    {
        public Game Game { get; set; }
        [Range(1,1000,ErrorMessage= "Wprowadź wartość z zakresu od 1 do 1000")]
        public int Count { get; set; }
    }
}
