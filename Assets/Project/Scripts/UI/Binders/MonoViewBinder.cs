using MVVM;
using UnityEngine;
using Zenject;

namespace Project.Scripts.UI.Binders
{
    public abstract class MonoViewBinder<TView, TViewModel> : MonoBehaviour where TView : class where TViewModel : class
    {
        [SerializeField] private TView _viewType;

        [Inject] private TViewModel _viewModelType;

        private IBinder _binder;

        private void Awake()
        {
            _binder = this.CreateBinder();
        }

        private void OnEnable()
        {
            _binder.Bind();
        }

        private void OnDisable()
        {
            _binder.Unbind();
        }

        private IBinder CreateBinder()
        {
            return BinderFactory.CreateComposite(_viewType, _viewModelType);
        }
    }
}