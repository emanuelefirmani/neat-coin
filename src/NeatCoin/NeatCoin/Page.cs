using System.Collections.Immutable;
using System.Linq;
using NeatCoin;

namespace NeatCoin
{
    public class Page
    {
        private readonly ImmutableList<Transaction> _transactions;

        private Page(ImmutableList<Transaction> transactions, string parent)
        {
            Parent = parent;
            _transactions=transactions;
        }

        public string Parent { get; set; }
        public string Hash => (new Pie.NCrypt.SHA1()).HashOf(new {Transactions= _transactions, Parent});

        public static Page GetEmpty(string id, string parent) => 
            new Page(ImmutableList<Transaction>.Empty, parent);

        public Page Append(Transaction transaction) => 
            new Page(_transactions.Add(transaction), Parent);

        public int Balance(string account) => 
            _transactions.Sum(x => x.Balance(account));

        public static Page Genesis(string id) => 
            GetEmpty(id, null);

        public bool IsGenesis() => 
            string.IsNullOrEmpty(Parent);
    }
}