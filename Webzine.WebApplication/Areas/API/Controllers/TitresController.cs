using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using Webzine.Entity;
using Webzine.Repository.Contracts;

namespace Webzine.WebApplication.Areas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitresController : ControllerBase
    {
        private ITitreRepository _titreRepository;
        public TitresController(ITitreRepository titreRepository)
        {
            _titreRepository = titreRepository;
        }

        // GET: api/Titres
        [HttpGet]
        public IActionResult GetTitres()
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(_titreRepository.FindAll(), Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            }
            catch (Exception e)
            {
                throw; return StatusCode(500, e);
            }
        }

        // GET: api/Titres/5
        [HttpGet("{id}")]
        public IActionResult GetTitre(int id)
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(_titreRepository.Find(id), Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            }
            catch (Exception e)
            {
                throw; return StatusCode(500, e);
            }

        }

        // PUT: api/Titres/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public IActionResult PutTitre(int id, Titre titre)
        {
            if (id != titre.IdTitre)
            {
                return BadRequest();
            }
            try
            {
                _titreRepository.Update(titre);
                return Ok(titre);
            }
            catch (Exception e)
            {
                throw; return StatusCode(500, e);
            }


        }

        // POST: api/Titres
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public IActionResult PostTitre(Titre titre)
        {
            try
            {
                _titreRepository.Add(titre);
                return CreatedAtAction("GetTitre", new { id = titre.IdTitre }, titre);
            }
            catch (Exception e)
            {
                throw; return StatusCode(500, e);
            }
        }

        // DELETE: api/Titres/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTitre(int id)
        {
            try
            {
                _titreRepository.Delete(_titreRepository.Find(id));
                return Ok(id);
            }
            catch (Exception e)
            {
                throw; return StatusCode(500, e);
            }
        }

    }
}
