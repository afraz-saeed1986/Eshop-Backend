using AngularEshop.DataLayer.Entities.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.DataLayer.Entities.Product
{
    public class ProductCategory: BaseEntity
    {
        #region properties
        public string Title { get; set; }
        public long? ParentId { get; set; }
        #endregion

        #region relations
        [ForeignKey("ParentId")]
        public ProductCategory ParentCategory { get; set; }
        public ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }
        #endregion
    }
}
