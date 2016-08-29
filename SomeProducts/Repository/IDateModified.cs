using System;

namespace SomeProducts.Repository
{
    interface IDateModified
    {
        DateTime CreateDate { get; set; }

        DateTime? ModifiedDate { get; set; }
    }
}
