using System;
using Code.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Views
{
    public class BuyConsumeView : View
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _buyMedPacket;
        [SerializeField] private Button _buyArmorPlate;
        
        [SerializeField] private TextMeshProUGUI _totalMedPacket;
        [SerializeField] private TextMeshProUGUI _totalArmorPlate;
        
        [SerializeField] private TextMeshProUGUI _priceMedPacket;
        [SerializeField] private TextMeshProUGUI _priceArmorPlate;
        
        private int _armorPlatePrice => GameModel.ConsumablesPrice[GameModel.ConsumableTypes.ArmorPlate].CoinPrice; 
        private int _medPacketPrice => GameModel.ConsumablesPrice[GameModel.ConsumableTypes.Medpack].CreditPrice;
        
        public ViewNames ViewName => ViewNames.ConvertView;

        protected override void OnOpen()
        {
            _closeButton.onClick.AddListener(Close);
            _buyMedPacket.onClick.AddListener(TryBuyMedPacket);
            _buyArmorPlate.onClick.AddListener(TryBuyArmorPlate);
            GameModel.ModelChanged += Refresh;
            Refresh();
        }

        private void TryBuyArmorPlate()
        {
            if (_armorPlatePrice > GameModel.CoinCount) 
                return;
            
            GameModel.BuyConsumableForCoin(GameModel.ConsumableTypes.ArmorPlate);
            _buyArmorPlate.interactable = false;
        }

        private void TryBuyMedPacket()
        {
            if (_armorPlatePrice > GameModel.CreditCount) 
                return;
            
            GameModel.BuyConsumableForCredit(GameModel.ConsumableTypes.Medpack);
            _buyMedPacket.interactable = false;
        }

        protected override void Refresh()
        {
            _buyArmorPlate.interactable = GameModel.CoinCount >= _armorPlatePrice;
            _buyMedPacket.interactable = GameModel.CreditCount >= _medPacketPrice;
            
            _priceMedPacket.text = NumberSeparator.Separate(_medPacketPrice);
            _priceArmorPlate.text = NumberSeparator.Separate(_armorPlatePrice);
            _totalMedPacket.text = NumberSeparator.Separate(GameModel.GetConsumableCount(GameModel.ConsumableTypes.Medpack));
            _totalArmorPlate.text = NumberSeparator.Separate(GameModel.GetConsumableCount(GameModel.ConsumableTypes.ArmorPlate));
        }
        
        protected override void OnClose()
        {
            GameModel.ModelChanged -= Refresh;
            _closeButton.onClick.RemoveAllListeners();
            _buyMedPacket.onClick.RemoveAllListeners();
            _buyArmorPlate.onClick.RemoveAllListeners();
        }
    }
}