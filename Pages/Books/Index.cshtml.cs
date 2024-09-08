using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Online_Book_Store.Data;
using Online_Book_Store.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        //
        public SelectList GenreList { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SelectedGenre { get; set; }
        //
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchAuthor { get; set; }
        //
        [BindProperty(SupportsGet = true)]
        public string SortParametar { get; set; } = "ID";
        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; } = "Asc";
        //
        [BindProperty(SupportsGet = true)]
        public bool MoreInfo { get; set; }

        public async Task OnGetAsync()
        {
            
            var books = _context.Book
                .Include(ba => ba.BookAuthor)
                .ThenInclude(a => a.Author)
                .AsNoTracking();

            IQueryable<string> genreQuery = from g in _context.Book
                                            select g.Genre;

            if (!String.IsNullOrEmpty(SelectedGenre))
            {
                books = books.Where(b => b.Genre == SelectedGenre);
            }
            if (!String.IsNullOrEmpty(SearchString))
            {
                books = books.Where(b => b.Title.Contains(SearchString));
            }
            if (!String.IsNullOrEmpty(SearchAuthor))
            {
                books = books.Where(b => b.BookAuthor.Any(a => a.Author.Name.Contains(SearchAuthor)));
            }

            if(SortOrder.Equals("Desc"))
            {
                switch (SortParametar)
                {
                    case "ID": books = books.OrderByDescending(m => m.ID); break;
                    case "Title": books = books.OrderByDescending(m => m.Title); break;
                    case "Genre": books = books.OrderByDescending(m => m.Genre).ThenBy(m => m.Title); break;
                    case "Publish Date": books = books.OrderByDescending(m => m.Publish_date); break;
                    case "Rating": books = books.OrderByDescending(m => m.Rating); break;
                    case "Price": books = books.OrderByDescending(m => m.Price); break;
                }
            }
            else
            {
                switch (SortParametar)
                {
                    case "ID": books = books.OrderBy(m => m.ID); break;
                    case "Title": books = books.OrderBy(m => m.Title); break;
                    case "Genre": books = books.OrderBy(m => m.Genre).ThenBy(m => m.Title); break;
                    case "Publish Date": books = books.OrderBy(m => m.Publish_date); break;
                    case "Rating": books = books.OrderBy(m => m.Rating); break;
                    case "Price": books = books.OrderBy(m => m.Price); break;

                }
            }
            

            GenreList = new SelectList(await genreQuery.Distinct().ToListAsync());

            Book = await books.ToListAsync();
        }
    }
}
