﻿namespace Virgil.SDK.Keys.Http
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// </summary>
    public abstract class ConnectionBase
    {
        /// <summary>
        ///     The access token header name
        /// </summary>
        protected const string AccessTokenHeaderName = "X-VIRGIL-ACCESS-TOKEN";

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConnectionBase" /> class.
        /// </summary>
        /// <param name="accessToken">The application token.</param>
        /// <param name="baseAddress">The base address.</param>
        protected ConnectionBase(string accessToken, Uri baseAddress)
        {
            this.AccessToken = accessToken;
            this.BaseAddress = baseAddress;
        }

        /// <summary>
        ///     Access Token
        /// </summary>
        public string AccessToken { get; protected set; }

        /// <summary>
        ///     Base address for the connection.
        /// </summary>
        public Uri BaseAddress { get; protected set; }

        /// <summary>
        ///     Sends an HTTP request to the API.
        /// </summary>
        /// <param name="request">The HTTP request details.</param>
        /// <returns></returns>
        public virtual async Task<IResponse> Send(IRequest request)
        {
            using (var httpClient = new HttpClient())
            {
                var nativeRequest = this.GetNativeRequest(request);
                var nativeResponse = await httpClient.SendAsync(nativeRequest);

                if (!nativeResponse.IsSuccessStatusCode)
                {
                    this.ExceptionHandler(nativeResponse);
                }

                var content = nativeResponse.Content.ReadAsStringAsync().Result;

                return new Response
                {
                    Body = content,
                    Headers = nativeResponse.Headers.ToDictionary(it => it.Key, it => it.Value.FirstOrDefault()),
                    StatusCode = (int) nativeResponse.StatusCode
                };
            }
        }

        /// <summary>
        ///     Produces native HTTP request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>HttpRequestMessage</returns>
        protected virtual HttpRequestMessage GetNativeRequest(IRequest request)
        {
            var message = new HttpRequestMessage(request.Method.GetMethod(), new Uri(this.BaseAddress, request.Endpoint));

            if (request.Headers != null)
            {
                message.Headers.TryAddWithoutValidation(AccessTokenHeaderName, this.AccessToken);

                foreach (var header in request.Headers)
                {
                    message.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            if (request.Method != RequestMethod.Get)
            {
                message.Content = new StringContent(request.Body, Encoding.UTF8, "application/json");
            }

            return message;
        }

        /// <summary>
        ///     Handles exception resposnses
        /// </summary>
        /// <param name="message">The http response message.</param>
        protected abstract void ExceptionHandler(HttpResponseMessage message);
    }
}