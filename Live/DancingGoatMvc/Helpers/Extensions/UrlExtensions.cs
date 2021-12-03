using System;
using System.Collections.Generic;
using System.Linq;
using Kentico.Content.Web.Mvc;
using Kentico.Web.Mvc;
using System.Web;
using System.Web.Mvc;

namespace DancingGoat.Helpers.Extensions
{
    public static class UrlExtensions
    {
        [System.Obsolete]
        public static string KenticoImageUrl(this UrlHelper helper, string path)
        {
            return helper.Kentico().ImageUrl(path, SizeConstraint.Empty);

        }
    }
}