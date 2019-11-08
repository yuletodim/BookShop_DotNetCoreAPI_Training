namespace BookShop.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using AutoMapper;

    using Services.Contracts;
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Models.Authors;
    using Data.Models;

    public class AuthorsController : BaseApiController
    {
        private readonly IAuthorsService authorsService;
        private readonly IMapper mapper;

        public AuthorsController(IAuthorsService authorsService, IMapper mapper)
        {
            this.authorsService = authorsService;
            this.mapper = mapper;
        }

        // GET /api/authors
        [HttpGet]
        public async Task<IActionResult> Get()
            => this.Ok(await this.authorsService.GetAllAsync());

        // GET /api/authors/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => this.ReturnOkOrNotFound(await this.authorsService.GetByIdAsync(id));

        // POST /api/authors
        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody]AddAuthorBindingModel model)
        {
            // Filter added!!
            //if (!ModelState.IsValid)
            //{
            //    return this.BadRequest(ModelState);
            //}

            var author = this.mapper.Map<Author>(model);
            var id = await this.authorsService.AddAsync(author);

            return this.Ok(id);
        }

        // GET /api/authors/{id}/books
        [HttpGet("{id}/books")]
        public async Task<IActionResult> GetBooks(int id)
            => this.Ok(await this.authorsService.GetAuthorBooksByIdAsync(id));
    }
}
