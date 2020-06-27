using FluentValidation;

namespace SimpleCloud_Monolithic.Application.MLServices.Commands.UploadPredictionFiles
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
