using System.Windows;
using System.Windows.Controls;

namespace Silverlight
{
	public partial class App
	{
		public App()
		{
			Startup += delegate { RootVisual = new MainPage(); };
			UnhandledException += Application_UnhandledException;

			InitializeComponent();
		}

		private static void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
		{
			if (System.Diagnostics.Debugger.IsAttached) return;
			
			e.Handled = true;
			ChildWindow errorWin = new ErrorWindow(e.ExceptionObject);
			errorWin.Show();
		}
	}
}