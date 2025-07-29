using Bookly.Core.Entities;

namespace Bookly.WebUI.Models
{
 
        public class CartViewModel
        {
            public List<CartLine>? CartLines { get; set; }
            public decimal TotalPrice { get; set; }
        }
}
