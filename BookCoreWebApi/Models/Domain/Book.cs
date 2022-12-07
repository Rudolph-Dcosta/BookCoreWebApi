using System.ComponentModel.DataAnnotations;

namespace BookCoreWebApi.Models.Domain
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}
