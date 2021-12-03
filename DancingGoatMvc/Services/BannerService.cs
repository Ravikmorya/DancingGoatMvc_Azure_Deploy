//using DancingGoat.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using CMS.DocumentEngine;
//using CMS.DocumentEngine.Types.DancingGoatMvc;
//using DancingGoat.Models.SliderBannerItems;

//namespace DancingGoat.Services
//{
//    public class BannerService : IService
//    {
//        public IEnumerable<SliderBannerItemViewModel> GetBanners()
//         {
//            return DocumentHelper.GetDocuments<SliderBannerItem>()
//                .AddColumns("BannerItemHeader1", "BannerItemHeader2", "BannerItemHeader3", "BannerItemCategory", "BannerItemImage")
//                .TopN(3)
//                .Select(x => new SliderBannerItemViewModel()
//                {
//                    Head1 = x.BannerItemHeader1,
//                    Head2 = x.BannerItemHeader2,
//                    Head3 = x.BannerItemHeader3,
//                    Category = x.BannerItemCategory,
//                    BanImage = x.Fields.BannerItemImage
//                });
//        }
//    }
//}