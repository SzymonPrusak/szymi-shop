
using System.ComponentModel.DataAnnotations;

namespace SzymiShop.WebApi.Persistence
{
    public class Entity
    {
        [Key]
        public required Guid Id { get; set; }
    }
}
