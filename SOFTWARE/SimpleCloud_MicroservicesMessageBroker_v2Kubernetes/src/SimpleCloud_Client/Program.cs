using SimpleCloudMonolithic_Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCloud_Client
{
    class ExecutionLog
    {
        public string RequestName { get; set; }
        public long RequestTime { get; set; }
    }

    class RequestMonitor
    {
        public List<ExecutionLog> ExecutionLogs { get; set; } = new List<ExecutionLog>();

        public async Task<TResult> ExecuteAsync<TResult>(
            string requestName,
            Func<Task<TResult>> request)
            where TResult : class
        {

            Thread.Sleep(500);
            var _timer = new Stopwatch();

            _timer.Start();
            var response = await request();
            _timer.Stop();

            ExecutionLogs.Add(
                new ExecutionLog()
                {
                    RequestName = requestName,
                    RequestTime = _timer.ElapsedMilliseconds
                });

            return response as TResult;
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            // var identityApiBaseUrl = "http://localhost:63907";
            // var machineLearningApiBaseUrl = "http://localhost:64699";
            // var paymentApiBaseUrl = "http://localhost:44312";
            var identityApiBaseUrl = "http://localhost:1421";
            var machineLearningApiBaseUrl = "http://localhost:1422";
            var paymentApiBaseUrl = "http://localhost:1423";

            var trainFilesPath = @"D:\STUDIA\PRACA MAGISTERSKA\ExampleProblem\data\train_small";
            var predictionFilesPath = @"D:\STUDIA\PRACA MAGISTERSKA\ExampleProblem\data\test";

            var clientsNumber = 5;

            var _configurationsIdentityClient = new ConfigurationsClient(identityApiBaseUrl, new HttpClient());
            var clearIdentityDatabaseCommand = new ClearDatabaseCommand();
            var clearIdentityDatabaseResponse = await _configurationsIdentityClient.ClearDatabaseAsync(clearIdentityDatabaseCommand);

            var _configurationsMachineLearningClient = new ConfigurationsClient(machineLearningApiBaseUrl, new HttpClient());
            var clearMachineLearningDatabaseCommand = new ClearDatabaseCommand();
            var clearMachineLearningDatabaseResponse = await _configurationsMachineLearningClient.ClearDatabaseAsync(clearMachineLearningDatabaseCommand);

            var _configurationsPaymentClient = new ConfigurationsClient(paymentApiBaseUrl, new HttpClient());
            var clearPaymentDatabaseCommand = new ClearDatabaseCommand();
            var clearPaymentDatabaseResponse = await _configurationsPaymentClient.ClearDatabaseAsync(clearPaymentDatabaseCommand);


            //TODO workaround - problem with downloading model architecture from internet
            var client0Response =  await RunClientRequests(0, identityApiBaseUrl, machineLearningApiBaseUrl, paymentApiBaseUrl, trainFilesPath, predictionFilesPath);

            List<Task<RequestMonitor>> clientTasks = new List<Task<RequestMonitor>>();

            for(int i=1; i<=clientsNumber; i++)
            {
                clientTasks.Add(RunClientRequests(i, identityApiBaseUrl, machineLearningApiBaseUrl, paymentApiBaseUrl, trainFilesPath, predictionFilesPath));
            }

            var results = await Task.WhenAll(clientTasks);

            for(int i=0; i<results.Length; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"Client: {i+1} :");
                results[i].ExecutionLogs
                    .ForEach(
                    res => Console.WriteLine(res.RequestName + ":" + res.RequestTime));
            }

        }

        public static async Task<RequestMonitor> RunClientRequests(int clientNumber, string identityApiBaseUrl, string machineLearningApiBaseUrl, string paymentApiBaseUrl, string trainFilesPath, string predictionFilesPath)
        {

            var _requestMonitor = new RequestMonitor();
            var _identityApihttpClient = new HttpClient();
            var _machineLearningApihttpClient = new HttpClient();
            _identityApihttpClient.Timeout = Timeout.InfiniteTimeSpan;
            var _clientsClient = new ClientsClient(identityApiBaseUrl, _identityApihttpClient);
            var _mlServicesClient = new MLServicesClient(machineLearningApiBaseUrl, _machineLearningApihttpClient);
            var _paymentClient = new PaymentClient(paymentApiBaseUrl, _machineLearningApihttpClient);
            //var _configurationsClient = new ConfigurationsClient(baseUrl, _httpClient);


            var createClientCommand = new CreateClientCommand() { Email = $"test{clientNumber}@mail.com", Name = "testName", Surname = "testSurname" };
            var createClientResponse = await _requestMonitor
                .ExecuteAsync(
                    typeof(CreateClientCommand).Name,
                    () => _clientsClient.CreateClientAsync(createClientCommand));

            var createMLServiceCommand = new CreateMLServiceCommand() { ClientId = createClientResponse.Response, ServiceName = "test" };
            var createMlServiceResponse = await _requestMonitor
                .ExecuteAsync(
                    typeof(CreateMLServiceCommand).Name,
                    () => _mlServicesClient.CreateMLServiceAsync(createMLServiceCommand));

            var mlServiceId = createMlServiceResponse.Response;

            var trainFilePaths = Directory.GetFiles(trainFilesPath);
            (var trainFiles, var trainFileNames) = await PrepareFilesToPost(trainFilePaths);
            var uploadLeaningFilesUrl = $"{machineLearningApiBaseUrl}/api/MLServices/{mlServiceId}/learning/files";
            var uploadLearningFilesResponse = await _requestMonitor
                .ExecuteAsync(
                "UploadLearningFilesCommand",
                () => UploadFiles<HandlerResponse>(uploadLeaningFilesUrl, trainFiles, trainFileNames));

            var a = uploadLearningFilesResponse.RequestMiliseconds;

            var performLearningResponse = await _requestMonitor
                .ExecuteAsync(
                "PerformLearningCommand",
                () => _mlServicesClient.PerformLearningAsync(mlServiceId));

            var predictionFilePaths = Directory.GetFiles(predictionFilesPath);
            (var predictionFiles, var predictionFileNames) = await PrepareFilesToPost(predictionFilePaths);
            var uploadPredictionFilesUrl = $"{machineLearningApiBaseUrl}/api/MLServices/{mlServiceId}/prediction/files";
            var uploadPredictionFilesResponse = await _requestMonitor
                .ExecuteAsync(
                "UploadPredictionFilesCommand",
                () => UploadFiles<CommandHandlerResponse>(uploadPredictionFilesUrl, predictionFiles, predictionFileNames));

            //var res = await _mlServicesClient.MakePredictionAsync(mlServiceId);

            var makePredictionResponse = await _requestMonitor
                .ExecuteAsync(
                "MakePredictionCommand",
                () => _mlServicesClient.MakePredictionAsync(mlServiceId));

            var getInvoicesResponse = await _requestMonitor
                .ExecuteAsync(
                "GetInvoicesQuery",
                () => _paymentClient.GetInvoicesAsync(createClientResponse.Response.ToString()));


            return _requestMonitor;
        }

        public static Task<(List<byte[]> files, List<string> fileNames)>
            PrepareFilesToPost(IEnumerable<string> filePaths)
        {
            List<byte[]> files = new List<byte[]>();
            List<string> fileNames = new List<string>();

            foreach (var trainFilePath in filePaths)
            {
                var bytes = File.ReadAllBytes(trainFilePath);

                files.Add(bytes);

                var pathSegments = trainFilePath.Split('\\');
                var name = pathSegments[pathSegments.Length - 1];

                fileNames.Add(name);
            }

            return Task.FromResult((files, fileNames));
        }

        public static async Task<TResponse> UploadFiles<TResponse>
            (string url, IEnumerable<byte[]> images,
            IEnumerable<string> fileNames)
            where TResponse : class
        {
            using (var client = new HttpClient())
            {
                using (var content =
                    new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
                {
                    for(int i=0; i< images.Count(); i++)
                    {
                        content.Add(new StreamContent(new MemoryStream(images.ElementAt(i))), "files", fileNames.ElementAt(i));
                    }

                    using (
                       var message =
                           await client.PostAsync(url, content))
                    {
                        //var input = await message.Content.ReadAsAsync<TResponse>();

                        return await message.Content.ReadAsAsync<TResponse>();

                        // return !string.IsNullOrWhiteSpace(input) ? Regex.Match(input, @"http://\w*\.directupload\.net/images/\d*/\w*\.[a-z]{3}").Value : null;
                    }
                }
            }
        }
    }
}
