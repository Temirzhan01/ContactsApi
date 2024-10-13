using ContactsApi.Dto;
using ContactsApi.Models;

namespace ContactsApi.Services.Abstract
{
    public interface IContactsService
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact?> GetAsync(Guid guid);
        Task<IEnumerable<Contact>> GetByPaginationAsync(PaginationParameters parameters);
        Task<Contact> CreateAsync(ContactDto dto);
        Task UpdateAsync(Guid guid, ContactDto dto);
        Task<bool> DeleteAsync(Guid guid);
    }
}
