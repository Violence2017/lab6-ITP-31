using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public sealed class Provider
    {
        public Provider()
        {
            Ingredients = new HashSet<Ingredient>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}