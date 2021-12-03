using CMS.Helpers;

using DancingGoat.Repositories;

namespace DancingGoat.Models.ContactsUs
{
    public class ContactUsViewModel
    {
        public string Name { get; set; }


        public string Phone { get; set; }


        public string Email { get; set; }


        public string ZIP { get; set; }


        public string Street { get; set; }


        public string City { get; set; }


        public string Country { get; set; }


        public string CountryCode { get; set; }


        public string State { get; set; }


        public string StateCode { get; set; }

        public ContactUsViewModel(IContactUs contactus)
        {
            Name = contactus.Name;
            Phone = contactus.Phone;
            Email = contactus.Email;
            ZIP = contactus.ZIP;
            Street = contactus.Street;
            City = contactus.City;
        }


        public static ContactUsViewModel GetViewModel(IContactUs contactus, ICountryRepository countryRepository)
        {
            var countryStateName = CountryStateName.Parse(contactus.Country);
            var country = countryRepository.GetCountry(countryStateName.CountryName);
            var state = countryRepository.GetState(countryStateName.StateName);

            var model = new ContactUsViewModel(contactus)
            {
                CountryCode = country.CountryTwoLetterCode,
                Country = ResHelper.LocalizeString(country.CountryDisplayName)
            };

            if (state != null)
            {
                model.StateCode = state.StateName;
                model.State = ResHelper.LocalizeString(state.StateDisplayName);
            }

            return model;
        }
    }
}