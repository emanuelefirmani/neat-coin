using FluentAssertions;
using Xunit;
using NeatCoin;

namespace NeatCoinTest
{
    public class LegerTest
    {
        private static Ledger LedgerWithTransactions()
        {
            var sut = Ledger.Empty
                .Append(
                    Page.Genesis("genesis")
                        .Append(new Transaction("from", "to", 100))
                        .Append(new Transaction("to", "from", 20)))
                .Append(
                    Page.GetEmpty("1", "genesis")
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

        [Fact]
        public void Ledger_accepts_root_page()
        {
            Ledger.Empty
                .Append(
                    Page.Genesis("genesis")
                        .Append(new Transaction("from", "to", 100)))
                .Balance("to")
                .Should().Be(100);
        }

        [Fact]
        public void Ledger_can_accept_only_one_root_page()
        {
            Ledger.Empty
                .Append(
                    Page.Genesis("genesis")
                        .Append(new Transaction("from", "to", 100)))
                .Append(
                    Page.Genesis("1")
                        .Append(new Transaction("from", "to", 10)))
                
                .Balance("to")
                .Should().Be(0);
        }
        
        [Fact]
        public void Ledger_accepts_a_tree_of_pages()
        {
            Ledger.Empty
                .Append(
                    Page.Genesis("genesis")
                        .Append(new Transaction("from", "to", 100)))
                .Append(
                    Page.GetEmpty("1", "genesis")
                        .Append(new Transaction("from", "to", 10)))
                .Append(
                    Page.GetEmpty("2", "1")
                        .Append(new Transaction("from", "to", 1000)))                
                .Append(
                    Page.GetEmpty("3", "1")
                        .Append(new Transaction("from", "to", 10000)))
                .Balance("to")
                
                .Should().Be(11110);
        }

        [Fact]
        public void Ledger_accepts_a_tree_of_unsorted_pages()
        {
            Ledger.Empty
                .Append(
                    Page.GetEmpty("3", "1")
                        .Append(new Transaction("from", "to", 10000)))
                .Append(
                    Page.GetEmpty("1", "genesis")
                        .Append(new Transaction("from", "to", 10)))
                .Append(
                    Page.GetEmpty("1", "wrong")
                        .Append(new Transaction("from", "to", 10)))
                .Append(
                    Page.Genesis("genesis")
                        .Append(new Transaction("from", "to", 100)))
                .Append(
                    Page.GetEmpty("2", "1")
                        .Append(new Transaction("from", "to", 1000)))                
                .Balance("to")
                
                .Should().Be(11110);
        }

        [Fact]
        public void Ledger_discard_unlinked_pages()
        {
            Ledger.Empty
                .Append(
                    Page.Genesis("genesis")
                        .Append(new Transaction("from", "to", 100)))
                .Append(
                    Page.GetEmpty("1", "unknown")
                        .Append(new Transaction("from", "to", 10)))
                .Balance("to")
                
                .Should().Be(100);
        }
    }
}