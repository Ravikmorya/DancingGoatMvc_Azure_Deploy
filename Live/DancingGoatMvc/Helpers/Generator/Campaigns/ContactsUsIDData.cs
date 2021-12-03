using System.Linq;

using CMS.DataEngine;

namespace DancingGoat.Helpers.Generator

/// <summary>
/// Represents IDs of all contactsUs whose first name starts with the same prefix.
/// </summary>
{
    internal class ContactsUsIDData
    {
        private int mCurrectContactUs;
        private readonly int mContactsUsCount;
        private readonly int[] mContactUsIDs;


        /// <summary>
        /// Creates the instance of <see cref="ContactsUsIDData"/>.
        /// </summary>
        /// <param name="firstNamePrefix">Commont first name prefix of contactsUs.</param>
        /// <param name="contactsUsCount">Represents number of contact IDs to retrieve. If value is <c>0</c> then all contact IDs with <paramref name="firstNamePrefix"/> are retrieved.</param>
        public ContactsUsIDData(string firstNamePrefix, int contactsUsCount)
        {

            mContactUsIDs = new ObjectQuery(PredefinedObjectType.CONTACT)
                .Column("ContactUsID")
                .WhereStartsWith("ContactUsFirstName", firstNamePrefix)
                .TopN(contactsUsCount)
                .GetListResult<int>()
                .ToArray();

            mCurrectContactUs = 0;
            mContactsUsCount = mContactUsIDs.Length;
        }



        /// <summary>
        /// Returns ID of the next contactUs.
        /// </summary>
        /// <returns>ContactUs ID.</returns>
        public int GetNextContactUsID()
        {
            var contactUsID = mContactUsIDs[mCurrectContactUs];
            mCurrectContactUs = (mCurrectContactUs + 1) % mContactsUsCount;

            return contactUsID;
        }
    }
}