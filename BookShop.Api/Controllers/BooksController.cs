namespace BookShop.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using AutoMapper;

    using Services.Contracts;
    using Services.Models.Books;
    using Infrastructure.Extensions;
    using Models.Books;
    using Infrastructure.Filters;

    [Route("api/[controller]")]
    public class BooksController : BaseApiController
    {
        private readonly IBooksService booksService;
        private readonly IAuthorsService authorsService;
        private readonly IMapper mapper;

        public BooksController(
            IBooksService booksService, 
            IAuthorsService authorsService,
            IMapper mapper)
        {
            this.booksService = booksService;
            this.authorsService = authorsService;
            this.mapper = mapper;
        }

        // GET /api/books/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => this.ReturnOkOrNotFound(await this.booksService.GetByIdAsync(id));

        // GET /api/books?search={word}
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string search = "")
            => this.Ok(await this.booksService.GetFirstTenBySearch(search));

        // POST /api/books
        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] AddBookBindingModel model)
        {
            var existsAuthor = await this.authorsService.AuthorExistAsync(model.AuthorId);
            if (!existsAuthor)
            {
                return this.BadRequest("Author does not exist.");
            }

            var book = this.mapper.Map<AddBookModel>(model);
            var id = await this.booksService.AddAsync(book);

            return this.Ok(id);
        }

        // PUT /api/books/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]EditBookBindingModel model)
        {
            var existingBook = await this.booksService.ExistAsync(id);
            if (!existingBook)
            {
                return this.NotFound();
            }

            var existsAuthor = await this.authorsService.AuthorExistAsync(model.AuthorId);
            if (!existsAuthor)
            {
                return this.BadRequest("Author does not exist.");
            }

            var book = this.mapper.Map<EditBookModel>(model);
            await this.booksService.EditAsync(id, book);

            return this.Ok();
        }

        // DELETE /api/books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingBook = await this.booksService.ExistAsync(id);
            if (!existingBook)
            {
                return this.NotFound();
            }

            await this.booksService.DeleteAsync(id);
            return this.Ok();
        }

        // POST JSON model
        //{
        // "Title": "Test",
        // "Description": "Alabala",
        // "Price": 10,
        // "Copies": 200,
        // "ReleaseDate": "2017-03-15T11:45:42+00:00",
        // "AuthorId": 1,
        // "Edition": 2,
        // "AgeRestriction": 100,
        // "CategoriesString": "Horror Fiction"
        //}

        // PUT JSON model
        //{
        // "Title": "Test",
        // "Description": "Alabala",
        // "Price": 10,
        // "Copies": 200,
        // "ReleaseDate": "2017-03-15T11:45:42+00:00",
        // "AuthorId": 1,
        // "Edition": 2,
        // "AgeRestriction": 100
        //}
    }
}
