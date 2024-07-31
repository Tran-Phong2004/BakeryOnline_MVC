using BakeryOnline_MVC.Models;

namespace BakeryOnline_MVC.Repository
{
    public class UnitOfWork
    {
        IRepositoryBase<Category> _categoryRepository;
        IRepositoryBase<Product> _productRepository;
        ApplicationDbContext appContext;

        public IRepositoryBase<Category> CategoryRepository { get => _categoryRepository ?? new CategoryRepository(appContext); }
        public IRepositoryBase<Product> ProductRepository { get => _productRepository ?? new ProductRepository(appContext);}

        public UnitOfWork(ApplicationDbContext appContext, IRepositoryBase<Category> categoryRepository,
                                                          IRepositoryBase<Product> productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            this.appContext = appContext;
        }

        public void SaveChange()
        {
            appContext.SaveChanges();
        }
    }
}
