﻿using System;

namespace SomeProducts.DAL.Repository.Interface
{
    public interface IDateModified
    {
        DateTime CreateDate { get; set; }

        DateTime? ModifiedDate { get; set; }
    }
}