using System;
using cAlgo.API;

namespace cAlgo.Indicators
{
    [Indicator(IsOverlay = true, TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class RoundLevels : Indicator
    {
        #region Parameters

        [Parameter(name: "Colour", DefaultValue = "Gray", Group = "Line Properties")]
        public string Colour { get; set; }

        [Parameter(name: "Opacity (%)", DefaultValue = 100, MinValue = 0, MaxValue = 100, Step = 5, Group = "Line Properties")]
        public int Opacity { get; set; }

        [Parameter(name: "Thickness", DefaultValue = 1, MinValue = 1, MaxValue = 5, Group = "Line Properties")]
        public int Thickness { get; set; }

        [Parameter(name: "Step Value", DefaultValue = 50, Group = "Levels")]
        public double StepValue { get; set; }

        [Parameter(name: "Maximum Levels", DefaultValue = 10, Group = "Levels")]
        public int MaxLevels { get; set; }

        #endregion Parameters

        public override void Calculate(int index)
        {
        }

        protected override void Initialize()
        {
            CreateGrid();
        }

        #region Create Round Levels

        private void CreateGrid()
        {
            int maxLines = MaxLevels * 2;

            StepValue *= Symbol.PipSize;
            double startLevel = Math.Round(Bars.ClosePrices.LastValue / StepValue) * StepValue;

            for (int i = 0; i < maxLines - 1; i++)
            {
                int multiplier = (i < MaxLevels) ? 1 : -1;
                double level = startLevel + (i % MaxLevels) * StepValue * multiplier;

                Color lineColour = Color.FromArgb(MapOpacityValue(Opacity), Colour.ToString());
                Chart.DrawHorizontalLine(name: "Level" + i, y: level, color: lineColour, thickness: Thickness);
            }
        }

        private int MapOpacityValue(int opacity)
        {
            return opacity * 255 / 100;
        }

        #endregion Create Round Levels
    }
}