//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.6.2.0 (NJsonSchema v10.1.23.0 (Newtonsoft.Json v11.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

using Payment_SimpleCloud_MicroservicesHttp;

#pragma warning disable 108 // Disable "CS0108 '{derivedDto}.ToJson()' hides inherited member '{dtoBase}.ToJson()'. Use the new keyword if hiding was intended."
#pragma warning disable 114 // Disable "CS0114 '{derivedDto}.RaisePropertyChanged(String)' hides inherited member 'dtoBase.RaisePropertyChanged(String)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword."
#pragma warning disable 472 // Disable "CS0472 The result of the expression is always 'false' since a value of type 'Int32' is never equal to 'null' of type 'Int32?'
#pragma warning disable 1573 // Disable "CS1573 Parameter '...' has no matching param tag in the XML comment for ...
#pragma warning disable 1591 // Disable "CS1591 Missing XML comment for publicly visible type or member ..."

namespace Payment_SimpleCloud_MicroservicesHttp
{
    using System = global::System;
    
    

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.23.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class CommandHandlerResponseOfGuid : CommandHandlerResponse
    {
        [Newtonsoft.Json.JsonProperty("response", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Guid Response { get; set; }
    
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.23.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class CommandHandlerResponse : HandlerResponse
    {
        [Newtonsoft.Json.JsonProperty("databaseExecutionTime", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public long DatabaseExecutionTime { get; set; }
    
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.23.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class HandlerResponse 
    {
        [Newtonsoft.Json.JsonProperty("handlerExecutionTime", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public long HandlerExecutionTime { get; set; }
    
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.23.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class CreateClientCommand 
    {
        [Newtonsoft.Json.JsonProperty("email", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Email { get; set; }
    
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.23.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class AddClientTaskCommand 
    {
        [Newtonsoft.Json.JsonProperty("clientEmail", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ClientEmail { get; set; }
    
        [Newtonsoft.Json.JsonProperty("taskName", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string TaskName { get; set; }
    
        [Newtonsoft.Json.JsonProperty("startTIme", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset StartTIme { get; set; }
    
        [Newtonsoft.Json.JsonProperty("endTime", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset EndTime { get; set; }
    
    
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.6.2.0 (NJsonSchema v10.1.23.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class FileResponse : System.IDisposable
    {
        private System.IDisposable _client; 
        private System.IDisposable _response; 

        public int StatusCode { get; private set; }

        public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

        public System.IO.Stream Stream { get; private set; }

        public bool IsPartial
        {
            get { return StatusCode == 206; }
        }

        public FileResponse(int statusCode, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.IO.Stream stream, System.IDisposable client, System.IDisposable response)
        {
            StatusCode = statusCode; 
            Headers = headers; 
            Stream = stream; 
            _client = client; 
            _response = response;
        }

        public void Dispose() 
        {
            if (Stream != null)
                Stream.Dispose();
            if (_response != null)
                _response.Dispose();
            if (_client != null)
                _client.Dispose();
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.6.2.0 (NJsonSchema v10.1.23.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class ApiException : System.Exception
    {
        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

        public ApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception innerException)
            : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + ((response == null) ? "(null)" : response.Substring(0, response.Length >= 512 ? 512 : response.Length)), innerException)
        {
            StatusCode = statusCode;
            Response = response; 
            Headers = headers;
        }

        public override string ToString()
        {
            return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.6.2.0 (NJsonSchema v10.1.23.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class ApiException<TResult> : ApiException
    {
        public TResult Result { get; private set; }

        public ApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result, System.Exception innerException)
            : base(message, statusCode, response, headers, innerException)
        {
            Result = result;
        }
    }

}

#pragma warning restore 1591
#pragma warning restore 1573
#pragma warning restore  472
#pragma warning restore  114
#pragma warning restore  108