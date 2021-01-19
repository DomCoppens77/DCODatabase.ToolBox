using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DCODatabase.ToolBox.Security.Hash
{
    public class HashService
    {
        private readonly HashAlgorithm _algo;

        public HashService(HashAlgorithm algo)
        {
            _algo = algo;
        }

        public byte[] Encode(String toHash)
        {
            return _algo.ComputeHash(Encoding.UTF8.GetBytes(toHash));
        }
    }
}
