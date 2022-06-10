using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Book_Store.Data;
using Online_Book_Store.Models;

namespace Online_Book_Store.Pages.BooksAuthors
{
    public class EditModel : PageModel
    {
        private readonly Online_Book_Store.Data.BookContext _context;

        public EditModel(Online_Book_Store.Data.BookContext context)
        {
            _context = context;
        }

        public SelectList AuthorList { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
        public List<Author> Authors { get; set; }
        [BindProperty(SupportsGet = true)]
        public int CurrentBookId { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ViewData["AuthorId"] = new SelectList(_context.Author, "ID", "ID");
            ViewData["BookId"] = new SelectList(_context.Book, "ID", "ID");



            IQueryable<string> authors = from a in _context.Author
                                         select Convert.ToString(a.ID) + " " + a.Name;
            AuthorList = new SelectList(await authors.ToListAsync());

            //helps display list of already selected Author
            BookAuthors = await _context.BookAuthor.ToListAsync();
            Authors = await _context.Author.ToListAsync();


            return Page();
        }

        [BindProperty]
        public BookAuthor BookAuthor { get; set; }
        [BindProperty]
        public string SelectedAuthor { get; set; }



        public async Task<IActionResult> OnPostAsync()
        {
            
            BookAuthor.BookId = CurrentBookId;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            BookAuthor.AuthorId = Convert.ToInt32(SelectedAuthor.Substring(0, SelectedAuthor.IndexOf(" ")));

            _context.BookAuthor.Add(BookAuthor);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Books/Index");
        }
    }
}
