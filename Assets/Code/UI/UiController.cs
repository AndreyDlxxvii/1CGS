using System.Collections.Generic;
using Code.Configs;
using UnityEngine;
using Zenject;

namespace Code.UI
{
    public interface IUiController
    {
        void Initialize();
        void TryOpenView(ViewNames name);
        void TryCloseView(ViewNames name);
    }
    
    public class UiController : MonoBehaviour, IUiController
    {
        [SerializeField] private Transform _rootViewTransform;
        
        private readonly Dictionary<ViewNames, IUiView> _views = new();

        [Inject] private IViewConfigs _viewConfigs;
        [Inject] private IAbstractFactory _abstractFactory;

        public void Initialize()
        {
            if (_rootViewTransform == null)
            {
                Debug.LogError($"[{GetType().Name}] Has no root for view");
                return;
            }
            
            for (var i = 0; i < _rootViewTransform.childCount; i++)
            {
                var child = _rootViewTransform.GetChild(i);
                
                if (!child.TryGetComponent(out IUiView view))
                    continue;

                _views[view.Name] = view;
            }
        }

        public void TryOpenView(ViewNames name)
        {
            if (_views.TryGetValue(name, out var view))
                view.Open();
            else
            {
                var config = _viewConfigs.GetConfig(name);

                if (config == null)
                {
                    Debug.LogError($"[{GetType().Name}] View {name} not found");
                    return;
                }

                var viewObject = _abstractFactory.Create(config.Prefab);
                viewObject.transform.SetParent(_rootViewTransform, false);
                viewObject.Open();
                _views.Add(name, viewObject);
            }
        }

        public void TryCloseView(ViewNames name)
        {
            if (_views.TryGetValue(name, out var view))
                view.Close();
        }
    }
}