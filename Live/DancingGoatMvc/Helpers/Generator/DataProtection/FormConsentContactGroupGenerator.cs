using CMS.ContactManagement;
using CMS.MacroEngine;
using CMS.Membership;

namespace DancingGoat.Helpers.Generator
{
    public class FormContactGroupGenerator
    {
        private const string CONTACT_GROUP_DISPLAY_NAME = "Coffee samples applicants";
        private const string CONTACT_GROUP_NAME = "CoffeeSamplesApplicants";
           
        //ADD CONTACTUS 
        private const string CONTACTUS_GROUP_DISPLAY_NAME = "Coffee samples applicants";
        private const string CONTACTUS_GROUP_NAME = "CoffeeSamplesApplicants";


        public void Generate()
        {
            CreateContactGroupWithFormConsentAgreementRule();
            //ADD CONTACT Us
            //CreateContactUSGroupWithFormConsentAgreementRule();

        }


        private void CreateContactGroupWithFormConsentAgreementRule()
        {
            if (ContactGroupInfo.Provider.Get(CONTACT_GROUP_NAME) != null)
            {
                return;
            }

            var contactGroup = new ContactGroupInfo
            {
                ContactGroupDisplayName = CONTACT_GROUP_DISPLAY_NAME,
                ContactGroupName = CONTACT_GROUP_NAME,
                ContactGroupDynamicCondition = GetFormConsentMacroRule(),
                ContactGroupEnabled = true
            };

            ContactGroupInfo.Provider.Set(contactGroup);
        }
        //ADD Contact Us
        //private void CreateContactUsGroupWithFormConsentAgreementRule()
        //{
        //    if (ContactGroupInfo.Provider.Get(CONTACTUS_GROUP_NAME) != null)
        //    {
        //        return;
        //    }

        //    var contactUsGroup = new ContactGroupInfo
        //    {
        //        ContactGroupDisplayName = CONTACTUS_GROUP_DISPLAY_NAME,
        //        ContactUsGroupName = CONTACTUS_GROUP_NAME,
        //        ContactUsGroupDynamicCondition = GetFormConsentMacroRule(),
        //        ContactUsGroupEnabled = true
        //    };

        //    ContactUsGroupInfo.Provider.Set(contactUsGroup);
        //}


        private string GetFormConsentMacroRule()
        {
            var rule = $@"{{%Rule(""(Contact.AgreedWithConsent(""{FormConsentGenerator.CONSENT_NAME}""))"", "" <rules><r pos =\""0\"" par=\""\"" op=\""and\"" n=\""CMSContactHasAgreedWithConsent\"" >
                        <p n=\""consent\""><t>{FormConsentGenerator.CONSENT_DISPLAY_NAME}</t><v>{FormConsentGenerator.CONSENT_NAME}</v><r>0</r><d>select consent</d><vt>text</vt><tv>0</tv></p>
                        <p n=\""_perfectum\""><t>has</t><v></v><r>0</r><d>select operation</d><vt>text</vt><tv>0</tv></p></r></rules>"")%}}";

            rule = $@"{{%Rule(""(ContactUs.AgreedWithConsent(""{FormConsentGenerator.CONSENT_NAME}""))"", "" <rules><r pos =\""0\"" par=\""\"" op=\""and\"" n=\""CMSContactUsHasAgreedWithConsent\"" >
                        <p n=\""consent\""><t>{FormConsentGenerator.CONSENT_DISPLAY_NAME}</t><v>{FormConsentGenerator.CONSENT_NAME}</v><r>0</r><d>select consent</d><vt>text</vt><tv>0</tv></p>
                        <p n=\""_perfectum\""><t>has</t><v></v><r>0</r><d>select operation</d><vt>text</vt><tv>0</tv></p></r></rules>"")%}}";

            return MacroSecurityProcessor.AddSecurityParameters(rule, MacroIdentityOption.FromUserInfo(UserInfoProvider.AdministratorUser), null);
        }
    }
}