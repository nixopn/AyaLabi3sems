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
    public class IndexModel : PageModel
    {
        private readonly WebApplication12ayalabeeer.Models.LabAyaerContext _context;

        public IndexModel(WebApplication12ayalabeeer.Models.LabAyaerContext context)
        {
            _context = context;
        }

        public IList<Teacher> Teacher { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Teacher = await _context.Teachers.ToListAsync();
        }
    }
}
