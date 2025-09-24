using System.Globalization;
using Code.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Views
{
    public class ConvertView : View
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _changeButton;
        
        [SerializeField] private TextMeshProUGUI _creditCount;
        [SerializeField] private TextMeshProUGUI _coinCount;
        [SerializeField] private TextMeshProUGUI _creditCourse;
        [SerializeField] private TextMeshProUGUI _totalCreditCanGet;

        [SerializeField] private TMP_InputField _inputField;

        private int _changeCourse = 1;
        private int _coinToChange = 0;
        
        public ViewNames ViewName => ViewNames.ConvertView;

        protected override void OnOpen()
        {
            _changeCourse = GameModel.CoinToCreditRate;
            _creditCourse.text = _changeCourse.ToString();

            _inputField.text = Mathf.Min(1, GameModel.CoinCount).ToString();
            
            _inputField.onValueChanged.AddListener(OnInputValueChanged);
            _closeButton.onClick.AddListener(Close);
            _changeButton.onClick.AddListener(ApplyExchange);
            GameModel.ModelChanged += Refresh;
            GameModel.OperationComplete += HandleOperationComplete;
            
            ValidateInput(_inputField.text);
            Refresh();
        }

        private void ApplyExchange()
        {
            if (_coinToChange == 0)
                return;
            
            GameModel.ConvertCoinToCredit(_coinToChange);
            _changeButton.interactable = false;
        }
        
        private void ValidateInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                SetInvalidState();
                return;
            }

            if (int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result) && result > 0)
            {
                if (result <= GameModel.CoinCount)
                {
                    _coinToChange = result;
                    _totalCreditCanGet.text = NumberSeparator.Separate(_changeCourse * result);
                    _changeButton.interactable = true;
                }
                else
                    SetInvalidState();
            }
            else
                SetInvalidState();
        }
        
        private void SetInvalidState()
        {
            _coinToChange = 0;
            _totalCreditCanGet.text = "0";
            _changeButton.interactable = false;
        }

        private void OnInputValueChanged(string value)
        {
            ValidateInput(value);
        }

        protected override void Refresh()
        {
            _creditCount.text = NumberSeparator.Separate(GameModel.CreditCount);
            _coinCount.text = NumberSeparator.Separate(GameModel.CoinCount);
            ValidateInput(_inputField.text);
        }
        
        private void HandleOperationComplete(GameModel.OperationResult result)
        {
            if (!result.IsSuccess)
                Debug.LogError($"[{GetType().Name}] Get some error: {result.ErrorDescription}");
        }
        
        protected override void OnClose()
        {
            _closeButton.onClick.RemoveAllListeners();
            _inputField.onValueChanged.RemoveAllListeners();
            _changeButton.onClick.RemoveAllListeners();
            GameModel.ModelChanged -= Refresh;
            GameModel.OperationComplete -= HandleOperationComplete;
        }
    }
}