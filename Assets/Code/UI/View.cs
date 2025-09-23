using Code.Configs;
using UnityEngine;

namespace Code.UI
{
    public abstract class View : MonoBehaviour, IUiView
    {
        private bool _isActivated;
        
        public ViewNames Name { get; }

        public void Open()
        {
            transform.SetAsLastSibling();
            
            if (!_isActivated)
            {
                _isActivated = true;
                gameObject.SetActive(true);
                OnOpen();
            }
            else
                Refresh();
        }

        public void Close()
        {
            gameObject.SetActive(false);
            _isActivated = false;
            OnClose();
        }
        
        protected virtual void OnOpen() { }
        protected virtual void OnClose() { }
        protected virtual void Refresh() { }
    }
}