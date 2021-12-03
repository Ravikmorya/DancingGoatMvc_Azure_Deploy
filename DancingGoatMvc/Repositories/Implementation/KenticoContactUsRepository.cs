using System.Linq;

using CMS.DocumentEngine.Types.DancingGoatMvc;

using Kentico.Content.Web.Mvc;

namespace DancingGoat.Repositories.Implementation
{
    public class KenticoContactUsRepository : IContactUsRepository
    {
        private readonly IPageRetriever pageRetriever;


        /// <summary>
        /// Initializes a new instance of the <see cref="KenticoContactRepository"/> class that returns contact information.
        /// </summary>
        /// <param name="pageRetriever">Retriever for pages based on given parameters.</param>
        public KenticoContactUsRepository(IPageRetriever pageRetriever)
        {
            this.pageRetriever = pageRetriever;
        }


        /// <summary>
        /// Returns company's contact information.
        /// </summary>
        public ContactUs GetCompanyContact()
        {
            return pageRetriever.Retrieve<ContactUs>(
                query => query
                    .TopN(1),
                cache => cache
                    .Key($"{nameof(KenticoContactRepository)}|{nameof(GetCompanyContact)}"))
                .FirstOrDefault();
        }
    }
}