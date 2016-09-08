using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeProducts.CrossCutting.Intefaces
{
    public interface IImageModel
    {
        byte[] Image { get; set; }

        string ImageType { get; set; }
    }
}
