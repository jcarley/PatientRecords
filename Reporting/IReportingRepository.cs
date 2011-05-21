using System;
using System.Collections.Generic;

namespace Reporting
{
    public interface IReportingRepository<T>
        where T : DtoBase
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Func<T, bool> predicate);
        T GetById(Guid id);

        void Insert(T dto);
        void Update(T dto);
        void Delete(T dto);
    }
}