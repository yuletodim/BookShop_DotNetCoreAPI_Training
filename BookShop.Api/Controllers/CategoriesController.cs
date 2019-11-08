namespace BookShop.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    using Services.Contracts;
    using Infrastructure.Extensions;
    using Models.Categories;

    public class CategoriesController : BaseApiController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        // GET /api/categories
        [HttpGet]
        public async Task<IActionResult> Get()
            => this.Ok(await this.categoriesService.GetAllAsync());

        // GET /api/categories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => this.ReturnOkOrNotFound(await this.categoriesService.GetByIdAsync(id));

        // PUT /api/categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditCategoryModel model)
        {
            var existsCategory = await this.categoriesService.ExistsCategoryWithIdAsync(id);
            if (!existsCategory)
            {
                return this.NotFound();
            }

            var existingName = await this.categoriesService.ExistsCategoryWithNameAsync(model.Name);
            if (existingName)
            {
                return this.BadRequest($"Category with name: \"{model.Name}\" exists already.");
            }

            await this.categoriesService.EditAsync(id, model.Name);

            return this.Ok();
        }
    }
}
