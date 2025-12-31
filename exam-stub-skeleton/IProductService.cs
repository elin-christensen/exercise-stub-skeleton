using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam_stub_skeleton
{
    internal interface IProductService
    {
        Product GetMostExpensive(List<Product> products);
    }
}
