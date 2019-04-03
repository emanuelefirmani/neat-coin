using System;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace NeatCoin
{
    public class Page
    {
        public ImmutableList<Transaction> Transactions { get; }
        public string Parent { get; }
        public int Nonce { get; }

        public bool IsRoot => Parent == null;

        private Page(ImmutableList<Transaction> transactions, int nonce = 0)
        {
            Transactions = transactions;
            Nonce = nonce;
        }

        public Page(params Transaction[] transactions) : this(ImmutableList.Create(transactions)) {}

        public Page(ImmutableList<Transaction> transactions, string parent)
        {
            Transactions = transactions;
            Parent = parent;
        }

        public Page LinkTo(Page parent) => new Page(Transactions, parent.Hash);

        public string Hash => CalculateHash(
            Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(
                    new
                    {
                        Transactions,
                        Parent,
                        Nonce
                    })));

        public bool IsValid(int difficulty) => Hash.StartsWith(new string('0', difficulty));

        public int Balance(string account) =>
            Transactions.Where(t => t.Receiver == account).Sum(t => t.Amount) -
            Transactions.Where(t => t.Sender == account).Sum(t => t.Amount);

        public string CalculateHash(byte [] bytes)
        {
            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public Page CalculateNonce(int difficulty)
        {
            for (var i = 0; i < int.MaxValue; i++)
            {
                var page = CloneWithNonce(i);
                if (page.IsValid(difficulty))
                    return page;
            }

            return this;
        }

        private Page CloneWithNonce(int nonce)
        {
            return new Page(Transactions, nonce);
        }
    }
}