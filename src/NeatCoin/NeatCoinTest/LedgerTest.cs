using FluentAssertions;
using Xunit;
using NeatCoin;

namespace NeatCoinTest
{
    public class LedgerTest
    {
        [Fact]
        public void Ledger_contains_a_transaction()
        {
            var sut = Ledger
                .Empty
                .Append(new Transaction("from", "to", 42));

            var actual = sut.GetTransaction();

            actual
                .Should()
                .BeEquivalentTo(new Transaction("from", "to", 42));
        }

        [Fact]
        public void Can_compute_sender_balance()
        {
            var sut = Ledger
                .Empty
                .Append(new Transaction("from", "to", 42));

            var actual = sut.Balance("from");

            actual.Should().Be(-42);
        }
        
        [Fact]
        public void Can_compute_receiver_balance()
        {
            var sut = Ledger
                .Empty
                .Append(new Transaction("from", "to", 42));

            var actual = sut.Balance("to");

            actual.Should().Be(42);
        }

        [Fact]
        public void Balance_of_unknown_account_should_be_zero()
        {
            var sut = Ledger
                .Empty
                .Append(new Transaction("from", "to", 42));

            var actual = sut.Balance("unknown");

            actual.Should().Be(0);
        }

    }
}