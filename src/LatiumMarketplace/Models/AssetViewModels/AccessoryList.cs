﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.AssetViewModels
{
    /// <summary>
    /// AccessoryList Model contains a list of accessories, and id, and a
    /// foreign key reference to the asset it is asociaited with
    /// </summary>
    public class AccessoryList
    {
        [Key]
        public int AccessoryListId { get; set; }

        // Navigation properties for one-to-many relationship between Accessory and AccessoryList.
        // One accessoryList can have many accessories
        public ICollection<Accessory> Accessories { get; set; }

        // Navigation properties for one-to-one relationship between Asset and AccessoryList.
        // One accessoryList only has one asset
        public Asset Asset { get; set; }
    }
}
