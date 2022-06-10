using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Online_Book_Store.Data;
using Online_Book_Store.Models;

namespace Online_Book_Store.Pages.Books
{
    public class DeleteModel : PageModel
    {
        private readonly Online_Book_Store.Data.BookContext _context;

        public DeleteModel(Online_Book_Store.Data.BookContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }
        public List<BookAuthor> BookAuthor { get; set; }
        public Book_additional_info BookAdditionalInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Book
                .Include(ba => ba.BookAuthor)
                .ThenInclude(a => a.Author)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            
            

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Book.FindAsync(id);
            BookAdditionalInfo = await _context.Book_additional_info.FirstOrDefaultAsync(m => m.Book_ID == id);
            var bookAuthor = from b in _context.BookAuthor
                             where b.BookId == id
                             select b;
            BookAuthor = await bookAuthor.ToListAsync();

            if (Book != null)
            {
                _context.Book.Remove(Book);

                foreach(var ba in BookAuthor)
                {
                    if(Book.ID == ba.BookId)
                    {
                        _context.BookAuthor.Remove(ba);
                    }
                }

                if(BookAdditionalInfo != null)
                {
                    _context.Book_additional_info.Remove(BookAdditionalInfo);
                }


                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
