using EcommPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommPortal.Repository
{
    public class CartRepository : ICartRepository
    {
		private readonly CartDbContext _context;
		public CartRepository(CartDbContext context)
		{
			this._context = context;
		}
		public async Task<int> AddCart(CartDto model)
		{
			await _context.Cart.AddAsync(model);
			int result = await _context.SaveChangesAsync();
			return result;
		}
	}
}
