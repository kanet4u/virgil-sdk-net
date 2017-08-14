﻿namespace Virgil.SDK.Tests
{
    using System;
    
    public class TestUtils
    {
        public static byte[] RandomBytes(int min = 100, int max = 1000)
        {
            var random = new Random();

            var randomCount = random.Next(min, max);
            var randomBytes = new byte[randomCount];

            random.NextBytes(randomBytes);

            return randomBytes;
        }   
    }
}