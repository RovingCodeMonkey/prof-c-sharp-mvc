using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Web.Data
{
    public class Products : IProducts
    {
        private readonly AppDbContext _context;
        public Products(AppDbContext context)
        {
            this._context = context;
        }    

        public async Task<IEnumerable<Models.Product>> Get()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Models.Product?> Get(int id)
        {
            return await _context.Products.Where(p => p.ProductId == id).FirstOrDefaultAsync();
        }

        public async Task<Models.Product> Create(Models.Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task Update(Models.Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await Get(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
