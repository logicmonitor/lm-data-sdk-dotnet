# Org.OpenAPITools.Api.DefaultApi

All URIs are relative to *http://localhost/rest*

Method | HTTP request | Description
------------- | ------------- | -------------
[**RestApiV1OtracesPost**](DefaultApi.md#restapiv1otracespost) | **POST** /rest/api/v1/otraces | 
[**RestApiV1TracesPost**](DefaultApi.md#restapiv1tracespost) | **POST** /rest/api/v1/traces | 


<a name="restapiv1otracespost"></a>
# **RestApiV1OtracesPost**
> void RestApiV1OtracesPost (List<byte[]> requestBody = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class RestApiV1OtracesPostExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost/rest";
            var apiInstance = new DefaultApi(config);
            var requestBody = new List<byte[]>(); // List<byte[]> |  (optional) 

            try
            {
                apiInstance.RestApiV1OtracesPost(requestBody);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DefaultApi.RestApiV1OtracesPost: " + e.Message );
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
 **requestBody** | [**List&lt;byte[]&gt;**](byte[].md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/x-thrift
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="restapiv1tracespost"></a>
# **RestApiV1TracesPost**
> void RestApiV1TracesPost (List<byte[]> requestBody = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class RestApiV1TracesPostExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost/rest";
            var apiInstance = new DefaultApi(config);
            var requestBody = new List<byte[]>(); // List<byte[]> |  (optional) 

            try
            {
                apiInstance.RestApiV1TracesPost(requestBody);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DefaultApi.RestApiV1TracesPost: " + e.Message );
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
 **requestBody** | [**List&lt;byte[]&gt;**](byte[].md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/x-protobuf
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

