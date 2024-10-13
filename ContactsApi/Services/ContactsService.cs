using AutoMapper;
using ContactsApi.Data;
using ContactsApi.Dto;
using ContactsApi.Models;
using ContactsApi.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ContactsApi.Services
{
    public class ContactsService : IContactsService
    {
        private readonly ILogger<ContactsService> _logger;
        private readonly ContactsDbContext _context;
        private readonly IMapper _mapper;

        public ContactsService(ILogger<ContactsService> logger, ContactsDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            try
            {
                return await _context.Contacts.AsNoTracking().ToListAsync();
            }
            catch (Exception ex) 
            {
                _logger.LogError("GetAllAsync/Error message: {0}", ex.Message);
                throw;
            }
        }

        public async Task<Contact?> GetAsync(Guid guid)
        {
            try
            {
                return await _context.Contacts.AsNoTracking().FirstOrDefaultAsync(c => c.Guid == guid);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAsync/Guid: {0}, Error message: {1}", guid, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Contact>> GetByPaginationAsync(PaginationParameters parameters)
        {
            try
            {
                var query = _context.Contacts.AsNoTracking().AsQueryable();
                var totalCount = await query.CountAsync();

                var result = await query.Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize).ToListAsync();

                return result;
            }
            catch (Exception ex) 
            {
                _logger.LogError("GetByPaginationAsync/Parameters: {0}, Error message: {1}", JsonConvert.SerializeObject(parameters), ex.Message);
                throw;
            }
        }

        public async Task<Contact> CreateAsync(ContactDto model)
        {
            try
            {
                var contact = _mapper.Map<Contact>(model);

                var result = await _context.Contacts.AddAsync(contact);
                await _context.SaveChangesAsync();

                return result.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError("CreateAsync/Model: {0}, Error message: {1}", JsonConvert.SerializeObject(model), ex.Message);
                throw;
            }
        }

        public async Task UpdateAsync(Guid guid, ContactDto model)
        {
            try
            {
                var contact = _mapper.Map<Contact>(model);
                contact.Guid = guid;

                _context.Contacts.Update(contact);
                await _context.SaveChangesAsync();

                return;
            }
            catch (Exception ex)
            {
                _logger.LogError("UpdateAsync/Model: {0}, Error message: {1}", JsonConvert.SerializeObject(model), ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid guid)
        {
            try
            {
                var contact = await _context.Contacts.AsNoTracking().FirstOrDefaultAsync(c => c.Guid == guid);

                if (contact == null) 
                {
                    _logger.LogInformation("DeleteAsync/Contact with guid: {0} not found ", guid);
                    return false;
                }

                var result = _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("DeleteAsync/Guid: {0}, Error message: {1}", guid, ex.Message);
                throw;
            }
        }

    }
}
