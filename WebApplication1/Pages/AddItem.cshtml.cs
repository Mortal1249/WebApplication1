using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    public class AddItemModel : PageModel
    {
        private readonly ItemService _itemService;

        [BindProperty]
        public Item NewItem { get; set; }

        public AddItemModel(ItemService itemService)
        {
            _itemService = itemService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _itemService.AddItem(NewItem); // ��������� ����� �����
                return RedirectToPage("/Admin"); // �������������� �� �������� �����-������
            }
            return Page(); // �������� �� ������� �������� ��� ������ ���������
        }
    }
}
