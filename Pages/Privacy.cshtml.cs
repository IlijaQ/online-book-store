using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Book_Store.Pages
{
    public class PrivacyModel : PageModel
    {
        private string ContactEmail { get; set; }

        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            ContactEmail = GetEmail();
            return Redirect($"mailto:{ContactEmail}");
        }

        private string GetEmail()
        {
            return "kujovicilija@gmail.com";
        }
    }
}
