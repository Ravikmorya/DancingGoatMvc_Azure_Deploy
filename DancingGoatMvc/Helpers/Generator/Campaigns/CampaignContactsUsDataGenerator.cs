using System.Linq;

using CMS.ContactManagement;

namespace DancingGoat.Helpers.Generator
{
    internal class CampaignContactsUsDataGenerator
    {
        private const int NUMBER_OF_GENERATED_CONTACTSUS = 531;
        private readonly string mContactUsFirstNamePrefix;
        private readonly string mContactUsLastNamePrefix;

        /// <summary>
        /// Constructor.
        /// </summary>        
        public CampaignContactsUsDataGenerator(string contactUsFirstNamePrefix, string contactUsLastNamePrefix)
        {
            mContactUsFirstNamePrefix = contactUsFirstNamePrefix;
            mContactUsLastNamePrefix = contactUsLastNamePrefix;
        }

        /// <summary>
        /// Performs campaign contactsUs sample data generating.
        /// </summary>
        public void Generate()
        {
            //DeleteOldContactsUs();
            //GenerateContactsUs();
        }


        //private void DeleteOldContactsUs()
        //{
        //    ContactUsInfo.Provider.Get()
        //                       .WhereStartsWith("ContactUsFirstName", mContactUsFirstNamePrefix)
        //                       .ToList()
        //                       .ForEach(ContactUsInfo.Provider.Delete);
        //}


        //private void GenerateContactsUs()
        //{
        //    for (var i = 0; i < NUMBER_OF_GENERATED_CONTACTSUS; i++)
        //    {
        //        var contactUs = new ContactUsInfo()
        //        {
        //            ContactUsFirstName = mContactUsFirstNamePrefix + i,
        //            ContactUsLastName = mContactUsLastNamePrefix + i,
        //            ContactUsEmail = string.Format("{0}{1}@localhost.local", mContactUsFirstNamePrefix, i)
        //        };
        //        ContactUsInfo.Provider.Set(contactUs);
        //    }
        //}
    }
}