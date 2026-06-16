using System;
using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Weapons;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Entities.Ship
{
    public class Ship : Entity<ShipPhysics>, IInitializable, IDisposable, ITickable
    {
        public override EntityType EntityType => EntityType.Ship;
        private BulletLauncher _bulletLauncher;

        [Inject]
        private void Construct(BulletLauncher launcher)
        {
            _bulletLauncher = launcher;
        }

        public void Initialize()
        {
            Debug.Log("Ship initialized");
        }

        public void Dispose()
        {
            Debug.Log("Ship disposed");
        }

        public void Tick()
        {
            // if (_desktopInput.IsFireBulletLauncher())
            // {
            //     Debug.Log("FireBullet");
            //
            //     _bulletLauncher.FireBullet(Physics.Position, Physics.Rotation,
            //         Physics.DirectionBodyDefault * Physics.Rotation);
            // }
        }
    }
}