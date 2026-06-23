using Project.Scripts.Infrastructure.Configs.SerializableData;
using Project.Scripts.Plugins.JsonUtilities;
using Zenject;

namespace Project.Scripts.Infrastructure.Installers
{
    public class ConfigInstaller : MonoInstaller
    {
        private const string ShipDataJson = "ShipData.json";
        private const string BulletDataJson = "BulletData.json";
        private const string LaserDataJson = "LaserData.json";
        private const string UfoDataJson = "UfoData.json";
        private const string AsteroidsDataJson = "AsteroidsData.json";

        public override void InstallBindings()
        {
            Container.Bind<JsonLoader>().AsSingle().NonLazy();

            JsonLoader loader = Container.Resolve<JsonLoader>();

            BindConfig<ShipData>(loader, ShipDataJson);

            BindConfig<BulletData>(loader, BulletDataJson);

            BindConfig<LaserData>(loader, LaserDataJson);

            BindConfig<UfoData>(loader, UfoDataJson);

            BindConfig<AsteroidsData>(loader, AsteroidsDataJson);
        }

        private void BindConfig<T>(JsonLoader loader, string fileName) where T : class
        {
            T config = loader.Load<T>(fileName);

            Container.Bind<T>().FromInstance(config).AsSingle().NonLazy();
        }
    }
}
