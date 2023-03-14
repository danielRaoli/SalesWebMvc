using Microsoft.EntityFrameworkCore;
using SalesWebMvc1.Data;
using SalesWebMvc1.Models.Entities;

namespace SalesWebMvc1.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvc1Context _context;

        public SalesRecordService(SalesWebMvc1Context context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDate(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date.Year).ThenByDescending(o => o.Date.Month).ThenByDescending(o => o.Date.Day)
                .ToListAsync();
        }

        public List<IGrouping<Department, SalesRecord>> FindByGrouping(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(o => o.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(o => o.Date <= maxDate.Value);
            }
            return  result.Include(o => o.Seller)
                .Include(o => o.Seller.Department)
                .OrderByDescending(o => o.Date.Year).ThenByDescending(o => o.Date.Month).ThenByDescending(o => o.Date.Day)
                .AsEnumerable()
                .GroupBy(o => o.Seller.Department)
                .ToList();
        }

    }
}
