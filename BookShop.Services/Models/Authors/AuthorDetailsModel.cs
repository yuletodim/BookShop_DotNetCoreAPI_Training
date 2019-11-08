namespace BookShop.Services.Models.Authors
{
    using System.Collections.Generic;
    using System.Linq;

    using Data.Models;
    using Common.Mappings;
    using AutoMapper;

    public class AuthorDetailsModel : IMapFrom<Author>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IEnumerable<string> Books { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile
                .CreateMap<Author, AuthorDetailsModel>()
                .ForMember(a => a.Books, cfg => cfg.MapFrom(a => a.Books.Select(b => b.Title)));
        }
    }
}
