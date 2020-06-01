using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimpleCloud_Monolithic.Application.Common.Configurations;
using SimpleCloud_Monolithic.Application.Models.HandlerResponse;
using SimpleCloud_Monolithic.Domain.Entities;
using SimpleCloudMonolithic.Application.Common.Exceptions;
using SimpleCloudMonolithic.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCloud_Monolithic.Application.MLServices.Commands.UploadPredictionFiles
{
    public class UploadPredictionFilesCommand : IRequest<CommandHandlerResponse>
    {
        public Guid MLServiceId { get; set; }

        public IEnumerable<IFormFile> files { get; set; }
    }

    public class UploadPredictionFilesCommandHandler : IRequestHandler<UploadPredictionFilesCommand, CommandHandlerResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly AppSettings AppSettings;
        private readonly IMapper _mapper;

        public UploadPredictionFilesCommandHandler(
            IOptions<AppSettings> settings,
            IApplicationDbContext dbContext,
            IMapper mapper)
        {
            AppSettings = settings.Value;
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<CommandHandlerResponse> Handle(UploadPredictionFilesCommand request, CancellationToken cancellationToken)
        {
            var mlService = await _dbContext.MLServices
                .Include(mlService => mlService.ServiceDetails)
                .Include(mlService => mlService.Client)
                //.ProjectTo<MLService>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(mlService => mlService.Id == request.MLServiceId)
                ?? throw new NotFoundException(nameof(MLService), request.MLServiceId);

            try
            {
                string path = Path.Combine(AppSettings.FileStorageSettings.Path, request.MLServiceId + "/Images/Testing/");

                Directory.CreateDirectory(path);

                foreach (IFormFile learningFile in request.files)
                {
                    var filePath = path + learningFile.FileName;

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await learningFile.CopyToAsync(stream);
                    }
                }

                mlService.UpdateTestingFilesPath(path);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                // TODO - revert uploaded files

                throw ex;
            }


            return new CommandHandlerResponse();
        }
    }
}
