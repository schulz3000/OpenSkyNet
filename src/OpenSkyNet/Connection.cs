﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSkyNet
{
    /// <summary>
    /// Base class for HTTP Connection to opensky network
    /// </summary>
    public abstract class Connection : IDisposable
    {
        const string baseurl = "opensky-network.org/api/";

        readonly HttpClient client;
        readonly JsonSerializer serializer;

        /// <summary>
        /// Detect if Credentials are given
        /// </summary>
        protected bool HasCredentials => client.DefaultRequestHeaders.Authorization != null;

        /// <summary>
        /// 
        /// </summary>
        protected Connection()
        {
            client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }, true);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("https://" + baseurl);

            serializer = new JsonSerializer();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <exception cref="ArgumentNullException"></exception>
        protected Connection(string username, string password)
            : this()
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="uriPath"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="OpenSkyNetException"></exception>
        protected async Task<TResult> GetAsync<TResult>(string uriPath, CancellationToken token = default)
        {
            var response = await client.GetAsync(uriPath, HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                using (var sr = new StreamReader(stream))
                using (var jtr = new JsonTextReader(sr))
                {
                    return serializer.Deserialize<TResult>(jtr);
                }
            }

            throw new OpenSkyNetException($"{ response.StatusCode } - {response.ReasonPhrase}");
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnDispose()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                OnDispose();

                client?.Dispose();
            }
        }
    }
}
