using System;
using Domain.Models.Samples;
using Domain.Models.Pagination;

namespace Domain.Samples
{
    public interface ISampleRepository : IRepository<Sample>
    {
        Pagination<Sample> Paginate(int pageNumber, int rowNumber);
        
    }
}