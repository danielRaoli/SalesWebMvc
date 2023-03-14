using Microsoft.EntityFrameworkCore;
using SalesWebMvc1.Data;
using SalesWebMvc1.Models.Entities;
using SalesWebMvc1.Models.Entities.Exceptions;
using SalesWebMvc1.Services.Exceptions;
using System.Collections;

namespace SalesWebMvc1.Services
{
    public class SellerService
    {
        private readonly SalesWebMvc1Context _context;

        public SellerService(SalesWebMvc1Context context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAll()
        {
            return await _context.Seller.ToListAsync();
        }

        public async  Task AddSeller(Seller seller)
        {
            _context.Add(seller);
           await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindById(int id)
        {
            return await _context.Seller.Include(x => x.Department).FirstOrDefaultAsync(s => s.Id == id);
        }

        public  async Task DeletSeller(Seller seller)
        {
            try
            {
                _context.Seller.Remove(seller);
                await _context.SaveChangesAsync();
            }catch(DbUpdateException e)
            {
                throw new DbUpdateException(e.Message);
            }
            catch (IntegrityException e)
            {
                throw new IntegrityException(e.Message);
            }
            
        }

        public async Task Update(Seller seller)
        {
            bool hasAny = await _context.Seller.AnyAsync(s => s.Id == seller.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id Not Found");
            }
            try
            {
                _context.Update(seller);
              await _context.SaveChangesAsync();
            }
            catch(DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
            
        }
        

    }
}
