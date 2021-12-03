using System;
using System.Linq;

using CMS.Activities;
using CMS.Base;
using CMS.ContactManagement;
using CMS.Core;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.FormEngine;
using CMS.Globalization;
using CMS.Helpers;
using CMS.Membership;
using CMS.OnlineForms;
using CMS.WebAnalytics.Internal;

namespace DancingGoat.Helpers.Generator
{
    internal class OnlineMarketingDataGenerator
    {
        private const string CEO_CONTACT_ROLE = "CEO";
        private const string BARISTA_CONTACT_ROLE = "Barista";

        private const string CEO_CONTACTUS_ROLE = "CEO";
        private const string BARISTA_CONTACTUS_ROLE = "Barista";

        private const string CONTACT_US_FORM_CODE_NAME = "DancingGoatMvcContactUsNew";
        private const string CONTACTUS_US_FORM_CODE_NAME = "DancingGoatMvcContactUsNew";
        private const string COFFEE_SAMPLE_LIST_FORM_CODE_NAME = "DancingGoatMvcCoffeeSampleList";

        private readonly TreeNode contactsDocument = DocumentHelper.GetDocuments()
                                                                       .All()
                                                                       .Culture("en-US")
                                                                       .Path(ContentItemIdentifiers.CONTACTS)
                                                                       .OnCurrentSite()
                                                                       .TopN(1)
                                                                       .FirstOrDefault();
        //private readonly TreeNode contactsUsDocument = DocumentHelper.GetDocuments()
        //                                                               .All()
        //                                                               .Culture("en-US")
        //                                                               .Path(ContentItemIdentifiers.CONTACTSUS)
        //                                                               .OnCurrentSite()
        //                                                               .TopN(1)
        //                                                               .FirstOrDefault();

        private readonly TreeNode coffeeSamplesDocument = DocumentHelper.GetDocuments()
                                                                       .All()
                                                                       .Culture("en-US")
                                                                       .Path(ContentItemIdentifiers.LANDINGPAGE_COFFEESAMPLES)
                                                                       .OnCurrentSite()
                                                                       .TopN(1)
                                                                       .FirstOrDefault();

        private ISiteService siteService = Service.Resolve<ISiteService>();
        private ISiteInfo currentSite => siteService.CurrentSite;
        private ActivityTitleBuilder nameBuilder = new ActivityTitleBuilder();


        /// <summary>
        /// Performs contacts and activities generation.
        /// </summary>
        public void Generate()
        {
            GenerateMonicaKing();
            GenerateDustinEvans();
            GenerateHaroldLarson();
            GenerateToddRay();
            GenerateStacyStewart();
        }


        private void GenerateMonicaKing()
        {
            var monicaKing = GenerateContact("Monica", "King", "monica.king@localhost.local", "(595)-721-1648");
            //monicaKing = GenerateContactUS("Monica", "King", "monica.king@localhost.local", "(595)-721-1648");

            monicaKing.ContactBirthday = DateTime.Today.AddYears(-35);
            monicaKing.ContactGender = (int)UserGenderEnum.Female;
            monicaKing.ContactJobTitle = BARISTA_CONTACT_ROLE;
            monicaKing.ContactMobilePhone = "+420123456789";

            //monicaKing.ContactUsBirthday = DateTime.Today.AddYears(-35);
            //monicaKing.ContactUsGender = (int)UserGenderEnum.Female;
            //monicaKing.ContactUsJobTitle = BARISTA_CONTACTUS_ROLE;
            //monicaKing.ContactUsMobilePhone = "+987654321";

            monicaKing.ContactCity = "Brno";
            monicaKing.ContactAddress1 = "New Market 187/5";
            monicaKing.ContactZIP = "602 00";
            monicaKing.ContactCompanyName = "Air Cafe";
            monicaKing.ContactCountryID = CountryInfo.Provider.Get("CzechRepublic").CountryID;
            monicaKing.ContactNotes = "Should be involved in every communication with Air Cafe.";

            //monicaKing.ContactUsCity = "Brno";
            //monicaKing.ContactUsAddress1 = "New Market 187/5";
            //monicaKing.ContactUsZIP = "602 00";
            //monicaKing.ContactUsCompanyName = "Air Cafe";
            //monicaKing.ContactUsCountryID = CountryInfo.Provider.Get("CzechRepublic").CountryID;
            //monicaKing.ContactUsNotes = "Should be involved in every communication with Air Cafe.";

            ContactInfo.Provider.Set(monicaKing);
            //ContactUsInfo.Provider.Set(monicaKing);

            //GeneratePageVisitActivity(contactsDocument, monicaKing);
            //GeneratePageVisitActivity(contactsUsDocument, monicaKing);
            //GeneratePageVisitActivity(coffeeSamplesDocument, monicaKing);
            //CreateFormSubmission(coffeeSamplesDocument, COFFEE_SAMPLE_LIST_FORM_CODE_NAME, monicaKing);
            //CreateFormSubmission(contactsDocument, CONTACT_US_FORM_CODE_NAME, monicaKing);
            //CreateFormSubmission(contactsUsDocument, CONTACTUS_US_FORM_CODE_NAME, monicaKing);
            //GeneratePurchaseActivity(20, monicaKing);
        }
        

