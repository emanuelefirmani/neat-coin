using System.Collections.Immutable;
using System.Linq;
using NeatCoin;

namespace NeatCoin
{
    public class Page
    {
        private readonly ImmutableList<Transaction> _transactions;

        private Page(ImmutableList<Transaction> transactions)
        {
            _transactions=transactions;
        }

        public static Page Empty => new Page(ImmutableList<Transaction>.Empty);

        public Page Append(Transaction transaction) => 
            new Page(_transactions.Add(transaction));

        public int Balance(string account) => 
            _transactions.Sum(x => x.Balance(account));
    }
}