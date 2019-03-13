using System.Collections.Generic;
using System.Linq;
using NeatCoin.Cryptography;

namespace NeatCoin
{
    internal class BlockChain
    {
        private readonly List<Block> _blocks = new List<Block>();

        public BlockChain(ICryptography cryptography, int difficulty)
        {
            _blocks.Add(new GenesisBlock(cryptography, difficulty));
        }

        internal void Push(Block block)
        {
            if (AddingTheGenesis(block) || block.IsChainedTo(Last))
                _blocks.Add(block);
        }

        private bool AddingTheGenesis(Block block)
        {
            return block.Parent == "0" && Last == null;
        }

        public Block Last => _blocks.LastOrDefault();

        public Block GetBlockByHash(string blockHash) =>
            _blocks.SingleOrDefault(b => b.Hash == blockHash);
    }
}