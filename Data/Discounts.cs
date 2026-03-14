using Microsoft.EntityFrameworkCore;

namespace Web.Data
{
    public class Discounts : IDiscounts
    {
        private readonly AppDbContext _context;
        public Discounts(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Models.Discount>> Get()
        {
            return await _context.Discounts.ToListAsync();
        }

        public async Task<Models.Discount?> Get(int id)
        {
            return await _context.Discounts.Where(d => d.DiscountId == id).FirstOrDefaultAsync();
        }

        public async Task<Models.Discount> Create(Models.Discount discount)
        {
            _context.Discounts.Add(discount);
            await _context.SaveChangesAsync();
            return discount;
        }

        public async Task Update(Models.Discount discount)
        {
            _context.Discounts.Update(discount);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var discount = await Get(id);
            if (discount != null)
            {
                _context.Discounts.Remove(discount);
                await _context.SaveChangesAsync();
            }
        }
    }
}
