using System;

namespace SomeProducts.Repository
{
    public interface IDateModified
    {
        DateTime CreateDate { get; set; }

        DateTime? ModifiedDate { get; set; }
    }
}
