//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Text;

//namespace MicShop.Core.Entities
//{
//    public class ReviewModel
//    {
//        public int ID { get; set; }
//        public UserModel User { get; set; }
//        public ProductModel Product { get; set; }
//        [Range(1, 5)]
//        public int Star { get; set; }
//        [StringLength(255, MinimumLength = 0)]
//        [DataType(DataType.MultilineText)]
//        public string Review { get; set; }
//    }
//}
