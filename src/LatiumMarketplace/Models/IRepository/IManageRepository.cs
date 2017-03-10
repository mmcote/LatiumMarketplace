using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.IRepository
{
    interface IManageRepository
    {
        //void Add(AssetViewModels.Asset Asset);
        //IEnumerable<AssetViewModels.Asset> GetAll();
        //AssetViewModels.Asset Find(int key);
        //void Remove(int key);
        void Update(ApplicationUser User);
    }
}
