using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Domain.Entities;
using WebApplication1.Services;

namespace WebApplication1.Areas.Admin.Pages
{
    [Authorize(Policy = "admin")]
    public class IndexModel : PageModel
    {
        private readonly IMuzInstrumentService _muzServices;
        public IndexModel(IMuzInstrumentService muzService)
        {
            //_context = context;
            _muzServices = muzService;
        }
        public List<MusInstruments> mi { get; set; } = default!;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public async Task OnGetAsync(int? pageNo = 1)
        {
            var response = await _muzServices.GetInstListAsync(null, pageNo.Value);
            if (response.Success)
            {
                mi = response.Data.Items;
                CurrentPage = response.Data.CurrentPage;
                TotalPages = response.Data.TotalPages;
            }
        }
    }
}
