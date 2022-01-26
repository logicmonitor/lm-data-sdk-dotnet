/*
 * Copyright, 2022, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */

using RestSharp;
namespace LogicMonitor.DataSDK.Api
{
    /// <summary>
    /// This interface is used to define the custom message for Successful and Failed request.
    /// </summary>
    public interface IResponseInterface
    {
        public void SuccessCallback(RestResponse response);
        public void ErrorCallback(RestResponse response);
    }
}

