// ------------------------------------------------------------------------------------------------------
// LightningChart® example code:  Simple 3D SurfaceGrid Chart Demo.
//
// If you need any assistance, or notice error in this example code, please contact support@arction.com. 
//
// Permission to use this code in your application comes with LightningChart® license. 
//
// http://arction.com/ | support@arction.com | sales@arction.com
//
// © Arction Ltd 2009-2019. All rights reserved.  
// ------------------------------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;

// Arction namespaces.
using Arction.WinForms.Charting;            // LightningChartUltimate and general types.
using Arction.WinForms.Charting.Series3D;   // Series for 3D chart.

namespace Simple3DSurfaceGridSeriesTutorial_WF
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// LightningChart component.
        /// </summary>
        private LightningChartUltimate _chart = null;

        /// <summary>
        /// Reference to SurfaceGrid series in chart.
        /// </summary>
        private SurfaceGridSeries3D _surfaceGrid = null;

        /// <summary>
        /// SurfaceGrid rows.
        /// </summary>
        int _rows = 500;

        ///<summary>
        /// SurfaceGrid columns.
        ///</summary>
        int _columns = 500;

        /// <summary>
        /// Minimum X-axis value.
        /// </summary>
        private const int MinX = 0;

        /// <summary>
        /// Maximum X-axis value.
        /// </summary>
        private const int MaxX = 100;

        /// <summary>
        /// Minimum Z-axis value.
        /// </summary>
        private const int MinZ = 0;

        /// <summary>
        /// Maximum Z-axis value.
        /// </summary>
        private const int MaxZ = 100;

        public Form1()
        {
            InitializeComponent();

            // Create chart.
            CreateChart();

            // Generate data for chart.
            GenerateData(_columns, _rows);

            // Safe disposal of LightningChart components when the form is closed.
            FormClosed += new FormClosedEventHandler(Form_Closed);

            #region Hidden polishing
            // Customize chart.
            CustomizeChart(_chart);
            #endregion
        }

        // Create chart instance.
        public void CreateChart()
        {
            // Create chart.
            _chart = new LightningChartUltimate();

            // Set chart control into the parent container.
            _chart.Parent = this;
            _chart.Dock = DockStyle.Fill;

            // Disable rendering before updating chart properties to improve performance
            // and to prevent unnecessary chart redrawing while changing multiple properties.
            _chart.BeginUpdate();

            // 1. Set View3D as active view and set Y-axis range.
            _chart.ActiveView = ActiveView.View3D;
            _chart.View3D.YAxisPrimary3D.SetRange(-50, 100);

            // Set a LegendBox into a chart with vertical layout.
            _chart.View3D.LegendBox.Layout = LegendBoxLayout.Vertical;

            // 2. Create a new SurfaceGrid instance as SurfaceGridSeries3D.
            _surfaceGrid = new SurfaceGridSeries3D(_chart.View3D, Axis3DBinding.Primary, Axis3DBinding.Primary, Axis3DBinding.Primary);

            // 3. Set range, size and color saturation options for SurfaceGrid.
            _surfaceGrid.RangeMinX = MinX;
            _surfaceGrid.RangeMaxX = MaxX;
            _surfaceGrid.RangeMinZ = MinZ;
            _surfaceGrid.RangeMaxZ = MaxZ;
            _surfaceGrid.SizeX = _columns;
            _surfaceGrid.SizeZ = _rows;

            // Stronger colors.
            _surfaceGrid.ColorSaturation = 80;

            // 4. Create ValueRangePalette for coloring SurfaceGrid's data.
            ValueRangePalette palette = CreatePalette(_surfaceGrid);
            _surfaceGrid.ContourPalette = palette;

            // 5. Define WireFrameType and ContourLineType for SurfaceGrid.
            _surfaceGrid.WireframeType = SurfaceWireframeType3D.WireframePalettedByY;
            _surfaceGrid.ContourLineType = ContourLineType3D.ColorLineByY;
            _surfaceGrid.ContourLineWidth = 2;

            // Add SurfaceGridSeries to chart.
            _chart.View3D.SurfaceGridSeries3D.Add(_surfaceGrid);

            // Call EndUpdate to enable rendering again.
            _chart.EndUpdate();

        }

        // Create a ValueRangePalette to present SurfaceGrid's data.
        private ValueRangePalette CreatePalette(SurfaceGridSeries3D series)
        {
            // Creating palette for SurfaceGridSeries3D.
            var palette = new ValueRangePalette(series);

            // LightningChart has some preset values for palette steps.
            // Clear the preset values from palette before setting new ones.
            foreach (var step in palette.Steps)
            {
                step.Dispose();
            }
            palette.Steps.Clear();

            // Add steps into palette. 
            // Palette is used for presenting data in SurfaceGrid with different colors based on their value.
            palette.Steps.Add(new PaletteStep(palette, Color.Blue, -25));
            palette.Steps.Add(new PaletteStep(palette, Color.DodgerBlue, 0));
            palette.Steps.Add(new PaletteStep(palette, Color.LawnGreen, 25));
            palette.Steps.Add(new PaletteStep(palette, Color.Yellow, 50));
            palette.Steps.Add(new PaletteStep(palette, Color.Orange, 75));
            palette.Steps.Add(new PaletteStep(palette, Color.Red, 100));
            palette.Type = PaletteType.Gradient;
            palette.MinValue = -50;

            // Return the created palette.
            return palette;
        }

        // 6. Generate data.
        public void GenerateData(int columns, int rows)
        {
            // Create variable for storing data.
            double data = 0;

            // Disable rendering before updating chart properties to improve performance
            // and to prevent unnecessary chart redrawing while changing multiple properties.
            _chart.BeginUpdate();

            // Set data values and add them to SurfaceGrid.
            for (int i = 0; i < _columns; i++)
            {
                for (int j = 0; j < _rows; j++)
                {
                    // Add values to the SurfaceGrid as SurfacePoints, points are distributed by using following function.
                    data = 30.0 + 8 * Math.Cos(20 + 0.0001 * (double)(i * j)) + 60.0 * Math.Cos((double)(j - i) * 0.01);
                    _surfaceGrid.Data[i, j].Y = data;
                }
            }

            // Notify chart about updated data.
            _surfaceGrid.InvalidateData();

            // Call EndUpdate to enable rendering again.
            _chart.EndUpdate();
        }

        // Safe disposal of LightningChart components when the form is closed.
        private void Form_Closed(Object sender, FormClosedEventArgs e)
        {
            // Dispose Chart.
            _chart.Dispose();
            _chart = null;

            // Dispose SurfaceGrid.
            _surfaceGrid.Dispose();
            _surfaceGrid = null;
        }

        #region Hidden polishing
        void CustomizeChart(LightningChartUltimate chart)
        {
            chart.Background.Color = Color.FromArgb(255, 30, 30, 30);
            chart.Background.GradientFill = GradientFill.Solid;
            chart.Title.Color = Color.FromArgb(255, 249, 202, 3);
            chart.Title.MouseHighlight = MouseOverHighlight.None;
        }
        #endregion
    }
}
