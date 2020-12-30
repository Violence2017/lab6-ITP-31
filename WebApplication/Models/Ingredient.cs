using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Count { get; set; }
        public double Cost { get; set; }
        public int ExpirationDate { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
    }
}