using System;
using MVVM;
using UniRx;
using UnityEngine.UI;

namespace Project.Scripts.UI.Binders
{
    public class ImageFillBinder : IBinder, IObserver<float>
    {
        private readonly Image _filledImage;
        private readonly IReadOnlyReactiveProperty<float> _property;
        private IDisposable _handle;

        public ImageFillBinder(Image filledImage, IReadOnlyReactiveProperty<float> property)
        {
            _filledImage = filledImage;
            _property = property;
        }

        public void Bind()
        {
            OnNext(_property.Value);
            _handle = _property.Subscribe(this);
        }

        public void Unbind()
        {
            _handle?.Dispose();
            _handle = null;
        }

        public void OnNext(float value)
        {
            _filledImage.fillAmount = value;
        }

        public void OnCompleted()
        { 
        }

        public void OnError(Exception error)
        {
        }
    }
}