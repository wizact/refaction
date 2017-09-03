using System;

namespace refactor_me.Business.Mappers.Interfaces
{
    public interface IMapper { }

    public interface IMapper<ModelType, EntityType>: IMapper
    {
        bool AppliesTo(Type modelType, Type entityType);

        ModelType ToModel(EntityType entity);

        EntityType ToEntity(ModelType model);
    }
}
