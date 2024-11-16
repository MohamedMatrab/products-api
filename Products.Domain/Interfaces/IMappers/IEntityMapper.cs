namespace Products.Domain.Interfaces.IMappers;

public interface IEntityMapper<TSource, TDestination> 
    where TSource:class where TDestination:class
{
    TDestination MapModel(TSource source);
    TDestination MapModel(TSource source, TDestination destination);
}