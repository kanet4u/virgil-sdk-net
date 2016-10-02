namespace Virgil.SDK.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using FluentAssertions;

    using Virgil.SDK.Client;
    using Virgil.SDK.Cryptography;

    using NUnit.Framework;
    using Virgil.SDK.Exceptions;

    public class VirgilCardCreationTest
    {
        [Test]
        public async Task CreateNewVirgilCard_DuplicateCardCreation_ShouldThrowException()
        {
            var crypto = new VirgilCrypto();
            var client = IntergrationHelper.GetVirgilClient();
            
            var appKey = crypto.ImportPrivateKey(IntergrationHelper.AppKey, IntergrationHelper.AppKeyPassword);

            var aliceKeys = crypto.GenerateKeys();
            var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);

            var aliceIdentity = "alice-" + Guid.NewGuid();
            var request = new CreateCardRequest(aliceIdentity, "member", exportedPublicKey);

            var requestSigner = new RequestSigner(crypto);

            requestSigner.SelfSign(request, aliceKeys.PrivateKey);
            requestSigner.AuthoritySign(request, IntergrationHelper.AppID, appKey);
            
            var virgilCard = await client.CreateCardAsync(request);
            Assert.ThrowsAsync<VirgilClientException>(async () => await client.CreateCardAsync(request));
        }

        [Test]
        public async Task CreateNewVirgilCard_IdentityAndPublicKeyGiven_ShouldBeFoundByIdentity()
        {
            var crypto = new VirgilCrypto();
            var client = IntergrationHelper.GetVirgilClient();

            var appKey = crypto.ImportPrivateKey(IntergrationHelper.AppKey, IntergrationHelper.AppKeyPassword);

            var aliceKeys = crypto.GenerateKeys();
            var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);

            var aliceIdentity = "alice-" + Guid.NewGuid();

            var request = new CreateCardRequest(aliceIdentity, "member", exportedPublicKey);

            var requestSigner = new RequestSigner(crypto);

            requestSigner.SelfSign(request, aliceKeys.PrivateKey);
            requestSigner.AuthoritySign(request, IntergrationHelper.AppID, appKey);

            var newCard = await client.CreateCardAsync(request);
            var cards = await client.SearchCardsAsync(new SearchCardsCriteria { Identities = new[] { aliceIdentity } });
            
            cards.Should().HaveCount(1);
            var foundCard = cards.Single();

            newCard.ShouldBeEquivalentTo(foundCard);
        }
    }
}