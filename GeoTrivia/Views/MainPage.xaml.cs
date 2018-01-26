﻿using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI.Controls;
using Windows.UI.Xaml.Controls;

namespace GeoTrivia
{
    /// <summary>
    /// A map page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();
            ViewModel.GraphicsOverlay = SceneView.GraphicsOverlays;
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
		}

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SceneViewModel.IsSubmitted))
            {
                var viewpoint = SceneView.GetCurrentViewpoint(ViewpointType.CenterAndScale);
                ViewModel.UserAnswer = viewpoint.TargetGeometry as MapPoint;

                var geom = ViewModel.CurrentQuestion.Geometry;
                var bufferedGeom = GeometryEngine.Buffer(geom, geom.Extent.Width);
                SceneView.SetViewpointAsync(new Viewpoint(bufferedGeom), new System.TimeSpan(0, 0, 3));
            }
        }

        /// <summary>
        /// Gets the view-model that provides mapping capabilities to the view
        /// </summary>
        public SceneViewModel ViewModel { get; } = new SceneViewModel();
    }
}
