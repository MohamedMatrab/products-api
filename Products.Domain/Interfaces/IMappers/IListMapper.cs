namespace Products.Domain.Interfaces.IMappers;

public interface IListMapper<TSource, TDestination>
    where TSource:class where TDestination:class
{
    IEnumerable<TDestination> MapList(IEnumerable<TSource> source);
}