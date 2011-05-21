using System;

namespace Reporting
{
    public abstract class DtoBase
    {
        //public string Id
        //{
        //    get
        //    {
        //        return GetDtoIdOf(AggregateRootId, GetType());
        //    }
        //}

        public Guid Id { get; set; }

        //public static string GetDtoIdOf<T>(Guid id)
        //    where T : DtoBase
        //{
        //    return GetDtoIdOf(id, typeof(T));
        //}

        //public static string GetDtoIdOf(Guid id, Type type)
        //{
        //    return type.Name + "/" + id;
        //}
    }
}
