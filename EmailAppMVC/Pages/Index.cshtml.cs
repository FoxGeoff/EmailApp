using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUi.Infrastructure;

namespace WebUi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IViewRenderer _vr;

        public IndexModel(IViewRenderer vr)
        {
            _vr = vr;
        }

        public string ViewRendererTest { get; set; }

        public void OnGet()
        {
            throw new System.ArgumentException("Parameter cannot be null", "original");

            ViewRendererTest = _vr.Render("Features/Email/Test", "Hello World");
        }
    }
}
