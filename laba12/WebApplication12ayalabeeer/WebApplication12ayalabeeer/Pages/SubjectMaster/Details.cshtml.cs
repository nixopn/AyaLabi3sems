using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication12ayalabeeer.Models;

namespace WebApplication12ayalabeeer.Pages.SubjectMaster
{
    public class DetailsModel : PageModel
    {
        private readonly WebApplication12ayalabeeer.Models.LabAyaerContext _context;

        public DetailsModel(WebApplication12ayalabeeer.Models.LabAyaerContext context)
        {
            _context = context;
        }

        public Subject Subject { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects.FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }
            else
            {
                Subject = subject;
            }
            return Page();
        }
    }
}
