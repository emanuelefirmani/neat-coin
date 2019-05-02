namespace NeatCoin
{
    public class Ledger
    {
        private readonly Transaction _transaction;

        private Ledger(Transaction transaction)
        {
            _transaction = transaction;
        }

        public static Ledger Empty => new Ledger(null);

        public Ledger Append(Transaction transaction) =>
            new Ledger(transaction);

        public Transaction GetTransaction() =>
            _transaction;

        public int Balance(string account) => 
            _transaction.Balance(account);
    }
}