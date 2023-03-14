using Microsoft.EntityFrameworkCore;
using SalesWebMvc1.Data;
using SalesWebMvc1.Models.Entities;

namespace SalesWebMvc1.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvc1Context _context;

        public DepartmentService(SalesWebMvc1Context context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAll()
        {
            return await _context.Department.OrderBy(d => d.Name).ToListAsync();
        }
    }
}
