using FluentValidation;

namespace MachineLearning_SimpleCloud_Broker.Application.MLServices.Commands.UploadPredictionFiles
{
    public class UploadPredictionFilesCommandValidator : AbstractValidator<UploadPredictionFilesCommand>
    {
        public UploadPredictionFilesCommandValidator()
        {
            RuleFor(command => command.files)
                .NotEmpty();
        }
    }
}
