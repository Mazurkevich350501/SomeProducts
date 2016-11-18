using System;

namespace SomeProducts.DAL.Repository.Interface.Model
{
    public interface IDateModified
    {
        int Id { get; set; }

        byte[] RowVersion { get; set; }

        DateTime CreateDate { get; set; }

        DateTime? ModifiedDate { get; set; }
    }
}
