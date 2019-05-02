using System.Collections.Immutable;
using System.Linq;

namespace NeatCoin
{
    public class Ledger
    {
        private readonly ImmutableList<Transaction> _transactions;

        private Ledger(ImmutableList<Transaction> transactions)
        {
            _transactions = transactions;
        }

        public static Ledger Empty => new Ledger(ImmutableList.Create<Transaction>());

        public Ledger Append(Transaction transaction) =>
            new Ledger(_transactions.Add(transaction));

        public int Balance(string account) => 
            _transactions.Sum(x => x.Balance(account));
    }
}