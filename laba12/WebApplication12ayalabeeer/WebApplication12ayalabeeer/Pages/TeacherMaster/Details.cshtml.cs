using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication12ayalabeeer.Models;

namespace WebApplication12ayalabeeer.Pages.TeacherMaster
{
    public class DetailsModel : PageModel
    {
        private readonly WebApplication12ayalabeeer.Models.LabAyaerContext _context;

        public DetailsModel(WebApplication12ayalabeeer.Models.LabAyaerContext context)
        {
            _context = context;
        }

        public Teacher Teacher { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.FirstOrDefaultAsync(m => m.TeacherId == id);
            if (teacher == null)
            {
                return NotFound();
            }
            else
            {
                Teacher = teacher;
            }
            return Page();
        }
    }
}
