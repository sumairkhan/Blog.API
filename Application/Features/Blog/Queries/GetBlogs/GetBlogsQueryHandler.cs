using Application.Interface;
using MediatR;
using de = Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Application.Features.Blog.Queries.GetBlogs
{
    public class GetBlogsQueryHandler : IRequestHandler<GetBlogsQuery, GetBlogsResponse>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<de.Blog> _repository; 

        public GetBlogsQueryHandler(IRepository<de.Blog> repository, IMapper mapper, ILogger<GetBlogsQueryHandler> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<GetBlogsResponse> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            var getBlogResponse = new GetBlogsResponse();
            var validator = new GetBlobValidator();

            try
            {
                var validationResult = await validator.ValidateAsync(request, new CancellationToken());
                if (validationResult.Errors.Count > 0)
                {
                    getBlogResponse.Success = false;
                    getBlogResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        getBlogResponse.ValidationErrors.Add(error);
                        _logger.LogError($"validation failed due to error {error} ");
                    }
                }
                else if (getBlogResponse.Success) 
                {
                    var result = await _repository.GetAllAsync();
                    getBlogResponse.Blogs = _mapper.Map<List<BlogDto>>(result); 
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Error: {ex.Message}");   
                getBlogResponse.Success = false;
                getBlogResponse.Message = ex.Message;
            }

            return getBlogResponse;
        }
    }
}
