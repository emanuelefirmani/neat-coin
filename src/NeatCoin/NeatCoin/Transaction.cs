namespace NeatCoin
{
    public class Transaction
    {
        public string Sender { get; }
        public string Receiver { get; }
        public int Amount { get; }

        public Transaction(string sender, string receiver, int amount)
        {
            Sender = sender;
            Receiver = receiver;
            Amount = amount;
        }

        public int Balance(string account)
        {
            if (account == this.Sender)
                return -this.Amount;
            else if (account == this.Receiver)
                return this.Amount;
            else
                return 0;
        }
    }
}