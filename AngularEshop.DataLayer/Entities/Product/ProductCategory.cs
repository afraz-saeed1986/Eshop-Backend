using AngularEshop.DataLayer.Entities.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.DataLayer.Entities.Product
{
    public class ProductCategory: BaseEntity
    {
        #region properties
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string Title { get; set; }
        [Display(Name = "عنوان لینک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string UrlTitle { get; set; }
        public long? ParentId { get; set; }
        #endregion

        #region relations
        [ForeignKey("ParentId")]
        public ProductCategory ParentCategory { get; set; }
        public ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }
        #endregion
    }
}
