using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstraction.Contracts
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
    }
}
