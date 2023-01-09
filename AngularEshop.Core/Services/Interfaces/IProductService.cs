using AngularEshop.DataLayer.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.Core.Services.Interfaces
{
    public interface IProductService:IDisposable
    {
        #region product
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        #endregion
    }
}
