using FindYourWayAPI.Data;
using FindYourWayAPI.Models;
using FindYourWayAPI.Models.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindYourWayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        private readonly FindYourWayDbContext _context;

        public FieldsController(FindYourWayDbContext context)
        {
            _context = context;
        }




        /// <summary>
        /// Returns all fields 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Field>>> GetAllFields()
        {
            var list =await _context.Fields.ToListAsync();
            return Ok(list);
        }




        /// <summary>
        /// Returns a field by specifying its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Field>> GetFieldByID(int id)
        {
            if(id == 0) return BadRequest();
            var item = await _context.Fields.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }





        /*
        [HttpPost]
        public async Task<ActionResult> AddField([FromBody] AddFieldRequest request)
        {
            if (request.FieldName == null || request.FieldName == string.Empty) return BadRequest();
            var newField = new Field { FieldName = request.FieldName };
            await _context.Fields.AddAsync(newField);
            await _context.SaveChangesAsync();
            return Ok(newField);
        }*/
    }
}
