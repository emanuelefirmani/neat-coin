using FluentAssertions;
using Xunit;
using NeatCoin;

namespace NeatCoinTest
{
    public class LedgerTest
    {
        [Fact]
        public void Can_compute_sender_balance()
        {
            var sut = Ledger
                .Empty
                .Append(new Transaction("from", "to", 100))
                .Append(new Transaction("to", "from", 20));

            sut.Balance("from").Should().Be(-80);
        }
        
        [Fact]
        public void Can_compute_receiver_balance()
        {
            var sut = Ledger
                .Empty
                .Append(new Transaction("from", "to", 100))
                .Append(new Transaction("to", "from", 20));

            var actual = sut.Balance("to");

            actual.Should().Be(80);
        }

        [Fact]
        public void Balance_of_unknown_account_should_be_zero()
        {
            var sut = Ledger
                .Empty
                .Append(new Transaction("from", "to", 100))
                .Append(new Transaction("to", "from", 20));

            var actual = sut.Balance("unknown");

            actual.Should().Be(0);
        }
    }
}