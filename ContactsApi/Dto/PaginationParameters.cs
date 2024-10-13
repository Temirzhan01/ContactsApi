using System.ComponentModel.DataAnnotations;

namespace ContactsApi.Dto
{
    public class PaginationParameters
    {
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0")]
        public int PageNumber { get; set; }
        [Range(1, 50, ErrorMessage = "Page number must be greater than 0")]
        public int PageSize { get; set; }
    }
}
