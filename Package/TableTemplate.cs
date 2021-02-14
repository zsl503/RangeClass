using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 排课助手
{
	public class HeaderText : INotifyPropertyChanged
	{
		string _text;
		public string Text
		{
			get => _text;
			set
			{
				_text = value; OnPropertyChanged("Text");
			}
		}

		protected internal virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler PropertyChanged;
	}

	public class MoningText : INotifyPropertyChanged
	{

		string _text;
		public string Text
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value; OnPropertyChanged("Text");
			}
		}

		protected internal virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler PropertyChanged;
	}

	public class AfternoonText : INotifyPropertyChanged
	{
		string _text;
		public string Text
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value; OnPropertyChanged("Text");
			}
		}

		protected internal virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler PropertyChanged;
	}

	public class TableTemplate
	{
		static public ObservableCollection<HeaderText> HeaderText { get; set; } = new ObservableCollection<HeaderText>();
		static public ObservableCollection<MoningText> MoningText { get; set; } = new ObservableCollection<MoningText>();
		static public ObservableCollection<AfternoonText> AfternoonText { get; set; } = new ObservableCollection<AfternoonText>();
		public static string RestText { get; set; } = "午休";
		public string restText { set { RestText = restText; } get { return RestText; } }
		static public int teacherMode;
		static public int studentMode;
		static public bool Clear()
		{
			HeaderText.Clear();
			MoningText.Clear();
			AfternoonText.Clear();
			RestText = "午休";
			teacherMode = 0;
			studentMode = 0;
			return true;
		}
		//static public void initTableTamplate(string[] headerText,
		//	string[] moningText, string[] afternoonText,
		//	string restText="午休",int teacherMode=0,int studentMode=0)
		//{
		//	TableTemplate.headerText = headerText;
		//	TableTemplate.moningText = moningText;
		//	TableTemplate.afternoonText = afternoonText;
		//	TableTemplate.restText = restText;
		//	TableTemplate.teacherMode = teacherMode;
		//	TableTemplate.studentMode = studentMode;
		//}
	}
}
