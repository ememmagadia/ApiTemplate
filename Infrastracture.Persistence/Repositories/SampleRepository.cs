using Domain.Models.Pagination;
using Domain.Models.Samples;
using Domain.Samples;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Linq;

namespace Infrastracture.Persistence.Repositories
{
    public class SampleRepository
        : RepositoryBase<Sample>, ISampleRepository
    {
        private readonly IApiDbContext context;
        public SampleRepository(IApiDbContext context) : base(context)
        {
            this.context = context;
        }

        public Pagination<Sample> Paginate(int pageNumber, int rowNumber)
        {
            Pagination<Sample> result = new Pagination<Sample>()
            {
                PageNumber = pageNumber > 0 ? pageNumber : 1,
                RowNumberRequest = rowNumber > 0 ? rowNumber : 5,
                TotalCount = this.context.Set<Sample>().Count()
            };

            int skip = 0;
            int take = 5;

            if (pageNumber > 0)
            {
                skip = (pageNumber - 1) * rowNumber;
            }

            if (rowNumber > 0)
            {
                take = rowNumber;
            }

            result.Record = this.context.Set<Sample>().Skip(skip).Take(take).ToList();
            result.RowNumberReturned = result.Record.Count();
            return result;
        }
        public IEnumerable<Sample> LinqTest()
        {

            List<Sample> sampleList = this.context.Set<Sample>().ToList();


            Func<Sample, bool> isTeenAger = delegate (Sample s) { return s.Age > 12 && s.Age < 20; };


            var filteredResult = from s in sampleList
                                 where isTeenAger(s)
                                 orderby s.Age descending
                                 select s;



            var x = sampleList.Where((s, i) =>
            {
                if (i % 2 == 0) return true;
                return false;
            });


            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Abram" , Age = 21 }
            };

            var groupedResult = from s in studentList
                                group s by s.Age;

            //iterate each group        
            foreach (var ageGroup in groupedResult)
            {
                Console.WriteLine("Age Group: {0}", ageGroup.Key); //Each group has a key 

                foreach (Student s in ageGroup) // Each group has inner collection
                    Console.WriteLine("Student Name: {0}", s.StudentName);
            }

            var xx = (from s in filteredResult
                      select s.Age).Count();

            var xxx = filteredResult.Count(s => s.Age > 12);

            return filteredResult;
        }

    }
}