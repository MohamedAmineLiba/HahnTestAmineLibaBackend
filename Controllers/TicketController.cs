using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_FullStackDevDotNet.Data;
using Task_FullStackDevDotNet.Entities;

namespace Task_FullStackDevDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public TicketController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ticket>>> GetAllTickets([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 7)
        {
            var totalTickets = await _dataContext.Tickets.CountAsync();
            var totalPages = (int)Math.Ceiling(totalTickets / (double)pageSize);

            var tickets = await _dataContext.Tickets
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new
            {
                TotalTickets = totalTickets,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                Tickets = tickets
            };

            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicketById(int id)
        {
            var ticket = await _dataContext.Tickets.FindAsync(id);
            if (ticket is null)
                return BadRequest("Ticket not found: try a different value!");
            return Ok(ticket);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<List<Ticket>>> GetTicketByStatus(bool status, int pageNumber = 1, int pageSize = 7)
        {
            var tickets = await _dataContext.Tickets
                .Where(t => t.Status == status)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return Ok(tickets);
        }

        [HttpPost]
        public async Task<ActionResult<List<Ticket>>> AddTicket([FromBody] Ticket ticket)
        {
            _dataContext.Tickets.Add(ticket);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Tickets.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Ticket ticket)
        {
            if (id != ticket.IdTicket)
            {
                return BadRequest("Ticket not found!");
            }

            _dataContext.Entry(ticket).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.Tickets.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketByid(int id)
        {
            var ticket = await _dataContext.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return BadRequest("Ticket not found");
            }

            _dataContext.Tickets.Remove(ticket);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.Tickets.ToListAsync());
        }
    }
}
