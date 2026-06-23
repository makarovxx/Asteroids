using MVVM;
using UnityEngine;
using Zenject;

namespace Project.Scripts.UI.Binders.MonoBinders
{
    public abstract class MonoViewBinder<TView, TViewModel> : MonoBehaviour where TView : class where TViewModel : class
    {
        [SerializeField] private TView _viewType;
        
        private TViewModel _viewModelType;
        private IBinder _binder;

        [Inject]
        private void Construct(TViewModel viewModel) => _viewModelType = viewModel;

        private void Awake() => _binder = CreateBinder();

        private void OnEnable() => _binder.Bind();

        private void OnDisable() => _binder.Unbind();

        private IBinder CreateBinder() => BinderFactory.CreateComposite(_viewType, _viewModelType);
    }
}