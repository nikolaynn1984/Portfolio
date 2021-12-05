using Landing.Library.Model;
using System.Collections.Generic;

namespace Landing.Library.ViewModel
{
    public class PageViewModel
    {
        public PageViewModel(PageView Page, IEnumerable<Blog> Blogs)
        {
            this.Blogs = Blogs;
            this.Page = Page;
        }
        public PageView Page { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
    }
}
