using System.Collections.Generic;

using CMS.DataEngine;

using Kentico.Forms.Web.Mvc;

namespace DancingGoat.Models.FormComponents.ContactUsGroupSelector
{
    public class ContactUsGroupSelectorProperties : FormComponentProperties<List<string>>
    {
        public ContactUsGroupSelectorProperties() : base(FieldDataType.Unknown)
        {
        }


        public override List<string> DefaultValue
        {
            get;
            set;
        }
    }
}