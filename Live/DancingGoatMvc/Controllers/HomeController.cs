using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

using CMS.DocumentEngine.Types.DancingGoatMvc;

using DancingGoat.Controllers;
using DancingGoat.Infrastructure;
using DancingGoat.Models;
using DancingGoat.Models.Home;
using DancingGoat.Models.References;
using DancingGoat.Models.SliderBannerItems;
using DancingGoat.Repositories;
using DancingGoat.Services;
using Kentico.Content.Web.Mvc;
using Kentico.Content.Web.Mvc.Routing;

[assembly: RegisterPageRoute(Home.CLASS_NAME, typeof(HomeController))]

namespace DancingGoat.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IPageDataContextRetriever pageDataContextRetriever;
        private readonly IHomeRepository homeSectionRepository;
        //private readonly IReferenceRepository referenceRepository;
        private readonly ISliderBannerItemRepository sliderbanneritemRepository;
        private readonly IPageUrlRetriever pageUrlRetriever;
        private readonly IPageAttachmentUrlRetriever attachmentUrlRetriever;
        private readonly IOutputCacheDependencies outputCacheDependencies;


        public HomeController(IPageDataContextRetriever pageDataContextRetriever,
            IHomeRepository homeSectionRepository,
            IReferenceRepository referenceRepository,
            ISliderBannerItemRepository sliderbanneritemRepository,
            IOutputCacheDependencies outputCacheDependencies, 
            IPageUrlRetriever pageUrlRetriever,
            IPageAttachmentUrlRetriever attachmentUrlRetriever)

        {
            this.pageDataContextRetriever = pageDataContextRetriever;
            this.homeSectionRepository = homeSectionRepository;
            this.sliderbanneritemRepository = sliderbanneritemRepository;
            this.pageUrlRetriever = pageUrlRetriever;
            this.outputCacheDependencies = outputCacheDependencies;
            this.attachmentUrlRetriever = attachmentUrlRetriever;
        }
       

        // [OutputCache(CacheProfile = "PageBuilder")]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            //var bannerservice = new SliderBannerItemViewModel();  // for using Banner service
            var home = pageDataContextRetriever.Retrieve<Home>().Page;
            var homeSections = await homeSectionRepository.GetHomeSectionsAsync(home.NodeAliasPath, cancellationToken);
            var sliderimageitem = await sliderbanneritemRepository.GetSliderBannerItemsAsync(home.NodeAliasPath, cancellationToken);
            //var reference = (await referenceRepository.GetReferencesAsync(home.NodeAliasPath, cancellationToken, 1)).FirstOrDefault();

            var Model = new IndexViewModel
            {
                //SliderBannerItem = bannerservice.GetBanners().ToList(),
                HomeSections = homeSections.Select(section => HomeSectionViewModel.GetViewModel(section, pageUrlRetriever, attachmentUrlRetriever)),
                SliderBannerItem = sliderimageitem.Select(section => SliderBannerItemViewModel.GetViewModel(section, pageUrlRetriever, attachmentUrlRetriever)),
                //Reference = ReferenceViewModel.GetViewModel(reference, attachmentUrlRetriever),

            };
            



            outputCacheDependencies.AddDependencyOnPage(home);
            outputCacheDependencies.AddDependencyOnPages(homeSections);
           // outputCacheDependencies.AddDependencyOnPage(reference);
            
            return View(Model);
        }
    }
}
