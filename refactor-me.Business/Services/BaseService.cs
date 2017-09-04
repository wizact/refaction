using refactor_me.Business.Mappers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace refactor_me.Business.Services
{
    public abstract class BaseService
    {
        internal IList<IMapper> Mappers;

        internal IMapper<TModelType, TEntityType> GetMapper<TModelType, TEntityType>()
        {
            return Mappers.FirstOrDefault(m => (m as IMapper<TModelType, TEntityType>) != null) as IMapper<TModelType, TEntityType>;
        }
    }
}
