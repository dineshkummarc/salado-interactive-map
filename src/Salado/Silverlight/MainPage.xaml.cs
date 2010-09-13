using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Input;
using Domain;
using Silverlight.ViewModels;

namespace Silverlight
{
	public partial class MainPage
	{
		private double _currentScale = 1.0;

		public MainPage()
		{
			InitializeComponent();

			LayoutRoot.KeyDown += LayoutRoot_KeyDown;

			var proxy = new WebClient();
			proxy.OpenReadCompleted += OnReadCompleted;
			proxy.OpenReadAsync(new Uri(HtmlPage.Document.DocumentUri + "/Map/Shops"));
		}

		private void LayoutRoot_KeyDown(object sender, KeyEventArgs e)
		{
			const int moveScale = 2;
			const double zoomScale = 0.01;

			double currentX = Convert.ToInt32(MapRoot.GetValue(Canvas.LeftProperty));
			if (e.Key == Key.Left) MapRoot.SetValue(Canvas.LeftProperty, currentX - moveScale);
			if (e.Key == Key.Right) MapRoot.SetValue(Canvas.LeftProperty, currentX + moveScale);

			double currentY = Convert.ToInt32(MapRoot.GetValue(Canvas.TopProperty));
			if (e.Key == Key.Up) MapRoot.SetValue(Canvas.TopProperty, currentY - moveScale);
			if (e.Key == Key.Down) MapRoot.SetValue(Canvas.TopProperty, currentY + moveScale);

			if (e.Key == Key.Subtract) _currentScale -= zoomScale;
			if (e.Key == Key.Add) _currentScale += zoomScale;

			Map.Height = 627 * _currentScale;
			Map.Width = 707 * _currentScale;

			Coordinates.Text = string.Format("({0}, {1}) : {2}", currentX, currentY, _currentScale);
		}

		private void OnReadCompleted(object sender, OpenReadCompletedEventArgs e)
		{
			var serializer = new DataContractJsonSerializer(typeof(IEnumerable<Establishment>));
			var establishments = (IEnumerable<Establishment>)serializer.ReadObject(e.Result);

			var mapMinimum = new Point(30.978317, -97.497348);
			var mapMaximum = new Point(30.909870, -97.557070);

			Establishments.DataContext = new MapViewModel(new Point(929, 695), establishments, mapMinimum, mapMaximum, 10000);
		}
	}
}
