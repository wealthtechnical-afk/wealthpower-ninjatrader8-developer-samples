// WealthPower Values Logger - sample NinjaScript indicator for NinjaTrader 8
// Prints the current values of selected WealthPower indicators to the NinjaScript Output window,
// so developers can see the Bullish (Values[0]) and Bearish (Values[1]) signals before building on them.
// Requires the WealthPower package (trial or developer access): https://www.wealthtechnical.com/developers-area.html
#region Using declarations
using System;
using System.ComponentModel.DataAnnotations;
using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.Indicators;
#endregion

namespace NinjaTrader.NinjaScript.Indicators.WealthTechnicalSamples
{
    public class WealthPowerValuesLogger : Indicator
    {
        private Wealth101TrendStarter trendStarter;
        private Wealth103SupportResistance supportResistance;
        private Wealth105Macd wealthMacd;

        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Name        = "WealthPowerValuesLogger";
                Description = "Sample: prints WealthPower indicator values to the Output window.";
                Calculate   = Calculate.OnBarClose;
                IsOverlay   = true;
                PrintEveryBar = false;
            }
            else if (State == State.DataLoaded)
            {
                trendStarter      = Wealth101TrendStarter();
                supportResistance = Wealth103SupportResistance();
                wealthMacd        = Wealth105Macd();
            }
        }

        protected override void OnBarUpdate()
        {
            if (CurrentBar < 50)
                return;
            if (!PrintEveryBar && State != State.Realtime && CurrentBar < Count - 2)
                return;

            Print(string.Format("{0} {1}", Instrument.FullName, Time[0]));
            PrintPlots("Wealth101TrendStarter", trendStarter);
            PrintPlots("Wealth103SupportResistance", supportResistance);
            PrintPlots("Wealth105Macd", wealthMacd);
        }

        private void PrintPlots(string label, Indicator ind)
        {
            for (int i = 0; i < ind.Values.Length; i++)
                Print(string.Format("  {0}.Values[{1}] ({2}) = {3}", label, i, ind.Plots[i].Name,
                    ind.Values[i].IsValidDataPoint(0) ? ind.Values[i][0].ToString() : "no signal on this bar"));
        }

        [NinjaScriptProperty]
        [Display(Name = "Print every bar", Order = 1, GroupName = "Parameters")]
        public bool PrintEveryBar { get; set; }
    }
}
