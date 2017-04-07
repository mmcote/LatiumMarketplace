using System.Collections.Generic;

namespace LatiumMarketplace.Models.IRepository
{
    /// <summary>
    /// Interface for Asset's Repository, contains basic add/remove/update functions
    /// </summary>
    public interface IAssetRepository
    {
        void Add(AssetViewModels.Asset Asset);
        IEnumerable<AssetViewModels.Asset> GetAll();
        AssetViewModels.Asset Find(int key);
        void Remove(int key);
        void Update(AssetViewModels.Asset Asset);
    }
}
