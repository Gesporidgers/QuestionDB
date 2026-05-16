using System.Diagnostics;

namespace QuestionDB
{
	public partial class MainPage : ContentPage
	{
		MainVM vm;
		public MainPage()
		{
          vm = new MainVM();
			InitializeComponent();
			BindingContext = vm;
		}

	}
}
