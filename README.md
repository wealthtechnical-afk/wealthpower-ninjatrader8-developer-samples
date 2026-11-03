# WealthPower – NinjaTrader 8 Developer Samples

Sample NinjaScript (C#) code showing how to build **add-ons, strategies and scanners on top of the 30 WealthPower indicators** for NinjaTrader 8.

> **Developer program:** eligible developers, brokers and fund houses get **free access** to all 30 WealthPower indicators.
> Details and application: **https://www.wealthtechnical.com/developers-area.html**

---

## What's inside

| File | What it does |
|---|---|
| `src/WealthPowerValuesLogger.cs` | Indicator that prints the live values (and plot names) of WealthPower indicators to the NinjaScript Output window — start here to see which value lives in which plot. |
| `src/WealthPowerSampleStrategy.cs` | Minimal strategy that goes long on a WealthPower **Bullish** signal and short on a **Bearish** signal. Educational only. |

## Requirements

1. **NinjaTrader 8** (free download for charting and simulation).
2. **WealthPower indicators** installed in NinjaTrader 8:
   - 3-day free trial, or
   - free developer access for eligible developers — [apply here](https://www.wealthtechnical.com/developers-area.html).

## Install the samples

1. Copy `WealthPowerValuesLogger.cs` to `Documents\NinjaTrader 8\bin\Custom\Indicators\`
2. Copy `WealthPowerSampleStrategy.cs` to `Documents\NinjaTrader 8\bin\Custom\Strategies\`
3. In NinjaTrader 8 open **New → NinjaScript Editor** and press **F5** to compile.
4. Add **WealthPowerValuesLogger** to a chart and open **New → NinjaScript Output** to see the values.

## Calling WealthPower from your own code

Every WealthPower indicator is available as a normal NinjaScript method.
**Every one of the 30 indicators publishes two public signal series: `Bullish` and `Bearish`.**

```csharp
var trend = Wealth101TrendStarter();               // in State.DataLoaded

// in OnBarUpdate:
if (trend.Bullish.IsValidDataPoint(0))             // bullish signal on this bar
{
    double level = trend.Bullish[0];               // price level of the up-triangle (bar low − 1 tick)
}
if (trend.Bearish.IsValidDataPoint(0))             // bearish signal on this bar
{
    double level = trend.Bearish[0];               // price level of the down-triangle (bar high + 1 tick)
}
```

| Series | Plot index | Meaning |
|---|---|---|
| `Bullish` | `Values[0]` | Set only on bars with a bullish signal; value = price level of the signal |
| `Bearish` | `Values[1]` | Set only on bars with a bearish signal; value = price level of the signal |

Notes:
- Bars with no signal hold no value, so always test with `IsValidDataPoint(0)` (not `> 0`).
- Signals start after 180 bars of history; keep `BarsRequiredToTrade` at 200 or more.
- Indicators calculate on bar close.
- You can combine several indicators, for example count how many of the 30 are bullish on the current bar.

Available indicators: `Wealth101TrendStarter`, `Wealth102TrendContinuation`, `Wealth103SupportResistance`,
`Wealth104MarketConfusion`, `Wealth105Macd`, `Wealth106Stochastics` … `Wealth110Stochastics`,
`Wealth111Ma` … `Wealth130Ma`.

## Developer program rules (summary)

To receive free developer access you must meet **all** conditions:

1. Developments are based only on WealthTechnical indicator values.
2. They do not change or affect the original indicator values.
3. Every licence you sell of your development pays the original WealthPower licence fee.
4. You have your own website.
5. All communication comes from your official email address.
6. You submit complete details via the application form.

Full details: https://www.wealthtechnical.com/developers-area.html

## Licence

The **sample code in this repository** is released under the MIT licence (see `LICENSE`).
The **WealthPower indicators themselves are proprietary** software of WealthTechnical LLP and are not included here.

## Disclaimer

Trading involves substantial risk of loss. These samples are for education and development only and are not
investment advice or a recommendation to trade. Test in simulation first. Past or hypothetical performance does not
guarantee future results. NinjaTrader® is a registered trademark of NinjaTrader Group, LLC.

---
© 2014–2026 WealthTechnical LLP · https://www.wealthtechnical.com · wealth@wealthtechnical.com
