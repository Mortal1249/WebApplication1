using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    public class ItemsModel : PageModel
    {
        private readonly ItemService _itemService;
        private readonly CartService _cartService;

        public List<Item> Items { get; set; }

        public ItemsModel(ItemService itemService, CartService cartService)
        {
            _itemService = itemService;
            _cartService = cartService;
        }

        public void OnGet()
        {
            Items = _itemService.GetItems();
        }

        public IActionResult OnPostAddToCart(int id)
        {
            var item = _itemService.GetItemById(id);
            if (item != null)
            {
                _cartService.AddItemToCart(item);
            }
            return RedirectToPage();
        }
    }
}
