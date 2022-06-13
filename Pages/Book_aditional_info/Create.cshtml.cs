using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Online_Book_Store.Data;
using Online_Book_Store.Models;

namespace Online_Book_Store.Pages.Book_aditional_info
{
    public class CreateModel : PageModel
    {
        private readonly Online_Book_Store.Data.BookContext _context;

        public CreateModel(Online_Book_Store.Data.BookContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int ReturnId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string BookTitle { get; set; }
        public bool DescCreated { get; set; } = false;

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Book_additional_info Book_additional_info { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Book_additional_info.Book_ID = ReturnId;
            _context.Book_additional_info.Add(Book_additional_info);
            await _context.SaveChangesAsync();

            DescCreated = true;
            return Page();
        }
    }
}
