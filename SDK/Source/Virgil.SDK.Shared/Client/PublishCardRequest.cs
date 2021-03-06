#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2016 Virgil Security Inc.
// 
// Lead Maintainer: Virgil Security Inc. <support@virgilsecurity.com>
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions 
// are met:
// 
//   (1) Redistributions of source code must retain the above copyright
//   notice, this list of conditions and the following disclaimer.
//   
//   (2) Redistributions in binary form must reproduce the above copyright
//   notice, this list of conditions and the following disclaimer in
//   the documentation and/or other materials provided with the
//   distribution.
//   
//   (3) Neither the name of the copyright holder nor the names of its
//   contributors may be used to endorse or promote products derived 
//   from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE AUTHOR ''AS IS'' AND ANY EXPRESS OR
// IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT,
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING
// IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.
#endregion

namespace Virgil.SDK.Client
{
	using System.Collections.Generic;

    /// <summary>
    /// Represents a signable request that uses to publish new <see cref="CardModel"/> to the Virgil Services.
    /// </summary>
    public class PublishCardRequest : SignableRequest<PublishCardSnapshotModel>
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="PublishCardRequest"/> class by specified 
		/// snapshot and signatures.
		/// </summary>
		/// <param name="snapshot">The snapshot of the card request.</param>
		/// <param name="signatures">The signatures.</param>
		internal PublishCardRequest(byte[] snapshot, IDictionary<string, byte[]> signatures) 
		{
			this.takenSnapshot = snapshot;
			this.acceptedSignatures = new Dictionary<string, byte[]>(signatures);
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishCardRequest"/> class.
        /// </summary>
        /// <param name="stringifiedRequest">The stringified request.</param>
        public PublishCardRequest(string stringifiedRequest) : base(stringifiedRequest)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishCardRequest" /> class.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="identityType">Type of the identity.</param>
        /// <param name="publicKeyData">The public key data.</param>
        /// <param name="info">The information.</param>
        /// <param name="customFields">The custom fields.</param>
        public PublishCardRequest(
            string identity, 
            string identityType, 
            byte[] publicKeyData,
            CardInfoModel info = null,
            Dictionary<string, string> customFields = null) 
            : base(new PublishCardSnapshotModel
            {
                Identity = identity,
                IdentityType = identityType,
                PublicKeyData = publicKeyData,
                Data = customFields,
                Info = info,
                Scope = CardScope.Application
            })
        {
        }
    }
}