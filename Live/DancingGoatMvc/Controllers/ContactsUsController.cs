using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using CMS.DocumentEngine.Types.DancingGoatMvc;

using Kentico.Content.Web.Mvc.Routing;

using DancingGoat.Controllers;
//using DancingGoat.Models.Contacts;
using DancingGoat.Repositories;
using DancingGoat.Models.ContactsUs;
using IndexViewModel = DancingGoat.Models.ContactsUs.IndexViewModel;

[assembly: RegisterPageRoute(ContactsUs.CLASS_NAME, typeof(ContactsUsController))]

namespace DancingGoat.Controllers
{
    public class ContactsUsController : Controller
    {
        private readonly ICafeRepository cafeRepository;
        //private readonly IContactRepository contactRepository;
        private readonly IContactUsRepository contactUsRepository;
        private readonly ICountryRepository countryRepository;


        public ContactsUsController(ICafeRepository cafeRepository,
           // IContactRepository contactRepository,
             IContactUsRepository contactUsRepository,
            ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
            this.cafeRepository = cafeRepository;
            //this.contactRepository = contactRepository;
            this.contactUsRepository = contactUsRepository;
        }


        public ActionResult Index()
        {
            var model = GetIndexViewModel();

            return View(model);
        }


        private IndexViewModel GetIndexViewModel()
        {
            var cafes = cafeRepository.GetCompanyCafes(ContentItemIdentifiers.CAFES, 4);

            return new IndexViewModel
            {
                //CompanyContact = GetCompanyContactModel(),
                //CompanyCafes = GetCompanyCafesModel(cafes)
            };
        }


        //private ContactUsViewModel GetCompanyContactModel()
        //{
        //    return ContactUsViewModel.GetViewModel(contactUsRepository.GetCompanyContactUs(), countryRepository);
        //}


        //private List<ContactUsViewModel> GetCompanyCafesModel(IEnumerable<Cafe> cafes)
        //{
        //    return cafes.Select(cafe => ContactUsViewModel.GetViewModel(cafe, countryRepository)).ToList();
        //}
    }
}