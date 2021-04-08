using System;
using System.Text;

namespace ToyBlockChain.Core
{
    public class Block
    {
        private readonly Transaction _transaction;
        private readonly BlockHeader _blockHeader;

        public Block(BlockHeader blockHeader, Transaction transaction)
        {
            _blockHeader = blockHeader;
            _transaction = transaction;
        }

        public int Index
        {
            get
            {
                return BlockHeader.Index;
            }
        }

        public byte[] HashBytes
        {
            get
            {
                return BlockHeader.HashBytes;
            }
        }

        public string HashString
        {
            get
            {
                return BlockHeader.HashString;
            }
        }

        public string PreviousHashString
        {
            get
            {
                return BlockHeader.PreviousHashString;
            }
        }

        public BlockHeader BlockHeader
        {
            get
            {
                return _blockHeader;
            }
        }

        public Transaction Transaction
        {
            get
            {
                return _transaction;
            }
        }

        public bool IsValid()
        {
            return (
                _blockHeader.TransactionHashString == _transaction.HashString
                && BlockHeader.IsValid()
                && Transaction.IsValid());
        }

        public override string ToString()
        {
            return (
                $"BLOCK HEADER:\n{BlockHeader}".Replace("\n", "\n\t")
                + "\n"
                + $"TRANSACTION:\n{Transaction}".Replace("\n", "\n\t"));
        }

        public string ToSerializedString()
        {
            return String.Format(
                "{0},{1}",
                BlockHeader.ToSerializedString(),
                Transaction.ToSerializedString());
        }

        public byte[] ToSerializedBytes()
        {
            return Encoding.UTF8.GetBytes(ToSerializedString());
        }

    }
}