        private void GenerateDustinEvans()
        {
            var dustinEvans = GenerateContact("Dustin", "Evans", "Dustin.Evans@localhost.local", "(808)-139-4639");

            //dustinEvans = GenerateContactUs("Dustin", "Evans", "Dustin.Evans@localhost.local", "(808)-139-4639");

            dustinEvans.ContactBirthday = DateTime.Today.AddYears(-40);
            dustinEvans.ContactGender = (int)UserGenderEnum.Male;
            dustinEvans.ContactJobTitle = CEO_CONTACT_ROLE;
            dustinEvans.ContactMobilePhone = "+420123456789";
            dustinEvans.ContactNotes = "Willing to participate in the partnership program - materials sent";

            //ADd contact Us
            //dustinEvans.ContactUsBirthday = DateTime.Today.AddYears(-40);
            //dustinEvans.ContactUsGender = (int)UserGenderEnum.Male;
            //dustinEvans.ContactUsJobTitle = CEO_CONTACTUS_ROLE;
            //dustinEvans.ContactUsMobilePhone = "+420123456789";
            //dustinEvans.ContactUsNotes = "Willing to participate in the partnership program - materials sent";

            dustinEvans.ContactCity = "South Yarra";
            dustinEvans.ContactAddress1 = "163 Commercial Road";
            dustinEvans.ContactZIP = "VIC 3141";
            dustinEvans.ContactCompanyName = "Jasper Coffee";
            dustinEvans.ContactCountryID = CountryInfo.Provider.Get("Australia").CountryID;
            dustinEvans.ContactNotes = "Willing to participate in the partnership program - materials sent";

            //ADd contact Us

            //dustinEvans.ContactUsCity = "South Yarra";
            //dustinEvans.ContactUsAddress1 = "163 Commercial Road";
            //dustinEvans.ContactUsZIP = "VIC 3141";
            //dustinEvans.ContactUsCompanyName = "Jasper Coffee";
            //dustinEvans.ContactUsCountryID = CountryInfo.Provider.Get("Australia").CountryID;
            //dustinEvans.ContactUsNotes = "Willing to participate in the partnership program - materials sent";

            ContactInfo.Provider.Set(dustinEvans);
           // ContactUsInfo.Provider.Set(dustinEvans);
        }


        private void GenerateHaroldLarson()
        {
            var haroldLarson = GenerateContact("Harold", "Larson", "Harold.Larson@localhost.local", "(742)-343-5223");
            //haroldLarson = GenerateContactUs("Harold", "Larson", "Harold.Larson@localhost.local", "(742)-343-5223");

            haroldLarson.ContactGender = (int)UserGenderEnum.Male;
            haroldLarson.ContactCountryID = CountryInfo.Provider.Get("USA").CountryID;
            haroldLarson.ContactCity = "Bedford";
            haroldLarson.ContactBounces = 5;
            haroldLarson.ContactStateID = StateInfo.Provider.Get("NewHampshire").StateID;

            //ADd contact Us
            //haroldLarson.ContactUsGender = (int)UserGenderEnum.Male;
            //haroldLarson.ContactUsCountryID = CountryInfo.Provider.Get("USA").CountryID;
            //haroldLarson.ContactUsCity = "Bedford";
            //haroldLarson.ContactUsBounces = 5;
            //haroldLarson.ContactUsStateID = StateInfo.Provider.Get("NewHampshire").StateID;

            ContactInfo.Provider.Set(haroldLarson);
            //ContactUsInfo.Provider.Set(haroldLarson);

        }


