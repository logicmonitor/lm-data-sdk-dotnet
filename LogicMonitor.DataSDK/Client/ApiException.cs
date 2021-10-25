/*
 * Copyright, 2021, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */

using System;
using RestSharp;

namespace LogicMonitor.DataSDK.Client
{
    /// <summary>
    /// API Exception
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Gets or sets the error code (HTTP status code)
        /// </summary>
        /// <value>The error code (HTTP status code).</value>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the error content (body json object)
        /// </summary>
        /// <value>The error content (Http response body).</value>
        public object ErrorContent { get; private set; }

        /// <summary>
        /// Gets or sets the HTTP headers
        /// </summary>
        /// <value>HTTP headers</value>
        public Multimap<string, string> Headers { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        public ApiException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="errorCode">HTTP status code.</param>
        /// <param name="message">Error message.</param>
        public ApiException(int errorCode, string message) : base(message)
        {
            this.ErrorCode = errorCode;
        }

        public ApiException(RestResponse response, string errorMessage = default) : base(errorMessage)
        {
            errorMessage += string.Format("{0}\n Reason: {1}",response.StatusCode,response.ErrorMessage);
            if (response.Headers != null)
                errorMessage += string.Format("\nHTTP response header: {0}", response.Headers.ToString());
            if(response.Content != null)
                errorMessage += string.Format("\nHTTP response body: {0}", response.Content.ToString());
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="errorCode">HTTP status code.</param>
        /// <param name="message">Error message.</param>
        /// <param name="errorContent">Error content.</param>
        /// <param name="headers">HTTP Headers.</param>
        public ApiException(int errorCode, string message, object errorContent = null, Multimap<string, string> headers = null) : base(message)
        {
            this.ErrorCode = errorCode;
            this.ErrorContent = errorContent;
            this.Headers = headers;
        }
    }

}
