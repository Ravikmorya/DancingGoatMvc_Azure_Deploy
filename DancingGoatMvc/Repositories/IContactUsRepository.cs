using CMS.DocumentEngine.Types.DancingGoatMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DancingGoat.Repositories
{
    public interface IContactUsRepository : IRepository
    {
        /// <summary>
        /// Returns company's contact information.
        /// </summary>
        /// <returns>Company's contact information, if found; otherwise, null.</returns>
        ContactUs GetCompanyContact();
    }
}