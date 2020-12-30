using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly RestarauntDbContext _db;

        public IngredientsController(RestarauntDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient>>> Get()
        {
            return await _db.Ingredients.Include(i => i.Provider).Take(20).ToListAsync();
        }

        [HttpGet("providers")]
        public async Task<ActionResult<IEnumerable<Provider>>> GetClients()
        {
            return await _db.Providers.OrderBy(p => p.Id).ToListAsync();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredient>> Get(int id)
        {
            Ingredient ingredient = await _db.Ingredients.FirstOrDefaultAsync(i => i.Id == id);
            if (ingredient == null)
                return NotFound();

            return new ObjectResult(ingredient);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<Ingredient>> Post(Ingredient ingredient)
        {
            if (ingredient == null)
                return BadRequest();

            var provider = await _db.Providers.FirstOrDefaultAsync(p => p.Id == ingredient.ProviderId);
            if (provider == null)
                return NotFound();

            Ingredient newIngredient = new Ingredient
            {
                Name = ingredient.Name,
                ReleaseDate = ingredient.ReleaseDate,
                Count = ingredient.Count,
                Cost = ingredient.Cost,
                ExpirationDate = ingredient.ExpirationDate,
                ProviderId = ingredient.ProviderId
            };

            _db.Ingredients.Add(newIngredient);
            await _db.SaveChangesAsync();

            ingredient.Id = _db.Ingredients.ToList().LastOrDefault().Id;
            ingredient.Provider = provider;

            return Ok(ingredient);
        }

        // PUT api/<ValuesController>/5
        [HttpPut]
        public async Task<ActionResult<Ingredient>> Put(Ingredient updIngredient)
        {
            Ingredient ingredient = await _db.Ingredients.FirstOrDefaultAsync(i => i.Id == updIngredient.Id);
            if (ingredient == null)
                return NotFound();

            var provider = await _db.Providers.FirstOrDefaultAsync(p => p.Id == updIngredient.ProviderId);
            if (provider == null)
                return NotFound();

            ingredient.Name = updIngredient.Name;
            ingredient.ReleaseDate = updIngredient.ReleaseDate;
            ingredient.Count = updIngredient.Count;
            ingredient.Cost = updIngredient.Cost;
            ingredient.ExpirationDate = updIngredient.ExpirationDate;
            ingredient.ProviderId = updIngredient.ProviderId;

            _db.Update(ingredient);
            await _db.SaveChangesAsync();

            ingredient.Provider = provider;

            return Ok(ingredient);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ingredient>> Delete(int id)
        {
            Ingredient ingredient = await _db.Ingredients.FirstOrDefaultAsync(i => i.Id == id);
            if (ingredient == null)
                return NotFound();

            _db.Ingredients.Remove(ingredient);
            await _db.SaveChangesAsync();

            return Ok(ingredient);
        }
    }
}
