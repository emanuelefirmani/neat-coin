﻿using System;
using System.Xml.XPath;
using FluentAssertions;
using NeatCoin;
using NeatCoin.Cryptography;
using Xunit;
using SHA256 = NeatCoin.Cryptography.SHA256;

namespace NeatCoinTest
{
    public class BlockTest
    {
        private readonly SHA256 _sha256;

        public BlockTest()
        {
            _sha256 = new SHA256();
        }

        [Fact]
        public void blocks_created_in_different_moments_should_have_different_hash_values()
        {
            var now = DateTimeOffset.UtcNow;
            var block1 = new Block(_sha256, now, "same content", "0");
            var block2 = new Block(_sha256, now.AddMilliseconds(1), "same content", block1.Hash);

            var hash1 = block1.Hash;
            var hash2 = block2.Hash;

            hash1.Should().NotBe(hash2);
        }

        [Fact]
        public void blocks_with_different_contents_should_have_different_hash_values()
        {
            var sameMoment = DateTimeOffset.UtcNow;
            var block1 = new Block(_sha256, sameMoment, "content1", "0");
            var block2 = new Block(_sha256, sameMoment, "content2", block1.Hash);

            var hash1 = block1.Hash;
            var hash2 = block2.Hash;

            hash1.Should().NotBe(hash2);
        }

        [Fact]
        public void blocks_with_different_parents_should_have_different_hash_values()
        {
            var sameMoment = DateTimeOffset.UtcNow;
            var block1 = new Block(_sha256, sameMoment, "content1", "0");
            var block2 = new Block(_sha256, sameMoment, "content1", "1");

            var hash1 = block1.Hash;
            var hash2 = block2.Hash;

            hash1.Should().NotBe(hash2);
        }

        [Fact]
        public void should_be_valid_if_hash_matches()
        {
            var sut = new Block(_sha256, DateTimeOffset.UtcNow, "some content", "some parent");

            var result = sut.IsValid(_sha256.HashOf(sut.Serialized));

            result.Should().Be(true);
        }

        [Fact]
        public void should_not_be_valid_if_hash_does_not_match()
        {
            var sut = new Block(_sha256, DateTimeOffset.UtcNow, "some content", "some parent");

            var result = sut.IsValid("another hash");

            result.Should().Be(false);
        }
    }
}