/*
 * Copyright, 2021, LogicMonitor, Inc.
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
        public Authenticate authentication;
        public int ConnectionPoolMaxsize;
        public Configuration() { }

        public Configuration(string company, Authenticate authentication)
        {
            objectNameValidator = new ObjectNameValidator();
            StringBuilder sb = new StringBuilder();
            ValidationContext validationContext = new ValidationContext(authentication);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(authentication, validationContext, validationResults);
            if (!valid)
            {
                foreach (ValidationResult validationResult in validationResults)
                {
                    sb.Append(validationResult.ErrorMessage);
                }
                throw new ArgumentException(sb.ToString());

            }

            company = company ?? Convert.ToString(Environment.GetEnvironmentVariable("LM_COMPANY"));
            if (company is null || company == "")
            {
                throw new ArgumentException("Company must have your account name");
            }
            if (!objectNameValidator.IsValidCompanyName(company))
            {
                throw new ArgumentException("Invalid Company Name");
            }

            this.check_authentication(authentication);

            this._company = company;
            this.authentication = authentication;
            this._host = "https://" + this._company + ".logicmonitor.com/rest";
            this.ConnectionPoolMaxsize = Environment.ProcessorCount * 5;
        }
        public bool AsyncRequest {get;set; }
        public string company
        {
            get => this._company;
            set
            {
                this._company = value;
                this._host = "https://" + this._company + ".logicmonitor.com/rest";
            }
        }

        public bool  check_authentication(Authenticate authentication)
        {
            if(authentication.Type == "LMv1")
            {
                if ( authentication.Id.Equals(null) || authentication.Key.Equals(null))
                {
                    throw new ArgumentException("Authenticate must provide the `id` and `key`");
                }
                if (!objectNameValidator.IsValidAuthId(authentication.Id))
                {
                    throw new ArgumentException("Invalid Access ID");
                }
            }

            if (authentication.Type == "Bearer" && authentication.Key == null)
            {
               
                    throw new ArgumentException("Authenticate must provide Bearer Token");
            }
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