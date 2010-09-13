using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Input;
using Domain;
using Silverlight.ViewModels;

namespace Silverlight
{
	public partial class MainPage
	{
		private bool _mouseCaptured;
		private Point _mouseClickPosition;

		public MainPage()
		{
			InitializeComponent();

			MapRoot.MouseLeftButtonDown += MapRoot_MouseLeftButtonDown;
			MapRoot.MouseLeftButtonUp += MapRoot_MouseLeftButtonUp;
			MapRoot.MouseMove += MapRoot_MouseMove;

			var proxy = new WebClient();
			proxy.OpenReadCompleted += OnReadCompleted;
			proxy.OpenReadAsync(new Uri(HtmlPage.Document.DocumentUri + "/Map/Shops"));
		}

		private void MapRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			MapRoot.CaptureMouse();
			_mouseClickPosition = e.GetPosition(sender as UIElement);
			_mouseCaptured = true;
		}

		private void MapRoot_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			MapRoot.ReleaseMouseCapture();
			_mouseCaptured = false;
		}

		private void MapRoot_MouseMove(object sender, MouseEventArgs e)
		{
			if (!_mouseCaptured) return;

			MapRootTransform.X = e.GetPosition(this).X - _mouseClickPosition.X;
			MapRootTransform.Y = e.GetPosition(this).Y - _mouseClickPosition.Y;
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
