using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Services;


namespace WebApplication1.Areas.Admin.Pages
{
    public class CreateModel(ICategoryService categoryService, IMuzInstrumentService muzService) : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            var categoryListData = await categoryService.GetCategoryListAsync();
            ViewData["CategoryId"] = new SelectList(categoryListData.Data, "Id", "Name");
            return Page();
        }
        [BindProperty]
        public MusInstruments mi { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult>OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await muzService.CreateInstAsync(mi, Image);
            return RedirectToPage("./Index");
        }
    }

}

