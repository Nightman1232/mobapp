using Microsoft.Maui;
using System.Globalization;

namespace PENTADO_Lab2
{
    public partial class MainPage : ContentPage
    {
        private double _billAmount = 0;
        private double _tipPercent = 0;
        private int _splitCount = 1;

        public MainPage()
        {
            InitializeComponent();
            Calculate();
        }

        private void OnBillChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(e.NewTextValue?.Replace(",", "."),
                NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
            {
                _billAmount = value;
            }
            else
            {
                _billAmount = 0;
            }
            Calculate();
        }

        private void OnTipPercentClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is string param
                && int.TryParse(param, out int percent))
            {
                _tipPercent = percent;
                TipSlider.Value = percent;
                TipPercentLabel.Text = $"Tip: {percent}%";
                Calculate();
            }
        }

        private void OnTipSliderChanged(object sender, ValueChangedEventArgs e)
        {
            _tipPercent = Math.Round(e.NewValue);
            TipPercentLabel.Text = $"Tip: {_tipPercent}%";
            Calculate();
        }

        private void OnDecreaseSplit(object sender, EventArgs e)
        {
            if (_splitCount > 1)
            {
                _splitCount--;
                SplitLabel.Text = _splitCount.ToString();
                Calculate();
            }
        }

        private void OnIncreaseSplit(object sender, EventArgs e)
        {
            _splitCount++;
            SplitLabel.Text = _splitCount.ToString();
            Calculate();
        }

        private void Calculate()
        {
            double tipAmount = _billAmount * (_tipPercent / 100.0);
            double total = _billAmount + tipAmount;
            double perPerson = _splitCount > 0 ? total / _splitCount : total;

            var us = CultureInfo.GetCultureInfo("en-US");

            SubtotalLabel.Text = _billAmount.ToString("C", us);
            TipAmountLabel.Text = tipAmount.ToString("C", us);
            TotalPerPersonLabel.Text = perPerson.ToString("C", us);
        }
    }
}