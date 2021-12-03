using System;
using System.Linq;
using System.Web.Mvc;

using CMS.MediaLibrary;
using CMS.SiteProvider;

using Kentico.Content.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;

using DancingGoat.Controllers.Widgets;
using DancingGoat.Infrastructure;
using DancingGoat.Models.Widgets;
using DancingGoat.Repositories;

[assembly: RegisterWidget(StarkImageWidgetController.IDENTIFIER, typeof(StarkImageWidgetController), "{$Stark Image$}", Description = "{$dancinggoatmvc.widget.starkimage.description$}", IconClass = "icon-badge")]

namespace DancingGoat.Controllers.Widgets
{
    /// <summary>
    /// Controller for hero image widget.
    /// </summary>
    public class StarkImageWidgetController : WidgetController<StarkImageWidgetProperties>
    {
        /// <summary>
        /// Widget identifier.
        /// </summary>
        public const string IDENTIFIER = "DancingGoat.LandingPage.StarkImage";


        private readonly IMediaFileRepository mediaFileRepository;
        private readonly IOutputCacheDependencies outputCacheDependencies;
        private readonly IComponentPropertiesRetriever componentPropertiesRetriever;
        private readonly IMediaFileUrlRetriever mediaFileUrlRetriever;

        /// <summary>
        /// Creates an instance of <see cref="BannerWidgetController"/> class.
        /// </summary>
        public StarkImageWidgetController(
            IMediaFileRepository mediaFileRepository,
            IOutputCacheDependencies outputCacheDependencies,
            IComponentPropertiesRetriever componentPropertiesRetriever,
            IMediaFileUrlRetriever mediaFileUrlRetriever)
        {
            this.mediaFileRepository = mediaFileRepository;
            this.outputCacheDependencies = outputCacheDependencies;
            this.componentPropertiesRetriever = componentPropertiesRetriever;
            this.mediaFileUrlRetriever = mediaFileUrlRetriever;
        }


        public ActionResult Index()
        {
            var properties = componentPropertiesRetriever.Retrieve<StarkImageWidgetProperties>();
            var image = GetImage(properties);

            outputCacheDependencies.AddDependencyOnInfoObject<MediaFileInfo>(image?.FileGUID ?? Guid.Empty);

            return PartialView("Widgets/_StarkImageWidget", new StarkImageWidgetViewModel
            {
                ImagePath = image == null ? null : mediaFileUrlRetriever.Retrieve(image).RelativePath,
                Text = properties.Text,
                ButtonText = properties.ButtonText,
                ButtonTarget = properties.ButtonTarget,
                Theme = properties.Theme
            });
        }


        private MediaFileInfo GetImage(StarkImageWidgetProperties properties)
        {
            var imageGuid = properties.Image.FirstOrDefault()?.FileGuid ?? Guid.Empty;
            if (imageGuid == Guid.Empty)
            {
                return null;
            }

            return mediaFileRepository.GetMediaFile(imageGuid, SiteContext.CurrentSiteID);
        }

    }
}