using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.DancingGoatMvc;

using Kentico.Content.Web.Mvc;

namespace DancingGoat.Repositories.Implementation
{
    /// <summary>
    /// Represents a collection of stories about company's strategy, history and philosophy.
    /// </summary>
    public class KenticoAboutUsRepository : IAboutUsRepository
    {
        private readonly IPageRetriever pageRetriever;


        /// <summary>
        /// Initializes a new instance of the <see cref="KenticoAboutUsRepository"/> class that returns stories about company's strategy, history and philosophy.
        /// </summary>
        /// <param name="pageRetriever">Retriever for pages based on given parameters.</param>
        public KenticoAboutUsRepository(IPageRetriever pageRetriever)
        {
            this.pageRetriever = pageRetriever;
        }


        /// <summary>
        /// Asynchronously returns an enumerable collection of stories about company's philosophy ordered by a position in the content tree.
        /// </summary>
        /// <param name="nodeAliasPath">The node alias path of the About us section in the content tree.</param>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        public Task<IEnumerable<AboutUsSection>> GetSideStoriesAsync(string nodeAliasPath, CancellationToken cancellationToken)
        {
            return pageRetriever.RetrieveAsync<AboutUsSection>(
                query => query
                    .Path(nodeAliasPath, PathTypeEnum.Children)
                    .OrderBy("NodeOrder"),
                cache => cache
                    .Key($"{nameof(KenticoAboutUsRepository)}|{nameof(GetSideStoriesAsync)}|{nodeAliasPath}")
                    // Include path dependency to flush cache when a new child page is created or page order is changed.
                    .Dependencies((_, builder) => builder.PagePath(nodeAliasPath, PathTypeEnum.Children).PageOrder()),
                cancellationToken);
        }


        /// <summary>
        /// Returns an enumerable collection of stories about company's philosophy ordered by a position in the content tree.
        /// </summary>
        /// <param name="nodeAliasPath">The node alias path of the About us section in the content tree.</param>
        public IEnumerable<AboutUsSection> GetSideStories(string nodeAliasPath)
        {
            return pageRetriever.Retrieve<AboutUsSection>(
                query => query
                    .Path(nodeAliasPath, PathTypeEnum.Children)
                    .OrderBy("NodeOrder"),
                cache => cache
                    .Key($"{nameof(KenticoAboutUsRepository)}|{nameof(GetSideStories)}|{nodeAliasPath}")
                    // Include path dependency to flush cache when a new child page is created or page order is changed.
                    .Dependencies((_, builder) => builder.PagePath(nodeAliasPath, PathTypeEnum.Children).PageOrder()));
        }
    }
}