        private void GenerateStacyStewart()
        {
            var stacyStewart = GenerateContact("Stacy", "Stewart", "Stacy.Stewart@localhost.local", null);
            //stacyStewart = GenerateContactUs("Stacy", "Stewart", "Stacy.Stewart@localhost.local", null);

            stacyStewart.ContactCountryID = CountryInfo.Provider.Get("Germany").CountryID;
            stacyStewart.ContactCity = "Berlin";
            stacyStewart.ContactNotes = "Contact acquired at CoffeeExpo2015";

            //ADd contact Us
            //stacyStewart.ContactUsCountryID = CountryInfo.Provider.Get("Germany").CountryID;
            //stacyStewart.ContactUsCity = "Berlin";
            //stacyStewart.ContactUsNotes = "ContactUs acquired at CoffeeExpo2015";

            ContactInfo.Provider.Set(stacyStewart);
            //ContactUsInfo.Provider.Set(stacyStewart);
        }


        private void GenerateToddRay()
        {
            var toddRay = GenerateContact("Todd", "Ray", "Todd.Ray@localhost.local", "(808)-289-4459");
           // toddRay = GenerateContactUs("Todd", "Ray", "Todd.Ray@localhost.local", "(808)-289-4459");

            toddRay.ContactBirthday = DateTime.Today.AddYears(-42);
            toddRay.ContactGender = (int)UserGenderEnum.Male;
            toddRay.ContactMobilePhone = "+420123456789";

            //toddRay.ContactUsBirthday = DateTime.Today.AddYears(-42);
            //toddRay.ContactUsGender = (int)UserGenderEnum.Male;
            //toddRay.ContactUsMobilePhone = "+420123456789";

            toddRay.ContactCity = "Brno";
            toddRay.ContactAddress1 = "Benesova 13";
            toddRay.ContactZIP = "612 00";
            toddRay.ContactCompanyName = "Air Cafe";
            toddRay.ContactCountryID = CountryInfo.Provider.Get("CzechRepublic").CountryID;

            //toddRay.ContactUsNotes = "Should be involved in every communication with Air Cafe.";
            //toddRay.ContactUsCity = "Brno";
            //toddRay.ContactUsAddress1 = "Benesova 13";
            //toddRay.ContactUsZIP = "612 00";
            //toddRay.ContactUsCompanyName = "Air Cafe";
            //toddRay.ContactUsCountryID = CountryInfo.Provider.Get("CzechRepublic").CountryID;
            //toddRay.ContactUsNotes = "Should be involved in every communication with Air Cafe.";

            ContactInfo.Provider.Set(toddRay);
            //ContactInfo.Provider.Set(toddRay);
        }


        private ContactInfo GenerateContact(string firstName, string lastName, string email, string businessPhone)
        {
            var contact = new ContactInfo
            {
                ContactFirstName = firstName,
                ContactLastName = lastName,
                ContactEmail = email,
                ContactBusinessPhone = businessPhone,
                ContactMonitored = true
            };
            ContactInfo.Provider.Set(contact);
            return contact;
        }

        //ADD contact us
        //private ContactInfo GenerateContactUs(string firstName, string lastName, string email, string businessPhone)
        //{
        //    var contactUs = new ContactInfo
        //    {
        //        ContactFirstName = firstName,
        //        ContactUsLastName = lastName,
        //        ContactUsEmail = email,
        //        ContactUsBusinessPhone = businessPhone,
        //        ContactUsMonitored = true
        //    };
        //    ContactInfo.Provider.Set(contactUs);
        //    return contactUs;
        //}


