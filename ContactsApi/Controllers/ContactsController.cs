using ContactsApi.Dto;
using ContactsApi.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService _contactsService;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController (IContactsService contactsService, ILogger<ContactsController> logger)
        {
            _contactsService = contactsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            var result = await _contactsService.GetAllAsync();
            if (!result.Any()) 
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("{guid:guid}")]
        public async Task<IActionResult> Get(Guid guid) 
        {
            _logger.LogInformation("Get/Guid: {0}", guid);

            var result = await _contactsService.GetAsync(guid);
            if (result == null) 
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> Get([FromQuery] PaginationParameters parameters) 
        {
            _logger.LogInformation("Get/Page number: {0}, page size: {1}", parameters.PageNumber, parameters.PageSize);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _contactsService.GetByPaginationAsync(parameters);
            if (!result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactDto model) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var result = await _contactsService.CreateAsync(model);
            return CreatedAtAction(nameof(Get), result.Guid, result);
        }

        [HttpPut("{guid:guid}")]
        public async Task<IActionResult> Put(Guid guid, [FromBody] ContactDto model) 
        {
            _logger.LogInformation("Put/Guid: {0}", guid);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var current = await _contactsService.GetAsync(guid);
            if (current == null) 
            {
                return NotFound();
            }

            await _contactsService.UpdateAsync(guid, model);
            return NoContent();
        }

        [HttpDelete("{guid:guid}")]
        public async Task<IActionResult> Delete(Guid guid) 
        {
            _logger.LogInformation("Delete/Guid: {0}", guid);

            var result = await _contactsService.DeleteAsync(guid);
            if (!result) 
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
