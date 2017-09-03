using refactor_me.Business.Mappers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace refactor_me.Business.Services
{
    public abstract class BaseService
    {
        internal IList<IMapper> _mappers;

        internal IMapper<ModelType, EntityType> GetMapper<ModelType, EntityType>()
        {
            return _mappers.FirstOrDefault(m => (m as IMapper<ModelType, EntityType>) != null) as IMapper<ModelType, EntityType>;
        }
    }
}
