using System.Collections.Generic;
using System.Linq;

using CMS.ContactManagement;
using Kentico.Forms.Web.Mvc;

using DancingGoat.Models.FormComponents.ContactUsGroupSelector;

[assembly: RegisterFormComponent("DancingGoat.ContactUsGroupSelector", typeof(ContactUsGroupSelectorComponent), "ContactUs group selector", IsAvailableInFormBuilderEditor = false, ViewName = "FormComponents/_ContactUsGroupSelector")]

namespace DancingGoat.Models.FormComponents.ContactUsGroupSelector
{
    public class ContactUsGroupSelectorComponent : FormComponent<ContactUsGroupSelectorProperties, List<string>>
    {
        [BindableProperty]
        public List<ContactUsGroupSelectorListItem> Items
        {
            get;
            set;
        } = GetItems();


        public override List<string> GetValue()
        {
            var selectedCodeName = Items.Where(x => x.Checked)
                                        .Select(x => x.CodeName).ToList();

            return selectedCodeName;
        }


        public override void SetValue(List<string> value)
        {
            var items = GetItems();
            if (value != null)
            {
                items.ForEach(x => x.Checked = value.Contains(x.CodeName));
            }

            Items = items;
        }


        private static List<ContactUsGroupSelectorListItem> GetItems()
        {
            return ContactGroupInfo.Provider.Get().Select(x => new ContactUsGroupSelectorListItem
            {
                CodeName = x.ContactGroupName,
                DisplayName = x.ContactGroupDisplayName,
                Checked = false
            }).ToList();
        }
    }
}