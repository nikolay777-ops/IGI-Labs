using System.Linq.Expressions;
using WEB_0535005_Vashkevich.Entities;

namespace WEB_0535005_Vashkevich.Models
{
    public class ListViewModel<T>: List<T> where T: class
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        private ListViewModel(IEnumerable<T> items, int currentPage, int totalPages) : base(items)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
           
        }

        public static ListViewModel<T> GetModel(IQueryable<T> list, int current, int pageItems, Expression<Func<T, bool>> filter)
        {
            var items = list.Where(filter);
            var total = 0;
            if (items.Count() != list.Count())
            {
                total = (int)Math.Ceiling((double)items.Count() / pageItems);
                return new ListViewModel<T>(items, current, total);
            }
            
            items = items
                .Skip((current - 1) * pageItems)
                .Take(pageItems);
            total = (int)Math.Ceiling((double)list.Count() / pageItems);
            return new ListViewModel<T>(items, current, total);
        }
    }
}
