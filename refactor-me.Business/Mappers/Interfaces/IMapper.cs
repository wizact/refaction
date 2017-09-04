namespace refactor_me.Business.Mappers.Interfaces
{
    public interface IMapper { }

    public interface IMapper<TModelType, TEntityType>: IMapper
    {
        TModelType ToModel(TEntityType entity);

        TEntityType ToEntity(TModelType model);
    }
}
