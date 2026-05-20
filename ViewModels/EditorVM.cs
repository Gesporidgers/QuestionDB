using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuestionDB.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionDB.ViewModels
{
	public partial class EditorVM : ObservableObject
	{
		private Question question;
		
		
		[ObservableProperty]
		private string questionText;

		private QuestionTypeClass questionType;
		public QuestionTypeClass QuestionType
		{
			get => questionType;
			set
			{
				SetProperty(ref questionType, value);
				ListVisibility = value.Value == 0 ? Visibility.Visible : Visibility.Collapsed;
			}
		}

		[ObservableProperty]
		private ObservableCollection<AnswerOption> answers;

		[ObservableProperty]
		private string correctAnswer;
		
		[ObservableProperty]
		private string explanation;

		[ObservableProperty]
		private Visibility listVisibility = Visibility.Collapsed;

		private readonly List<QuestionTypeClass> questionTypes = QuestionTypeClass.GetTypes();
		public List<QuestionTypeClass> QuestionTypes => questionTypes;

		[ObservableProperty]
		private AnswerOption selectedAnswer;

		

		
		
		public EditorVM(ref Question question)
		{
			QuestionText = question.Text;
			QuestionType = QuestionTypes[question.QuestionType];
			Answers = new ObservableCollection<AnswerOption>(question.Answers.Select(a => new AnswerOption { Text = a }).ToList());
			CorrectAnswer = question.CorrectAnswer;
			Explanation= question.Explanation;
			this.question = question;
		}

		[RelayCommand(CanExecute = nameof(CanRemoveAnswerOption))]
		private void RemoveAnswerOption()
		{
			Answers.Remove(Answers.Last());
			RemoveAnswerOptionCommand.NotifyCanExecuteChanged();
		}

		private bool CanRemoveAnswerOption()
		{
			return Answers.Count > 0;
		}

		[RelayCommand]
		private void AddAnswerOption()
		{
			Answers.Add(new AnswerOption { Text = string.Empty });
			RemoveAnswerOptionCommand.NotifyCanExecuteChanged();
		}

		[RelayCommand]
		private void SaveQuestion()
		{
			question.Text = QuestionText;
			question.CorrectAnswer = CorrectAnswer;
			question.Answers = Answers.Select(a => a.Text).ToList();
			question.QuestionType = QuestionType.Value;
			question.Explanation = Explanation;
			App.Current.MainPage.Navigation.PopAsync();
		}

	}

	public partial class AnswerOption : ObservableObject
	{
		[ObservableProperty]
		private string text;
	}

	public class QuestionTypeClass
	{
		public string Name { get; set; }
		public int Value { get; set; }
		public static List<QuestionTypeClass> GetTypes()
		{
			return new List<QuestionTypeClass>
			{
				new QuestionTypeClass { Name = "Множственный выбор", Value = 0 },
				new QuestionTypeClass { Name = "Одиночный выбор", Value = 1 }
			};
		}
	}
	
}
