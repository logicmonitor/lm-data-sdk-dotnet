# Org.OpenAPITools.Api.LMMetricIngestApi

All URIs are relative to *http://localhost/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**RestMetricIngestPost**](LMMetricIngestApi.md#restmetricingestpost) | **POST** /rest/metric/ingest | MetricIngestAPI


<a name="restmetricingestpost"></a>
# **RestMetricIngestPost**
> PushMetricAPIResponse RestMetricIngestPost (bool? create = null, RestMetricsV1 restMetricsV1 = null)

MetricIngestAPI

MetricIngestAPI is used for the purpose of ingesting raw metrics to the LM application. It needs metrics in the format of RestMetricsV1 object. Payload is then validated with series of validation, successfully verified metrics will be ingested to Kafka. Only POST method is applied to this API

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class RestMetricIngestPostExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost/rest";
            // Configure API key authorization: LMv1
            config.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new LMMetricIngestApi(config);
            var create = true;  // bool? | Do you want to create resource? true/false (optional)  (default to false)
            var restMetricsV1 = new RestMetricsV1(); // RestMetricsV1 |  (optional) 

            try
            {
                // MetricIngestAPI
                PushMetricAPIResponse result = apiInstance.RestMetricIngestPost(create, restMetricsV1);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LMMetricIngestApi.RestMetricIngestPost: " + e.Message );
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
 **create** | **bool?**| Do you want to create resource? true/false | [optional] [default to false]
 **restMetricsV1** | [**RestMetricsV1**](RestMetricsV1.md)|  | [optional] 

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

