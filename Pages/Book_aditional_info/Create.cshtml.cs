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

        public IActionResult OnGet()
        {
        ViewData["Book_ID"] = new SelectList(_context.Book, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public Book_additional_info Book_additional_info { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Book_additional_info.Add(Book_additional_info);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
