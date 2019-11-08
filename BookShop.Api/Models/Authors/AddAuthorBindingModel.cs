namespace BookShop.Api.Models.Authors
{
    using System.ComponentModel.DataAnnotations;
    using Common.Mappings;
    using Data.Models;

    using static Data.DataConstants;

    public class AddAuthorBindingModel : IMapTo<Author>
    {
        [Required]
        [MaxLength(AuthorNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(AuthorNameMaxLength)]
        public string LastName { get; set; }
    }
}
