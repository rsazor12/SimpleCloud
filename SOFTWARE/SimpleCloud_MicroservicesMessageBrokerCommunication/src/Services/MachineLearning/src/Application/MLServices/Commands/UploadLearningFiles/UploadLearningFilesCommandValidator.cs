using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearning_SimpleCloud_Broker.Application.MLServices.Commands.UploadLearningFiles
{
    public class UploadLearningFilesCommandValidator : AbstractValidator<UploadLearningFilesCommand>
    {
        public UploadLearningFilesCommandValidator()
        {
            RuleFor(command => command.files)
                .NotEmpty();
        }
    }
}
