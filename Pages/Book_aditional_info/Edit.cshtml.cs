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

namespace Online_Book_Store.Pages.Book_aditional_info
{
    public class EditModel : PageModel
    {
        private readonly Online_Book_Store.Data.BookContext _context;

        public EditModel(Online_Book_Store.Data.BookContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book_additional_info Book_additional_info { get; set; }
        [BindProperty(SupportsGet = true)]
        public string BookTitle { get; set; }
        public bool ChangesMade { get; set; } = false;
        public bool DeletedDesc { get; set; } = false;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book_additional_info = await _context.Book_additional_info
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Book_additional_info == null)
            {
                return NotFound();
            }
           
            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Book_additional_info).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Book_additional_infoExists(Book_additional_info.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            ChangesMade = true;
            return Page();
        }

        private bool Book_additional_infoExists(int id)
        {
            return _context.Book_additional_info.Any(e => e.ID == id);
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            _context.Book_additional_info.Remove(Book_additional_info);
            await _context.SaveChangesAsync();

            DeletedDesc = true;
            return Page();
        }
    }
}
