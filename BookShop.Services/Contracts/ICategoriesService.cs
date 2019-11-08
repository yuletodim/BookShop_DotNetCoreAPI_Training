namespace BookShop.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Categories;

    public interface ICategoriesService
    {
        Task<IEnumerable<CategoryModel>> GetAllAsync();

        Task<CategoryModel> GetByIdAsync(int id);

        Task<bool> ExistsCategoryWithIdAsync(int id);

        Task<bool> ExistsCategoryWithNameAsync(string name);

        Task EditAsync(int id, string name);
    }
}
