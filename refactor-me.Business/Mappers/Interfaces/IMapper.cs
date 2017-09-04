namespace refactor_me.Business.Mappers.Interfaces
{
    public interface IMapper { }

    public interface IMapper<ModelType, EntityType>: IMapper
    {
        ModelType ToModel(EntityType entity);

        EntityType ToEntity(ModelType model);
    }
}
