namespace IMS_Timetracker.Abstraction
{
    public interface IMapper<TSource, TDestination>
    {
        TSource Map(TDestination source);

        TDestination Map(TSource source);
    }
}