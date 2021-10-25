/*
 * Copyright, 2021, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.ComponentModel.DataAnnotations;

namespace LogicMonitor.DataSDK.Model
{
    /// <summary>
    /// This model is used to Accept Lm access credentials.
    /// </summary>
    
    public class Authenticate
    {
        private string type;
        /// <summary>
        /// Authentication type("LMv1"/"Bearer").
        /// </summary>
        [Required(ErrorMessage = "Type must be intialised")]
        public string Type { get { return type; }
            set
            {
                if (value == "LMv1" || value == "Bearer")
                {
                    this.type = value;
                }
                else
                    throw new ArgumentException("Type must be 'LMv1' or 'Bearer' ");
            }
        }

        /// <summary>
        /// LM access ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// LM access key if ID is specified, otherwise accepts Bearer token
        /// </summary>
        
        public string Key { get; set; }

        
        public Authenticate()
        {
        }


    }
}
