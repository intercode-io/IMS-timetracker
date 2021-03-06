﻿namespace Timetracker.BLL.Mappers.Interfaces
{
    public interface IMapper<TSource, TDestination>
    {
        TSource Map(TDestination source);

        TDestination Map(TSource source);
    }
}
