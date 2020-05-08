using MediatR;
using SimpleCloudMonolithic.Application.Common.Interfaces;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCloud_Monolithic.Application.MLService.Commands
{
    public class PerformLearningCommand : IRequest
    {
        public string TrainDataFilePath { get; set; }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<PerformLearningCommand>
    {
        private IModelBuilder _modelBuilder;

        public CreateTodoItemCommandHandler(IModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        async Task<Unit> IRequestHandler<PerformLearningCommand, Unit>.Handle(PerformLearningCommand request, CancellationToken cancellationToken)
        {
             
            var (mlContext, mlModel, dataViewSchema) = _modelBuilder.CreateModel(request.TrainDataFilePath);

            //var modelFilePath = GetAbsolutePath();
            _modelBuilder.SaveModel(mlContext, mlModel, "model.zip", dataViewSchema);

            return Unit.Value;
        }

        // TODO - move this method to service
        private string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(PerformLearningCommand).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }
    }
}
