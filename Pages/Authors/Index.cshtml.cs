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
    public class IndexModel : PageModel
    {
        private readonly Online_Book_Store.Data.BookContext _context;

        public IndexModel(Online_Book_Store.Data.BookContext context)
        {
            _context = context;
        }

        public IList<Author> Author { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Unassigned { get; set; } = true;

        public async Task OnGetAsync()
        {
            var authors = _context.Author
                .Include(ba => ba.BookAuthor)//helps determin wheather Author has a Book assigned or not
                .AsNoTracking();

            if (!String.IsNullOrEmpty(SearchString))
            {
                SearchString = SearchString.ToLower();
                authors = authors.Where(a => a.Name.ToLower().Contains(SearchString));
            }

            Author = await authors.ToListAsync();
        }
    }
}
