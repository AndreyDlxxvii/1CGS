using Code.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Views
{
    public class MainView : View
    {
        [SerializeField] private Button _showChanger;
        [SerializeField] private Button _showConsumables;
        
        [SerializeField] private TextMeshProUGUI _creditCount;
        [SerializeField] private TextMeshProUGUI _coinCount;
        [SerializeField] private TextMeshProUGUI _medPacketCount;
        [SerializeField] private TextMeshProUGUI _armorPlateCount;
        
        public ViewNames ViewName => ViewNames.MainView;
        
        [Inject] private IUiController _uiController;

        protected override void OnOpen()
        {
            GameModel.ModelChanged += Refresh;
            _showChanger.onClick.AddListener(() => _uiController.TryOpenView(ViewNames.ConvertView));
            _showConsumables.onClick.AddListener(() => _uiController.TryOpenView(ViewNames.BuyConsumeView));
            Refresh();
        }

        protected override void Refresh()
        {
            _creditCount.text = NumberSeparator.Separate(GameModel.CreditCount);
            _coinCount.text = NumberSeparator.Separate(GameModel.CoinCount);
            _medPacketCount.text = NumberSeparator.Separate(GameModel.GetConsumableCount(GameModel.ConsumableTypes.Medpack));
            _armorPlateCount.text = NumberSeparator.Separate(GameModel.GetConsumableCount(GameModel.ConsumableTypes.ArmorPlate));
        }

        protected override void OnClose()
        {
            GameModel.ModelChanged -= Refresh;
            _showChanger.onClick.RemoveAllListeners();
            _showConsumables.onClick.RemoveAllListeners();
        }
    }
}