
using System.ComponentModel.DataAnnotations;

namespace SzymiShop.WebApi.Persistence
{
    internal class Entity
    {
        [Key]
        public required Guid Id { get; set; }
    }
}
