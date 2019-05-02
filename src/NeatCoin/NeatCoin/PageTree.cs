using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace NeatCoin
{
    public class PageTree : IEnumerable<Page>
    {
        private readonly ImmutableList<Page> _pages;

        private PageTree(ImmutableList<Page> pages)
        {
            _pages = pages;
        }

        public static PageTree Empty => 
            new PageTree(ImmutableList<Page>.Empty);

        public PageTree Add(Page page) => 
            new PageTree(_pages.Add(page));

        public IEnumerator<Page> GetEnumerator()
        {
            if (_pages.Count(p => p.IsGenesis()) != 1)
                yield break;

            var queue = new Queue<Page>();
            queue.Enqueue(_pages.Single(p => p.IsGenesis()));
            while (queue.Count > 0)
            {
                var result = queue.Dequeue();
                foreach (var page in _pages.Where(p => p.Parent == result.Hash))
                {
                    queue.Enqueue(page);
                }

                yield return result;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            GetEnumerator();
    }
}