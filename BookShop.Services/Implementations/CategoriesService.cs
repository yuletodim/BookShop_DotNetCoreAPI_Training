namespace BookShop.Services.Implementations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Contracts;
    using Models.Categories;

    public class CategoriesService : ICategoriesService
    {
        private readonly BookShopDbContext dbContext;

        public CategoriesService(BookShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
            => await this.dbContext
                .Categories
                .ProjectTo<CategoryModel>()
                .ToListAsync();

        public async Task<CategoryModel> GetByIdAsync(int id)
            => await this.dbContext
                .Categories
                .Where(c => c.Id == id)
                .ProjectTo<CategoryModel>()
                .FirstOrDefaultAsync();

        public async Task<bool> ExistsCategoryWithIdAsync(int id)
            => await this.dbContext
                .Categories
                .AnyAsync(c => c.Id ==id );

        public async Task<bool> ExistsCategoryWithNameAsync(string name)
            => await this.dbContext
                .Categories
                .AnyAsync(c => c.Name == name);

        public async Task EditAsync(int id, string name)
        {
            var category = this.dbContext.Categories.Find(id);
            category.Name = name;

            await this.dbContext.SaveChangesAsync();
        }
    }
}
