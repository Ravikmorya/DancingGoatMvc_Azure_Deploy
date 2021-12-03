using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.DancingGoatMvc;
using Kentico.Content.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace DancingGoat.Models.SliderBannerItems
{
    public class SliderBannerItemViewModel
    {
       
			public string Head1 { get; set; }
			public string Head2 { get; set; }
			public string Head3 { get; set; }
			public string Category { get; set; }
            public string BanImage { get; set; }

        //public DocumentAttachment BanImage { get; set; }

        //public IEnumerable<SliderBannerItemViewModel> GetBanners()
        //{
        //    return DocumentHelper.GetDocuments<SliderBannerItem>()
        //        .AddColumns("BannerItemHeader1", "BannerItemHeader2", "BannerItemHeader3", "BannerItemCategory", "BannerItemImage")
        //        .TopN(3)
        //        .Select(x => new SliderBannerItemViewModel()
        //        {
        //            Head1 = x.BannerItemHeader1,
        //            Head2 = x.BannerItemHeader2,
        //            Head3 = x.BannerItemHeader3,
        //            Category = x.BannerItemCategory,
        //            BanImage = x.Fields.BannerItemImage
        //        });
        //}

        public static SliderBannerItemViewModel GetViewModel(SliderBannerItem sliderimageitem, IPageUrlRetriever pageUrlRetriever,  IPageAttachmentUrlRetriever attachmentUrlRetriever)
        {
            //var link = sliderimageitem?.Fields.Link.FirstOrDefault();
            return sliderimageitem == null ? null : new SliderBannerItemViewModel
            {
                BanImage = attachmentUrlRetriever.Retrieve(sliderimageitem.Fields.BannerItemImage).RelativePath,
                Head1 = sliderimageitem.Fields.BannerItemHeader1,
                Head2 = sliderimageitem.Fields.BannerItemHeader2,
                Head3 = sliderimageitem.Fields.BannerItemHeader3,
                Category = sliderimageitem.Fields.BannerItemCategory
                //MoreButtonUrl = link != null ? pageUrlRetriever.Retrieve(link).RelativePath : string.Empty
            };
        }
    }
}
