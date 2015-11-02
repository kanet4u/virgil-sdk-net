﻿namespace Virgil.Examples.SDK
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using Virgil.Examples.Common;

    using Virgil.SDK.Keys;
    using Virgil.SDK.Keys.Exceptions;

    public class GetPublicKeySigned : IExample
    {
        public async Task Run()
        {
            try
            {
                var keysService = new KeysClient(Constants.AppToken); // use your application access token
                var publicKey = await keysService.PublicKeys.SearchExtended(Constants.PublicKeyId, Constants.PrivateKey);
                
                Console.Write(Encoding.UTF8.GetString(publicKey.Key));
            }
            catch (KeysException ex)
            {
                Console.Write("Error: {0}", ex.Message);
            }
        }
    }
}