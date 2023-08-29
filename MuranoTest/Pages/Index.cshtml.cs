using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MuranoTest.Data;
using MuranoTest.Models;
using MuranoTest.Tools;
using System.Net;
using System.Text;

namespace MuranoTest.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Input { get; set; }
        public List<UrlObject> Urls { get; set; }
        private ApplicationDbContext _context;
        private RandomUrlListCreator _randomCreator;

        private int _count = 10;
        public IndexModel(ApplicationDbContext context, RandomUrlListCreator randomCreator)
        {
            _context = context;
            _randomCreator = randomCreator;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (Input != null)
            {
                var existingWord = _context.Words.FirstOrDefault(w => w.Name == Input);
                if (existingWord != null)
                {
                    Urls = _context.Urls.Where(w => w.WordId == existingWord.Id).ToList();
                }
                else
                {
                    _context.Words.AddAsync(new Word() { Name = Input });
                    await _context.SaveChangesAsync();
                    Urls = _randomCreator.GetRandomUrlObjects(Input, _count);
                    var wordId = await _context.Words.FirstOrDefaultAsync(w => w.Name == Input);
                    foreach (var url in Urls)
                    {
                        url.WordId = wordId.Id;
                        await _context.Urls.AddAsync(url);
                    }
                    await _context.SaveChangesAsync();
                }
            }
            return Page();
        }
    }
}