﻿using AngularEshop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AngularEshop.DataLayer.Entities.Product
{
    public class ProductGallery: BaseEntity
    {
        #region properties
        public long ProductId { get; set; }
        [Display(Name = "نام تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string ImageName { get; set; }
        #endregion

        #region relations
        public Product Product { get; set; }
        #endregion
    }
}
