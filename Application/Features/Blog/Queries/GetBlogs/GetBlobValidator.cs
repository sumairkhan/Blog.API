using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blog.Queries.GetBlogs
{
    public class GetBlobValidator: AbstractValidator<GetBlogsQuery>
    {
        public GetBlobValidator()
        {
                
        }
    }
}
