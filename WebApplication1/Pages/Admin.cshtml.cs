using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;

namespace WebApplication1.Pages
{
    public class AdminModel : PageModel
    {
        private readonly ItemService _itemService;

        public List<Item> Items { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public decimal Price { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; } // Свойство для файла изображения

        public AdminModel(ItemService itemService)
        {
            _itemService = itemService;
        }

        public void OnGet()
        {
            Items = _itemService.GetItems(); // Загрузка списка товаров
        }

        public IActionResult OnPostAddItem()
        {
            string imagePath = null;

            if (Image != null)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }
                var filePath = Path.Combine(uploads, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }
                imagePath = "/images/" + fileName;
            }

            var item = new Item
            {
                Name = Name,
                Description = Description,
                Price = Price,
                ImagePath = imagePath // Сохранение пути к изображению
            };

            _itemService.AddItem(item); // Добавление товара
            return RedirectToPage();
        }

        public IActionResult OnPostDeleteItem(int id)
        {
            _itemService.DeleteItem(id); // Удаление товара по id
            return RedirectToPage();
        }
    }
}
