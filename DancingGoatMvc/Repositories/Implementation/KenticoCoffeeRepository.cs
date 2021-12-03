﻿using System.Collections.Generic;

using CMS.DocumentEngine.Types.DancingGoatMvc;

using DancingGoat.Repositories.Filters;

using Kentico.Content.Web.Mvc;

namespace DancingGoat.Repositories.Implementation
{
    /// <summary>
    /// Represents a collection of coffees.
    /// </summary>
    public class KenticoCoffeeRepository : ICoffeeRepository
    {
        private readonly IPageRetriever pageRetriever;


        /// <summary>
        /// Initializes a new instance of the <see cref="KenticoCoffeeRepository"/> class that returns coffees.
        /// </summary>
        /// <param name="pageRetriever">Retriever for pages based on given parameters.</param>
        public KenticoCoffeeRepository(IPageRetriever pageRetriever)
        {
            this.pageRetriever = pageRetriever;
        }


        /// <summary>
        /// Returns an enumerable collection of coffees ordered by the date of publication.
        /// </summary>
        /// <param name="filter">Instance of a product filter.</param>
        /// <param name="count">The number of coffees to return. Use 0 as value to return all records.</param>
        public IEnumerable<Coffee> GetCoffees(IRepositoryFilter filter, int count = 0)
        {
            return pageRetriever.Retrieve<Coffee>(
                query => query
                    .TopN(count)
                        .WhereTrue("SKUEnabled")
                        .Where(filter?.GetWhereCondition())
                        .FilterDuplicates()
                        .OrderByDescending("SKUInStoreFrom"),
                cache => cache
                    .Key($"{nameof(KenticoCoffeeRepository)}|{nameof(GetCoffees)}|{filter?.GetCacheKey()}|{count}")
                    // Include dependency on all pages of this type to flush cache when a new page is created.
                    .Dependencies((_, builder) => builder.Pages(Coffee.CLASS_NAME).ObjectType("ecommerce.sku")));
        }
    }
}