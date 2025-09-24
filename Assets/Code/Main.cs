using Code.Configs;
using Code.UI;
using UnityEngine;
using Zenject;

namespace Code
{
    public class Main : MonoBehaviour
    {
        [Inject] private IUiController _uiController;
        
        private bool _isInitialized;

        private void Start()
        {
            _uiController.Initialize();
            _uiController.TryOpenView(ViewNames.MainView);
            _isInitialized = true;
        }

        private void Update()
        {
            if (!_isInitialized)
                return;

            if (!GameModel.HasRunningOperations)
                return;
            
            GameModel.Update();
        }
    }
}