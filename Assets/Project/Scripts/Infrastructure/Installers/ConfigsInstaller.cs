using Project.Scripts.Infrastructure.Configs.SerializableData;
using Project.Scripts.Plugins.JsonUtilities;
using Zenject;

namespace Project.Scripts.Infrastructure.Installers
{
    public class ConfigInstaller : MonoInstaller
    {
        private const string PlayerdataJson = "PlayerData.json";
        private const string BulletdataJson = "BulletData.json";
        private const string UfodataJson = "UfoData.json";
        private const string AsteroidsdataJson = "AsteroidsData.json";

        public override void InstallBindings()
        {
            Container.Bind<JsonLoader>().AsSingle().NonLazy();

            JsonLoader loader = Container.Resolve<JsonLoader>();

            BindConfig<PlayerData>(loader, PlayerdataJson);

            BindConfig<BulletData>(loader, BulletdataJson);

            BindConfig<UfoData>(loader, UfodataJson);

            BindConfig<AsteroidsData>(loader, AsteroidsdataJson);
        }

        private void BindConfig<T>(JsonLoader loader, string fileName) where T : class
        {
            T config = loader.Load<T>(fileName);

            Container.Bind<T>().FromInstance(config).AsSingle().NonLazy();
        }
    }
}