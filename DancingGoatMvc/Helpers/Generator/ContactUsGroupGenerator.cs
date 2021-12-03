using CMS.ContactManagement;

namespace DancingGoat.Helpers.Generator
{

    /// <summary>
    /// Contains methods for generating sample contactUs groups.
    /// </summary>
    internal class ContactUsGroupGenerator
    {
        /// <summary>
        /// Generates three empty contactUs groups.
        /// </summary>
        /// 
        public void Generate()
        {
            CreateContactUsGroup("CoffeeClubMembershipRecipients", "Coffee club membership recipients");
            CreateContactUsGroup("ProspectiveClients", "Prospective clients");
            CreateContactUsGroup("ImportedContactsUs", "Imported contactsUs");
        }


        private static void CreateContactUsGroup(string contactUsGroupCodeName, string contactUsGroupName)
        {
            var contactUsGroup = ContactGroupInfo.Provider.Get(contactUsGroupCodeName);
            if (contactUsGroup != null)
            {
                return;
            }

            contactUsGroup = new ContactGroupInfo
            {
                ContactGroupDisplayName = contactUsGroupName,
                ContactGroupName = contactUsGroupCodeName,
                ContactGroupEnabled = true
            };
            ContactGroupInfo.Provider.Set(contactUsGroup);
        }
    }
}