using BakeryOnline_MVC.Models;

namespace BakeryOnline_MVC.Repository
{
    public class CategoryRepository : RepositoryBase<Category>
    {
        public CategoryRepository(ApplicationDbContext appContext) : base(appContext)
        {
        }


    }
}
