using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Maui.Alerts;
using QuestionDB.Model;
using Microsoft.Maui.Storage;
using System.Text.Json;

namespace QuestionDB
{
	public partial class MainVM : ObservableObject
	{
		[ObservableProperty]
		private ObservableCollection<Question> questions;

		[ObservableProperty]
		private Question selectedQuestion;


		public MainVM()
		{
			questions = new ObservableCollection<Question>();
		}

		[RelayCommand]
		public void AddQuestion()
		{
			questions.Add(new Question());
		}

		[RelayCommand]
		private void OpenEditor(Question question)
		{
			var editor = new QuestionEditor(ref question);
			App.Current.MainPage.Navigation.PushAsync(editor);
		}

		[RelayCommand]
		public async void ImportDB()
		{
			string content = await PickFile();
			List<Question> importedQuestions;
			if (content != string.Empty)
			{
				try
				{
					importedQuestions = Question.FromJSON(content);
					Questions = new ObservableCollection<Question>(importedQuestions);
				}
				catch (Exception ex)
				{
					await App.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка при импорте:\n{ex.Message}", "OK");
				}
			}

		}

		[RelayCommand]
		public async void ExportDB()
		{
			string json = Question.ToJSON(Questions.ToList());
			if (await Utility.CheckPermissions())
				await SaveFile(json);
			else
			{
				await CommunityToolkit.Maui.Alerts.Toast.Make("У программы отсутствуют разрешения\nв хранилище").Show();
			}
		}

		private async Task<string> PickFile()
		{
			var result = await FilePicker.PickAsync(new PickOptions
			{
				FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
				{
					{ DevicePlatform.WinUI, new[] { ".json" } },
					{ DevicePlatform.Android, new[] { "application/json" } },
					{ DevicePlatform.iOS, new[] { "public.json" } },
					{ DevicePlatform.MacCatalyst, new[] { "public.json" } }
				})
			});
			string fileContent = string.Empty;
			if (result != null)
			{
				using var stream = await result.OpenReadAsync();
				using (StreamReader reader = new StreamReader(stream))
					fileContent = await reader.ReadToEndAsync();
			}
			return fileContent;
		}

		private async Task SaveFile(string content)
		{
			using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
			var fileSaverResult = await FileSaver.Default.SaveAsync("questions_db.json", stream);
			if (fileSaverResult.IsSuccessful)
				await App.Current.MainPage.DisplayAlert("Успешно", "Файл сохранён", "OK");
			else
				await App.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка при сохранении:\n{fileSaverResult.Exception.Message}", "OK");
		}
	}
}
