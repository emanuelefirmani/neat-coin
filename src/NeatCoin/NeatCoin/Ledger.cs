using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NeatCoin
{
    public class Ledger
    {
        private readonly ImmutableList<Page> _pages;

        private Ledger(ImmutableList<Page> pages) => _pages = pages;

        public static Ledger Empty => new Ledger(ImmutableList.Create<Page>());

        public Ledger Append(Page page) =>
            new Ledger(_pages.Add(page));

        public int Balance(string account) => 
            _pages.Sum(x => x.Balance(account));
    }
}