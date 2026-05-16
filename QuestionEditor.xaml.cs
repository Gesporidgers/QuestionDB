using QuestionDB.Model;
using QuestionDB.ViewModels;

namespace QuestionDB;

public partial class QuestionEditor : ContentPage
{
	EditorVM vm;
	public QuestionEditor(ref Question question)
	{
		vm = new EditorVM(ref question);
		BindingContext = vm;
		InitializeComponent();
	}
}