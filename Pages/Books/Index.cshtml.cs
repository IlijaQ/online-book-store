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
    public class IndexModel : PageModel
    {
        private readonly Online_Book_Store.Data.BookContext _context;

        public IndexModel(Online_Book_Store.Data.BookContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; }

        public async Task OnGetAsync()
        {
            Book = await _context.Book
                .Include(ba => ba.BookAuthor)
                .ThenInclude(a => a.Author)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
