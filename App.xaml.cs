

namespace QuestionDB
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
		}

		protected override Window CreateWindow(IActivationState? activationState)
		{
			var w = new Window(new AppShell());
#if WINDOWS
			w.Height = 900;
#elif ANDROID
			w.Height = 1000;
#endif
			return w;
		}
	}
}