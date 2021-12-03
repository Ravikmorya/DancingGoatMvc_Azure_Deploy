﻿using System.Collections.Generic;
using System.Linq;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.DancingGoatMvc;

using Kentico.Content.Web.Mvc;

namespace DancingGoat.Models.Home
{
    public class HomeSectionViewModel
    {
        public string BackgroundImagePath { get; set; }
        public string Heading { get; set; }
        public string Text { get; set; }
        public string MoreButtonText { get; set; }
        public string MoreButtonUrl { get; set; }

        

        public static HomeSectionViewModel GetViewModel(HomeSection homeSection, IPageUrlRetriever pageUrlRetriever, IPageAttachmentUrlRetriever attachmentUrlRetriever)
        {
            var link = homeSection?.Fields.Link.FirstOrDefault();
            return homeSection == null ? null : new HomeSectionViewModel
            {
                BackgroundImagePath = attachmentUrlRetriever.Retrieve(homeSection.Fields.Image).RelativePath,
                Heading = homeSection.Fields.Heading,
                Text = homeSection.Fields.Text,
                MoreButtonText = homeSection.Fields.LinkText,
                MoreButtonUrl = link != null ? pageUrlRetriever.Retrieve(link).RelativePath : string.Empty
            };
        }

        
    }
}