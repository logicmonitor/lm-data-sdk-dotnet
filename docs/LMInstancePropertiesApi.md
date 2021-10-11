# Org.OpenAPITools.Api.LMInstancePropertiesApi

All URIs are relative to *http://localhost/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**RestInstancePropertyIngestPatch**](LMInstancePropertiesApi.md#restinstancepropertyingestpatch) | **PATCH** /rest/instance_property/ingest | UpdateInstancePropertiesAPI
[**RestInstancePropertyIngestPut**](LMInstancePropertiesApi.md#restinstancepropertyingestput) | **PUT** /rest/instance_property/ingest | UpdateInstancePropertiesAPI


<a name="restinstancepropertyingestpatch"></a>
# **RestInstancePropertyIngestPatch**
> PushMetricAPIResponse RestInstancePropertyIngestPatch (RestInstancePropertiesV1 restInstancePropertiesV1 = null)

UpdateInstancePropertiesAPI

UpdateInstancePropertiesAPI is for the purpose of updating instance properties for any resource to the LM application. It needs payload of object type RestInstancePropertiesV1. Payload is then validated with series of validation, successfully verified metrics will be ingested to Kafka. Only PUT and PATCH method will be applied to this API

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class RestInstancePropertyIngestPatchExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost/rest";
            // Configure API key authorization: LMv1
            config.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new LMInstancePropertiesApi(config);
            var restInstancePropertiesV1 = new RestInstancePropertiesV1(); // RestInstancePropertiesV1 |  (optional) 

            try
            {
                // UpdateInstancePropertiesAPI
                PushMetricAPIResponse result = apiInstance.RestInstancePropertyIngestPatch(restInstancePropertiesV1);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LMInstancePropertiesApi.RestInstancePropertyIngestPatch: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **restInstancePropertiesV1** | [**RestInstancePropertiesV1**](RestInstancePropertiesV1.md)|  | [optional] 

### Return type

[**PushMetricAPIResponse**](PushMetricAPIResponse.md)

### Authorization

[LMv1](../README.md#LMv1)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **202** | The request has been accepted for processing, but the processing has not been completed. |  -  |
| **400** | Received a bad request. |  -  |
| **401** | Authentication error. |  -  |
| **402** | Push Metrics feature is disabled for company. |  -  |
| **403** | Invalid user permission. |  -  |
| **404** | Company not found. |  -  |
| **405** | HTTP Method Not Allowed. |  -  |
| **415** | HTTP 415 Unsupported Media Type. |  -  |
| **500** | Something went wrong. Could not process your request right now. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="restinstancepropertyingestput"></a>
# **RestInstancePropertyIngestPut**
> PushMetricAPIResponse RestInstancePropertyIngestPut (RestInstancePropertiesV1 restInstancePropertiesV1 = null)

UpdateInstancePropertiesAPI

UpdateInstancePropertiesAPI is for the purpose of updating instance properties for any resource to the LM application. It needs payload of object type RestInstancePropertiesV1. Payload is then validated with series of validation, successfully verified metrics will be ingested to Kafka. Only PUT and PATCH method will be applied to this API

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class RestInstancePropertyIngestPutExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost/rest";
            // Configure API key authorization: LMv1
            config.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new LMInstancePropertiesApi(config);
            var restInstancePropertiesV1 = new RestInstancePropertiesV1(); // RestInstancePropertiesV1 |  (optional) 

            try
            {
                // UpdateInstancePropertiesAPI
                PushMetricAPIResponse result = apiInstance.RestInstancePropertyIngestPut(restInstancePropertiesV1);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LMInstancePropertiesApi.RestInstancePropertyIngestPut: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **restInstancePropertiesV1** | [**RestInstancePropertiesV1**](RestInstancePropertiesV1.md)|  | [optional] 

### Return type

[**PushMetricAPIResponse**](PushMetricAPIResponse.md)

### Authorization

[LMv1](../README.md#LMv1)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **202** | The request has been accepted for processing, but the processing has not been completed. |  -  |
| **400** | Received a bad request. |  -  |
| **401** | Authentication error. |  -  |
| **402** | Push Metrics feature is disabled for company. |  -  |
| **403** | Invalid user permission. |  -  |
| **404** | Company not found. |  -  |
| **405** | HTTP Method Not Allowed. |  -  |
| **415** | HTTP 415 Unsupported Media Type. |  -  |
| **500** | Something went wrong. Could not process your request right now. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