        private void GeneratePageVisitActivity(TreeNode document, ContactInfo contact, ContactInfo contactUs)
        {
            var nameBuilder = new ActivityTitleBuilder();
            var hashService = Service.Resolve<IActivityUrlHashService>();
            var url = DocumentURLProvider.GetAbsoluteUrl(document);

            var activity = new ActivityInfo
            {
                ActivityContactID = contact.ContactID,
                //ActivityContactID = contactUs.ContactID,
                ActivitySiteID = currentSite.SiteID,
                ActivityTitle = nameBuilder.CreateTitle(PredefinedActivityType.PAGE_VISIT, document.GetDocumentName()),
                ActivityType = PredefinedActivityType.PAGE_VISIT,
                ActivityURL = url,
                ActivityURLHash = hashService.GetActivityUrlHash(url)
            };

            ActivityInfo.Provider.Set(activity);
        }


        private void CreateFormSubmission(TreeNode document, string formName, ContactInfo contact, ContactInfo contactUs)
        {
            
            var form = BizFormInfo.Provider.Get(formName, currentSite.SiteID);
            var classInfo = DataClassInfoProvider.GetDataClassInfo(form.FormClassID);
            var formItem = new BizFormItem(classInfo.ClassName);
            var url = DocumentURLProvider.GetAbsoluteUrl(document);

            CopyDataFromContactToForm(contact, classInfo, formItem);
            CopyDataFromContactToForm(contactUs, classInfo, formItem);
            SetFormSpecificData(formName, contact, formItem);
            SetFormSpecificData(formName, contactUs, formItem);
            formItem.Insert();

            var activity = new ActivityInfo
            {
                ActivityContactID = contact.ContactID,
                //ActivityContactID = contactUs.ContactUsID,
                ActivityItemID = form.FormID,
                ActivityItemDetailID = formItem.ItemID,
                ActivitySiteID = currentSite.SiteID,
                ActivityTitle = nameBuilder.CreateTitle(PredefinedActivityType.BIZFORM_SUBMIT, form.FormDisplayName),
                ActivityType = PredefinedActivityType.BIZFORM_SUBMIT,
                ActivityURL = url
            };

            ActivityInfo.Provider.Set(activity);
        }


        private void CopyDataFromContactToForm(ContactInfo contact, DataClassInfo classInfo, BizFormItem formItem)
        {
            var mapInfo = new FormInfo(classInfo.ClassContactMapping);
            var fields = mapInfo.GetFields(true, true);
            foreach (FormFieldInfo ffi in fields)
            {
                formItem.SetValue(ffi.MappedToField, contact.GetStringValue(ffi.Name, string.Empty));
            }
        }

        // add contactUs
        private void SetFormSpecificData(string formName, ContactInfo contactUs, BizFormItem formItem)
        {
            if (formName == COFFEE_SAMPLE_LIST_FORM_CODE_NAME)
            {
                formItem.SetValue("Country", CountryInfo.Provider.Get(contactUs.ContactCountryID).CountryThreeLetterCode);
                var state = StateInfo.Provider.Get(contactUs.ContactStateID);
                var stateName = state != null ? state.StateDisplayName : string.Empty;
                formItem.SetValue("State", stateName);
            }
            if (formName == CONTACTUS_US_FORM_CODE_NAME)
            {
                formItem.SetValue("UserMessage", "Message");
            }
        }


        private void GeneratePurchaseActivity(double spent, ContactInfo contact, ContactInfo contactUs)
        {            
            var activity = new ActivityInfo
            {
                ActivityTitle = nameBuilder.CreateTitle(PredefinedActivityType.PURCHASE, "$" + spent),
                ActivityValue = spent.ToString(CultureHelper.EnglishCulture),
                ActivityItemID = 0,
                ActivityType = PredefinedActivityType.PURCHASE,
                ActivitySiteID = currentSite.SiteID,
                ActivityContactID = contact.ContactID
                //ActivityContactUsID = contactUs.ContactUsID
            };

            ActivityInfo.Provider.Set(activity);
        }
    }
}