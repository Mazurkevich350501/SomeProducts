﻿using System;
using System.Collections.Generic;
using SomeProducts.DAL.Models;

namespace SomeProducts.DAL.IDao
{
    public interface IBrandDao
    {
        IEnumerable<Brand> GetAllItems();

        void RemoveBrand(int id);

        void CreateBrand(Brand brand);

        bool IsBrandUsing(int id);

        DateTime GetCreateTime(int id);

        bool UpdateBrand(Brand brand);
    }
}
