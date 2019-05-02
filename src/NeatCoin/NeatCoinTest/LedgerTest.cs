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
        
    }
}