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
    }
}