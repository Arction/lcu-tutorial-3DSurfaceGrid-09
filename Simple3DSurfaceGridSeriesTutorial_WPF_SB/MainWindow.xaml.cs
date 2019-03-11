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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

// Arction namespaces.
using Arction.Wpf.SemibindableCharting;              // LightningChartUltimate and general types.
using Arction.Wpf.SemibindableCharting.Views.View3D; // View for 3D chart.

namespace Simple3DSurfaceGridSeriesTutorial_WPF_SB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// SurfaceGrid rows.
        /// </summary>
        int _rows = 500;

        ///<summary>
        /// SurfaceGrid columns.
        ///</summary>
        int _columns = 500;

        // SurfaceGrids minimum and maximum values for X- and Z-axis
        // are defined using XAML.

        public MainWindow()
        {
            InitializeComponent();

            // Create chart and set default lights for 3D view.
            CreateChart();
            _chart.View3D.Lights = View3D.CreateDefaultLights();

            // Generate data for chart.
            GenerateData(_columns, _rows);

            // Safe disposal of LightningChart components when the window is closed.
            Closed += new EventHandler(Window_Closed);

            #region Hidden polishing
            // Customize chart.
            CustomizeChart(_chart);
            #endregion
        }

        // Create chart instance.
        public void CreateChart()
        {
            // Create chart.
            // This is done using XAML.

            // Set chart control into the parent container.
            (Content as Grid).Children[0] = _chart;

            // Disable rendering before chart updates.
            _chart.BeginUpdate();

            // 1. Set View3D as active view and set Y-axis range.
            // This is done using XAML.

            // Set a LegendBox into a chart with vertical layout.
            // This is done using XAML.

            // 2. Create a new SurfaceGrid instance as SurfaceGridSeries3D.
            // This is done using XAML.

            // 3. Set range, size and color saturation options for SurfaceGrid.
            // This is done using XAML.

            // 4. Create ValueRangePalette for coloring SurfaceGrid's data.
            // This is done using XAML.

            // 5. Define WireFrameType and ContourLineType for SurfaceGrid.
            // This is done using XAML.

            // Add SurfaceGridSeries to chart.
            // This is done using XAML.

            // Allow chart rendering.
            _chart.EndUpdate();

        }

        // Create a ValueRangePalette to present SurfaceGrid's data.
        // This is done using XAML.

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

        // Safe disposal of LightningChart components when the window is closed.
        private void Window_Closed(Object sender, EventArgs e)
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
            chart.ChartBackground.Color = Color.FromArgb(255, 30, 30, 30);
            chart.ChartBackground.GradientFill = GradientFill.Solid;
            chart.Title.Color = Color.FromArgb(255, 249, 202, 3);
            chart.Title.MouseHighlight = MouseOverHighlight.None;
        }
        #endregion
    }
}
