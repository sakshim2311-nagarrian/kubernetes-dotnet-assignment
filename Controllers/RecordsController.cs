using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceApiTier.Models;

namespace ServiceApiTier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<RecordsController> _logger;

        public RecordsController(AppDbContext context, ILogger<RecordsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/records
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Record>>> GetRecords()
        {
            _logger.LogInformation("Fetching all records from database");
            return await _context.Records.ToListAsync();
        }

        // GET: api/records/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Record>> GetRecord(int id)
        {
            _logger.LogInformation("Fetching record with id: {Id}", id);
            var record = await _context.Records.FindAsync(id);

            if (record == null)
            {
                _logger.LogWarning("Record with id {Id} not found", id);
                return NotFound();
            }

            return record;
        }

        // POST: api/records
        [HttpPost]
        public async Task<ActionResult<Record>> CreateRecord(Record record)
        {
            _logger.LogInformation("Creating new record: {Name}", record.Name);

            record.CreatedAt = DateTime.UtcNow;
            _context.Records.Add(record);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecord), new { id = record.Id }, record);
        }

        // PUT: api/records/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecord(int id, Record record)
        {
            if (id != record.Id)
            {
                return BadRequest();
            }

            _context.Entry(record).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Updated record with id: {Id}", id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecordExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/records/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            var record = await _context.Records.FindAsync(id);
            if (record == null)
            {
                return NotFound();
            }

            _context.Records.Remove(record);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted record with id: {Id}", id);

            return NoContent();
        }

        private bool RecordExists(int id)
        {
            return _context.Records.Any(e => e.Id == id);
        }
    }
}