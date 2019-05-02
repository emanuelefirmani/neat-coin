using System.Collections.Immutable;
using System.Linq;
using NeatCoin;

namespace NeatCoin
{
    public class Page
    {
        private readonly ImmutableList<Transaction> _transactions;
        public string Id { get; set; }

        private Page(ImmutableList<Transaction> transactions, string id, string parent)
        {
            Id = id;
            Parent = parent;
            _transactions=transactions;
        }

        public string Parent { get; set; }

        public static Page GetEmpty(string id, string parent) => 
            new Page(ImmutableList<Transaction>.Empty, id, parent);

        public Page Append(Transaction transaction) => 
            new Page(_transactions.Add(transaction), Id, Parent);

        public int Balance(string account) => 
            _transactions.Sum(x => x.Balance(account));

        public static Page Genesis(string id) => 
            GetEmpty(id, null);

        public bool IsGenesis() => 
            string.IsNullOrEmpty(Parent);
    }
}