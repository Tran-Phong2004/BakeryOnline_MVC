using BakeryOnline_MVC.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BakeryOnline_MVC.Repository
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(ApplicationDbContext appContext) : base(appContext)
        {
        }
    }
}
