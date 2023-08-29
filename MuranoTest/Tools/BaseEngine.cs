using MuranoTest.Models;

namespace MuranoTest.Tools
{
    public abstract class BaseEngine
    {
        public virtual async Task<List<UrlObject>> SearchAsync(string searchText)
        {
            throw new NotImplementedException();
        }
    }
}
