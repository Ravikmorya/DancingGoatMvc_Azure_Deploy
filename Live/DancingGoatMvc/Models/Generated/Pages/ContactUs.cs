using DancingGoat.Models.Contacts;
//using DancingGoat.Models.ContactsUs;

//namespace DancingGoat.Models.Generated.Pages
namespace CMS.DocumentEngine.Types.DancingGoatMvc
{
    public partial class ContactUs : IContact
    {
        public new string Name
        {
            get
            {
                return Fields.Name;
            }
        }


        public string Phone
        {
            get
            {
                return Fields.Phone;
            }
        }


        public string Email
        {
            get
            {
                return Fields.Email;
            }
        }


        public string ZIP
        {
            get
            {
                return Fields.ZipCode;
            }
        }


        public string Street
        {
            get
            {
                return Fields.Street;
            }
        }


        public string City
        {
            get
            {
                return Fields.City;
            }
        }


        public string Country
        {
            get
            {
                return Fields.Country;
            }
        }
    }
}