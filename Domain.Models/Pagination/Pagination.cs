
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Pagination
{
    public class Pagination <TEntity>
        where TEntity : class
    {
        public int PageNumber { get; set; }
        public int RowNumberRequest { get; set; }
        public int RowNumberReturned { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<TEntity> Record { get; set; }

    }
}
