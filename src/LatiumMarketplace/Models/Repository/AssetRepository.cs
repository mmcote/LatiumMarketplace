using LatiumMarketplace.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LatiumMarketplace.Models.AssetViewModels;

namespace LatiumMarketplace.Models.Repository
{
    public class AssetRepository : IAssetRepository
    {
        private readonly AssetContext _context;

        public AssetRepository(AssetContext context)
        {
            _context = context;
            Add(new Asset { name = "Asset1" });
        }

        public void Add(Asset Asset)
        {
            _context.AssetItems.Add(Asset);
            _context.SaveChanges();
        }

        public Asset Find(int key)
        {
            return _context.AssetItems.FirstOrDefault(t => t.assetID == key);
        }

        public IEnumerable<Asset> GetAll()
        {
            return _context.AssetItems.ToList();
        }

        public void Remove(int key)
        {
            var entity = _context.AssetItems.First(t => t.assetID == key);
            _context.AssetItems.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Asset Asset)
        {
            _context.AssetItems.Update(Asset);
            _context.SaveChanges();
        }
    }
}
