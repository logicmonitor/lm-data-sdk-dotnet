/*
 * Copyright, 2022, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */
using System;
using System.Collections.Generic;
using LogicMonitor.DataSDK.Utils;
using LogicMonitor.DataSDK.Model;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LogicMonitor.DataSDK
{
    /// <summary>
    /// This Class is used to configure the device with account name and Lm access credinatial(by using autenticate model).
    /// </summary>
    public class Configuration
    {
        private readonly ObjectNameValidator objectNameValidator;
        private string _company;
        private string _host;
        public int ConnectionPoolMaxsize;
        public bool AsyncRequest { get; set; }
        /// <summary>
        /// Use GZip to set if body is to be compressed while sending the data.Gzip compression algorithm is used for compression.
        /// </summary>
        public bool GZip { get; set; }
        /// <summary>
        /// Use AcceessID to set the LMv1's access id while using LMv1 authentication.
        /// </summary>
        public string AccessID { get; set; }
        /// <summary>
        /// Use AcceessKey to set the LMv1's access key while using LMv1 authentication.
        /// </summary>
        public string AccessKey { get; set; }
        /// <summary>
        /// Use BearerToken property to set the BearerToken while using Bearer authentication.
        /// </summary>
        public string BearerToken { get; set; }
        /// <summary>
        /// Use this property to set the Account name(Company Name).
        /// </summary>
        public string Company
        {
            get => this._company;
            set
            {
                this._company = value;
                this._host = "https://" + this._company + ".logicmonitor.com/rest";
            }
        }

        public Configuration()
        {
            objectNameValidator = new ObjectNameValidator();

            _company = Environment.GetEnvironmentVariable("LM_COMPANY");
            AccessID = Environment.GetEnvironmentVariable("LM_ACCESS_ID");
            AccessKey = Environment.GetEnvironmentVariable("LM_ACCESS_KEY");
            BearerToken = Environment.GetEnvironmentVariable("LM_BEARER_TOKEN");

            CheckAuthentication();

        }

        public Configuration(string company = null, string accessID = null, string accessKey = null, string bearerToken = null)
        {
            objectNameValidator = new ObjectNameValidator();

            _company = company ??= Environment.GetEnvironmentVariable("LM_COMPANY");
            AccessID = accessID ??= Environment.GetEnvironmentVariable("LM_ACCESS_ID");
            AccessKey = accessKey ??= Environment.GetEnvironmentVariable("LM_ACCESS_KEY");
            BearerToken = bearerToken ??= Environment.GetEnvironmentVariable("LM_BEARER_TOKEN");

            CheckAuthentication();
        }


        public bool CheckAuthentication()
        {
            bool flagBearerCheck = true;
            bool flagLMv1Check = true;


            if (AccessID != null && AccessKey != null)
                flagBearerCheck = false;
            if (BearerToken != null)
                flagLMv1Check = false;

            if ((AccessID == null || AccessKey == null) && flagLMv1Check == true)
            {
                throw new ArgumentException("Authenticate must provide the `id` and `key`");
            }
            if (flagLMv1Check == true)
            {
                if (!objectNameValidator.IsValidAuthId(AccessID))
                {
                    throw new ArgumentException("Invalid Access ID");
                }
                if (!objectNameValidator.IsValidAuthKey(AccessKey))
                {
                    throw new ArgumentException("Invalid Access Key");
                }
            }
            if (BearerToken == null && flagBearerCheck == true)
            {
                throw new ArgumentException("Authenticate must provide Bearer Token");
            }
            if (_company is null || _company == "")
            {
                throw new ArgumentException("Company must have your account name");
            }
            if (!objectNameValidator.IsValidCompanyName(_company))
            {
                throw new ArgumentException("Invalid Company Name");
            }

            this._host = "https://" + this._company + ".logicmonitor.com/rest";
            this.ConnectionPoolMaxsize = Environment.ProcessorCount * 5;
            return true;
        }

        public string host
        {
            get
            {
                return this._host;
            }
        }

        public PlatformID Platform { get; }
    }
}