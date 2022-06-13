using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Online_Book_Store.Data;
using Online_Book_Store.Models;

namespace Online_Book_Store.Pages.Authors
{
    public class DetailsModel : PageModel
    {
        private readonly Online_Book_Store.Data.BookContext _context;

        public DetailsModel(Online_Book_Store.Data.BookContext context)
        {
            _context = context;
        }

        public Author Author { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool DeleteAttempt { get; set; } = false;


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = _context.Author.Include(ba => ba.BookAuthor)
                .ThenInclude(b => b.Book)
                .AsNoTracking();


            Author = await author.FirstOrDefaultAsync(m => m.ID == id);

            if (Author == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
