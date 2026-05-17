using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace QuestionDB.Model
{
	public partial class Question : ObservableObject
	{

		private string text;

		[JsonPropertyName("text")]
		public string Text
		{
			get => text;
			set => SetProperty(ref text, value);
		}


		private string correctAnswer;
		[JsonPropertyName("correct_answer")]
		public string CorrectAnswer
		{
			get => correctAnswer;
			set => SetProperty(ref correctAnswer, value);
		}


		private int questionType;
		[JsonPropertyName("question_type")]
		public int QuestionType
		{
			get => questionType;
			set => SetProperty(ref questionType, value);
		}


		private string note = string.Empty;
		[JsonPropertyName("note")]
		public string Note
		{
			get => note;
			set => SetProperty(ref note, value);
		}


		private List<string> answers = new List<string>();
		[JsonPropertyName("answers")]
		public List<string> Answers
		{
			get => answers;
			set => SetProperty(ref answers, value);
		}

		[JsonIgnore]
		private static JsonSerializerOptions options = new JsonSerializerOptions
		{
			Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
			WriteIndented = true,
		};

		public static List<Question> FromJSON(string json) => JsonSerializer.Deserialize<List<Question>>(json);

		public static string ToJSON(List<Question> serializible)
		{
			
			return JsonSerializer.Serialize(serializible, options);
		}


		public Question()
		{
			text = "Новый вопрос";
		}
		
	}

}
