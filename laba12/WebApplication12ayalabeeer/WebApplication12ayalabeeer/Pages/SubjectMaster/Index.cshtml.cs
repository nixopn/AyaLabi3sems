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
    public class IndexModel : PageModel
    {
        private readonly WebApplication12ayalabeeer.Models.LabAyaerContext _context;

        public IndexModel(WebApplication12ayalabeeer.Models.LabAyaerContext context)
        {
            _context = context;
        }

        public IList<Subject> Subject { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Subject = await _context.Subjects.ToListAsync();
        }
    }
}
