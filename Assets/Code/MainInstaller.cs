using Code.Configs;
using Code.UI;
using UnityEngine;
using Zenject;

namespace Code
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private PrefabViewConfigs _viewConfigs;
        [SerializeField] private UiController _uiController;
        
        public override void InstallBindings()
        {
            BindConfiguration();
            BindServices();
            BindFactories();
        }

        private void BindConfiguration()
        {
            Container.Bind<IViewConfigs>().FromInstance(_viewConfigs).AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<IUiController>().FromInstance(_uiController).AsSingle();
        }
    
        private void BindFactories()
        {
            Container.Bind<IAbstractFactory>().To<AbstractFactory>().FromInstance(new AbstractFactory(Container)).AsSingle();
        }
    }
}