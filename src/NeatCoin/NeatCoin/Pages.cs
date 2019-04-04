using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace NeatCoin
{
    internal class Pages : IEnumerable<Page>
    {
        private readonly ImmutableList<Page> _pages;

        internal Pages(ImmutableList<Page> pages)
        {
            _pages = pages;
        }

        public Pages Add(Page page) => new Pages(_pages.Add(page));

        public IEnumerator<Page> GetEnumerator()
        {
            if (!_pages.Any())
                yield break;

            var currentPage = _pages.SingleOrDefault(p => p.Parent == null);
            while (!currentPage.Equals(default(Page)))
            {
                yield return currentPage;

                var singleOrDefault = _pages.SingleOrDefault(p => p.Parent == currentPage.Hash);
                currentPage = singleOrDefault;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}