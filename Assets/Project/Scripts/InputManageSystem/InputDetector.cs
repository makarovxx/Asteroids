using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Project.Scripts.InputManageSystem
{
    public sealed class InputDetector : IInitializable, ITickable
    {
        private readonly IReadOnlyList<IInputStrategy> _strategies;

        public IInputStrategy ActiveStrategy { get; private set; }

        public InputDetector(IEnumerable<IInputStrategy> strategies )
        {
            _strategies = new List<IInputStrategy>(strategies);
        }

        public void Initialize()
        {
            InputDeviceType initialDevice = Application.isMobilePlatform
                ? InputDeviceType.Mobile
                : InputDeviceType.Desktop;

            SetActiveStrategy(GetStrategy(initialDevice));
        }

        public void Tick()
        {
            IInputStrategy desktop = GetStrategy(InputDeviceType.Desktop);
            IInputStrategy mobile = GetStrategy(InputDeviceType.Mobile);

            if (desktop.HasActivity)
            {
                SetActiveStrategy(desktop);
                return;
            }

            if (mobile.HasActivity)
                SetActiveStrategy(mobile);
        }

        private IInputStrategy GetStrategy(InputDeviceType deviceType)
        {
            IInputStrategy strategy = _strategies.FirstOrDefault(item => item.DeviceType == deviceType);

            return strategy;
        }

        private void SetActiveStrategy(IInputStrategy strategy)
        {
            if (ActiveStrategy == strategy)
                return;

            ActiveStrategy = strategy;
        }
    }
}
