using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeProducts.DAL.Repository
{
    public interface IIdentify
    {
        int Id { get; set; }

        byte[] RowVersion { get; set; }
    }
}
