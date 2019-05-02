using FluentAssertions;
using NeatCoin;
using Xunit;

namespace NeatCoinTest
{
    public class PageTest
    {
        [Fact]
        public void pages_with_same_details_have_same_hash()
        {
            var p1 = Page.GetEmpty("my id", "my parent").Append(new Transaction("to", "from", 10));
            var p2 = Page.GetEmpty("my id", "my parent").Append(new Transaction("to", "from", 10));

            p1.Hash.Should().Be(p2.Hash);
        }

        [Fact]
        public void pages_with_different_transactions_have_different_hashes()
        {
            var p1 = Page.GetEmpty("my id", "my parent").Append(new Transaction("to", "from", 10));
            var p2 = Page.GetEmpty("my id", "my parent").Append(new Transaction("to", "from", 11));

            p1.Hash.Should().NotBe(p2.Hash);
        }

        [Fact]
        public void pages_with_different_details_have_different_hashes()
        {
            var p1 = Page.GetEmpty("my id", "my other parent").Append(new Transaction("to", "from", 10));
            var p2 = Page.GetEmpty("my id", "my parent").Append(new Transaction("to", "from", 10));

            p1.Hash.Should().NotBe(p2.Hash);
        }
    }
}