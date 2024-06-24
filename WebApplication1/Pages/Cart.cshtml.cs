using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    public class CartModel : PageModel
    {
        private readonly CartService _cartService;

        public List<CartItem> CartItems { get; private set; }

        public CartModel(CartService cartService)
        {
            _cartService = cartService;
        }

        public void OnGet()
        {
            CartItems = _cartService.GetCartItems();
        }

        public IActionResult OnPostClearCart()
        {
            _cartService.ClearCart();
            return RedirectToPage();
        }

        public IActionResult OnPostRemoveFromCart(int itemId)
        {
            _cartService.RemoveFromCart(itemId);
            return RedirectToPage();
        }
    }
}
