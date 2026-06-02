using Project.Scripts.Entities;
using Project.Scripts.Plugins;

namespace Project.Scripts.EntityFactories
{
    public interface IEntityFactory<TEntity> : ICreator<TEntity>
        where TEntity : PhysicalEntity
    {
    }
}
