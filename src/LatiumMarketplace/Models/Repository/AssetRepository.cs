using LatiumMarketplace.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LatiumMarketplace.Models.AssetViewModels;
using LatiumMarketplace.Data;

namespace LatiumMarketplace.Models.Repository
{
    /// <summary>
    /// Asset's Repository, contains basic add/update functions
    /// </summary>
    public class AssetRepository : IAssetRepository
    {
        private readonly ApplicationDbContext _context;

        public AssetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Asset Asset)
        {
            _context.Add(Asset);
            _context.SaveChanges();
            return;
        }

        public Asset Find(int key)
        {
            if (key == 0)
            {
                throw new ArgumentNullException("The assetID given was null. No assetID's are null.");
            }

            var asset = _context.Asset.Single(m => m.assetID == key);
            if (asset == null)
            {
                throw new KeyNotFoundException("No asset found by the given assetID");
            }

            return asset;
        }

        public IEnumerable<Asset> GetAll()
        {
            return _context.Asset.ToList();
        }

        public void Remove(int key)
        {
            if (key == 0)
            {
                throw new ArgumentNullException("The assetID given was null. No assetID's are null.");
            }

            var asset = _context.Asset.Single(t => t.assetID == key);
            if (asset == null)
            {
                throw new Exception("No asset found by the given assetID");
            };
            _context.Asset.Remove(asset);
            _context.SaveChanges();
            return;
        }

        public void Update(Asset Asset)
        {
            _context.Update(Asset);
            _context.SaveChanges();
            return;
        }

    }
}
