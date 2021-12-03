﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using CMS.DocumentEngine.Types.DancingGoatMvc;


namespace DancingGoat.Repositories
{
    public interface ISliderBannerItemRepository : IRepository
    {
        /// <summary>
        /// Asynchronously returns an enumerable collection of references ordered by the node order.
        /// </summary>
        /// <param name="nodeAliasPath">The node alias path of the parent page in the content tree.</param>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <param name="count">The number of references to return. Use 0 as value to return all records.</param>
        Task<IEnumerable<SliderBannerItem>> GetSliderBannerItemsAsync(string nodeAliasPath, CancellationToken cancellationToken, int count = 0);
    }
}