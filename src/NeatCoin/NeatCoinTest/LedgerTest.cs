using FluentAssertions;
using Xunit;
using NeatCoin;

namespace NeatCoinTest
{
    public class LedgerTest
    {
        private static Ledger LedgerWithTransactions()
        {
            var sut = Ledger.Empty
                .Append(
                    Page.Empty
                        .Append(new Transaction("from", "to", 100))
                        .Append(new Transaction("to", "from", 20)))
                .Append(
                    Page.Empty
                        .Append(new Transaction("from", "to", 10)));
            return sut;
        }

        [Fact]
        public void Can_compute_sender_balance()
        {
            var sut = LedgerWithTransactions();

            sut.Balance("from").Should().Be(-90);
        }

        [Fact]
        public void Can_compute_receiver_balance()
        {
            var sut = LedgerWithTransactions();

            var actual = sut.Balance("to");

            actual.Should().Be(90);
        }

        [Fact]
        public void Balance_of_unknown_account_should_be_zero()
        {
            var sut = LedgerWithTransactions();

            var actual = sut.Balance("unknown");

            actual.Should().Be(0);
        }
    }
}