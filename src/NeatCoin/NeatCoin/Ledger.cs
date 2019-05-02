using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NeatCoin
{
    public class Ledger
    {
        private readonly PageTree _pages;

        private Ledger(PageTree pages) => _pages = pages;

        public static Ledger Empty => new Ledger(PageTree.Empty);

        public Ledger Append(Page page) =>
            new Ledger(_pages.Add(page));

        public int Balance(string account) => 
            _pages.Sum(x => x.Balance(account));
    }

}