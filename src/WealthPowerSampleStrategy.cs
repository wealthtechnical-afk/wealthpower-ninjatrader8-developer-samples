// WealthPower Sample Strategy - NinjaTrader 8
// Educational example showing how a strategy can use the Bullish / Bearish signals
// that every WealthPower indicator (Wealth101 ... Wealth130) publishes.
// It is NOT a trading recommendation and has not been optimised. Test only in Sim / Playback.
// Requires the WealthPower package (trial or developer access): https://www.wealthtechnical.com/developers-area.html
#region Using declarations
using System;
using System.ComponentModel.DataAnnotations;
using NinjaTrader.Cbi;
using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.Indicators;
#endregion

namespace NinjaTrader.NinjaScript.Strategies.WealthTechnicalSamples
{
    public class WealthPowerSampleStrategy : Strategy
    {
        private Wealth101TrendStarter trend;   // swap for any of the 30 WealthPower indicators

        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Name                         = "WealthPowerSampleStrategy";
                Description                  = "Sample: goes long on a WealthPower Bullish signal and short on a Bearish signal. Educational only.";
                Calculate                    = Calculate.OnBarClose;
                EntriesPerDirection          = 1;
                EntryHandling                = EntryHandling.AllEntries;
                IsExitOnSessionCloseStrategy = true;
                BarsRequiredToTrade          = 200;   // WealthPower indicators start signalling after 180 bars
                StopTicks                    = 40;
                TargetTicks                  = 80;
            }
            else if (State == State.Configure)
            {
                SetStopLoss(CalculationMode.Ticks, StopTicks);
                SetProfitTarget(CalculationMode.Ticks, TargetTicks);
            }
            else if (State == State.DataLoaded)
            {
                trend = Wealth101TrendStarter();
                AddChartIndicator(trend);
            }
        }

        protected override void OnBarUpdate()
        {
            if (CurrentBar < BarsRequiredToTrade)
                return;

            // A WealthPower signal exists on a bar only when the indicator set a value there.
            // Bullish[0] holds the price level of the up-triangle (below the bar),
            // Bearish[0] holds the price level of the down-triangle (above the bar).
            bool bullish = trend.Bullish.IsValidDataPoint(0);
            bool bearish = trend.Bearish.IsValidDataPoint(0);

            if (bullish && !bearish)
                EnterLong("WP Long");
            else if (bearish && !bullish)
                EnterShort("WP Short");
        }

        [Range(1, int.MaxValue), NinjaScriptProperty]
        [Display(Name = "Stop (ticks)", Order = 1, GroupName = "Parameters")]
        public int StopTicks { get; set; }

        [Range(1, int.MaxValue), NinjaScriptProperty]
        [Display(Name = "Target (ticks)", Order = 2, GroupName = "Parameters")]
        public int TargetTicks { get; set; }
    }
}
