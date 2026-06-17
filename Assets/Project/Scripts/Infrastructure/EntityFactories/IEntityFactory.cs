using Project.Scripts.Gameplay.Entities;
using Project.Scripts.Plugins;

namespace Project.Scripts.Infrastructure.EntityFactories
{
    public interface IEntityFactory<TEntity> : ICreator<TEntity>
        where TEntity : Entity
    {
    }
}
