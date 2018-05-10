using System.Collections.Generic;
using BookCave.Models.InputModels;

namespace BookCave.Models.ViewModels
{
    public class BuyCartViewModel
    {
        public List<CartViewModel> TheCart { get; set;}
        public CheckoutInputModel Info { get; set; }
    }
}