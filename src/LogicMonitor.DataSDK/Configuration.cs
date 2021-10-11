using System;
using System.IO;
using System.Collections.Generic;
using LogicMonitor.DataSDK.Utils;
using LogicMonitor.DataSDK.Model;
using System.ComponentModel.DataAnnotations;

namespace LogicMonitor.DataSDK
{
   /// <summary>
   /// This Class is used to configure the device with account name and Lm access credinatial(by using autenticate model).
   /// </summary>
    public class Configuration
    {
        //private readonly ILogger<Configuration> _logger;
        ObjectNameValidator objectNameValidator;
        private bool asyncRequest;
        private object _company;
        private string _host;
        public Authenticate authentication;
        public int ConnectionPoolMaxsize;
        public Configuration(){ }
        public bool AppendToFile { get; set; }
        public Configuration(string company, Authenticate authentication)
        {
            string id;
            string key;
            objectNameValidator = new ObjectNameValidator();
            ValidationContext validationContext = new ValidationContext(authentication);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(authentication, validationContext, validationResults);
            if (!valid)
            {
                foreach (ValidationResult validationResult in validationResults)
                {
                    throw new ArgumentException(validationResult.ErrorMessage);
                }
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
            
            this.asyncRequest = false;
            this.ConnectionPoolMaxsize = Environment.ProcessorCount * 5;
        }
        public bool AsyncRequest
        {
            get
            {
                return this.asyncRequest;
            }
            set
            {
                this.asyncRequest = value;
            }
        }
        public object company
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
                if (authentication == null || !(authentication is Authenticate) || authentication.Id == null || authentication.Key == null)
                {
                    throw new ArgumentException("Authenticate must provide the `id` and `key`");
                }
                if (!objectNameValidator.IsValidAuthId(authentication.Id))
                {
                    throw new ArgumentException("Invalid Access ID");
                }
            }

            if (authentication.Type == "Bearer")
            {
                if (authentication == null || !(authentication is Authenticate) ||  authentication.Key == null)
                {
                    throw new ArgumentException("Authenticate must provide Bearer Token");
                }
            }
            return true;
        }
        public object host
        {
            get
            {
                return this._host;
            }
        }

        public PlatformID Platform { get; }
        static void to_debug_report(PlatformID pID, Version ver)
        {
            OperatingSystem opSys = new OperatingSystem(pID, ver);
            PlatformID platform = opSys.Platform;
            Version version = opSys.Version;
            Console.WriteLine("   Platform: {0,-15} Version: {1}",
                platform, version);
        }
    }
}