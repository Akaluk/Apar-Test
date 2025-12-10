using Microsoft.EntityFrameworkCore;
using ServiceAPI.Data;
using ServiceAPI.Models;

namespace ServiceAPI.Services
{
    public class ItemService
    {
        private readonly AppDbContext _db;

        public ItemService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Item>> GetAllAsync() => await _db.Items.ToListAsync();

        public async Task AddAsync(Item item)
        {
            _db.Items.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task ApproveAsync(List<int> ids, string reason)
        {
            var items = await _db.Items.Where(x => ids.Contains(x.Id)).ToListAsync();
            foreach (var item in items)
            {
                item.Status = "approved";
                item.Reason = reason;
            }
            await _db.SaveChangesAsync();
        }

        public async Task RejectAsync(List<int> ids, string reason)
        {
            var items = await _db.Items.Where(x => ids.Contains(x.Id)).ToListAsync();
            foreach (var item in items)
            {
                item.Status = "rejected";
                item.Reason = reason;
            }
            await _db.SaveChangesAsync();
        }
    }
}