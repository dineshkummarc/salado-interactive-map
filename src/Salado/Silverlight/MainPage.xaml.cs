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

		void LayoutRoot_KeyDown(object sender, KeyEventArgs e)
		{
			int moveScale = 2;
			double zoomScale = 0.01;

			double currentX = Convert.ToInt32(Map.GetValue(Canvas.LeftProperty));
			if (e.Key == Key.Left) Map.SetValue(Canvas.LeftProperty, currentX + moveScale);
			if (e.Key == Key.Right) Map.SetValue(Canvas.LeftProperty, currentX - moveScale);

			double currentY = Convert.ToInt32(Map.GetValue(Canvas.TopProperty));
			if (e.Key == Key.Up) Map.SetValue(Canvas.TopProperty, currentY + moveScale);
			if (e.Key == Key.Down) Map.SetValue(Canvas.TopProperty, currentY - moveScale);

			if (e.Key == Key.Subtract) _currentScale -= zoomScale;
			if (e.Key == Key.Add) _currentScale += zoomScale;

			Map.Height = 627 * _currentScale;
			Map.Width = 707 * _currentScale;

			Coordinates.Text = string.Format("({0}, {1}) : {2}", currentX, currentY, _currentScale);
		}

		public void OnReadCompleted(object sender, OpenReadCompletedEventArgs e)
		{
			var serializer = new DataContractJsonSerializer(typeof(IEnumerable<Establishment>));
			var establishments = new List<Establishment>((IEnumerable<Establishment>)serializer.ReadObject(e.Result));
			Establishments.DataContext = new MapViewModel(establishments, new Point(30.95, -97.53), 10000);
		}
	}
}
