using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace 排课助手
{
	/// <summary>
	/// 可用于自身或导航至 Frame 内部的空白页。
	/// </summary>
	/// 
	public class HeaderText: INotifyPropertyChanged
	{
		string _headerText;
		public string headerText
		{
			get
			{
				return _headerText;
			}
			set
			{
				_headerText = value; OnPropertyChanged("headerText"); 
			}
		}

		string _afternoonText;
		string afternoonText
		{
			get
			{
				return _headerText;
			}
			set
			{
				_headerText = value; OnPropertyChanged("headerText");
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

		string _moningText;
		public string moningText
		{
			get
			{
				return _moningText;
			}
			set
			{
				_moningText = value; OnPropertyChanged("moningText");
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
		string _afternoonText;
		public string afternoonText
		{
			get
			{
				return _afternoonText;
			}
			set
			{
				_afternoonText = value; OnPropertyChanged("afternoonText");
			}
		}

		protected internal virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler PropertyChanged;
	}

	public sealed partial class CourseTable : Page
	{
		public string[] headerText = new string[6];
		public ObservableCollection<MoningText> Moning = new ObservableCollection<MoningText>();
		public ObservableCollection<AfternoonText> Afternoon = new ObservableCollection<AfternoonText>();
		public string restText = "午休";
		public int teacherMode, studentMode;
		public void setMoningCouse(Boolean isAdd, int setIndex)
		{
			if (isAdd)
			{
				Moning.Add(new MoningText() { moningText= "第一节课" });

				RowDefinition row = new RowDefinition();
				TableMoning.RowDefinitions.Add(row);
				TextBox textBox = new TextBox() ;

				textBox.SetBinding(TextBox.TextProperty, new Binding()
				{
					Source = Moning[setIndex],
					Path = new PropertyPath("moningText"),
					Mode = BindingMode.TwoWay
				});

				textBox.SetValue(Grid.ColumnProperty, 0);
				textBox.SetValue(Grid.RowProperty, setIndex);
				TableMoning.Children.Add(textBox);
			}
			else
			{
				TableMoning.RowDefinitions.RemoveAt(0);
				TableMoning.Children.RemoveAt(setIndex);

			}
		}

		public void setAfternoonCouse(Boolean isAdd, int setIndex)
		{
			if (isAdd)
			{

				Afternoon.Add(new AfternoonText() { afternoonText = "第一节课" });


				RowDefinition row = new RowDefinition();
				TableAfternoon.RowDefinitions.Add(row);
				TextBox textBox = new TextBox();
				
				textBox.SetValue(Grid.RowProperty, setIndex);
				textBox.SetValue(Grid.ColumnProperty, 0);
				textBox.SetBinding(TextBox.TextProperty, new Binding()
				{
					Source = Afternoon[setIndex],
					Path = new PropertyPath("afternoonText"),
					Mode = BindingMode.TwoWay
				});


				TableAfternoon.Children.Add(textBox);
			}
			else
			{
				TableMoning.RowDefinitions.RemoveAt(0);
				TableAfternoon.Children.RemoveAt(setIndex);

			}
		}


		private void initTable()
		{
			for (int i = 0; i < 4; i++)
			{
				setMoningCouse(true, i);
			}
			for (int i = 0; i < 3; i++)
			{
				setAfternoonCouse(true, i);
			}
			headerText[0]="";
			headerText[1]=("星期一");
			headerText[2]=("星期二");
			headerText[3]=("星期三");
			headerText[4]=("星期四");
			headerText[5]=("星期五");


		}

		public CourseTable()
		{
			this.InitializeComponent();
			initTable();

		}

		private void moningCourse_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
		{
			if (TableMoning == null)
				return;
			if (e.NewValue > e.OldValue)
				for (int i = (int)e.OldValue; i < e.NewValue; i++)
					setMoningCouse(true, i);
			else
				for (int i = (int)e.OldValue - 1; i >= e.NewValue; i--)
					setMoningCouse(false, i);
		}

		private void afternoonCourse_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
		{
			if (TableMoning == null)
				return;
			if (e.NewValue > e.OldValue)
				for (int i = (int)e.OldValue; i < e.NewValue; i++)
					setAfternoonCouse(true, i);
			else
				for (int i = (int)e.OldValue - 1; i >= e.NewValue; i--)
					setAfternoonCouse(false, i);
		}

		private void Button_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Console.WriteLine();
			List<string> moningText = new List<string>();
			foreach (MoningText i in Moning){
				moningText.Add(i.moningText);
			}

			List<string> afternoonText = new List<string>();
			foreach (AfternoonText i in Afternoon)
			{
				afternoonText.Add(i.afternoonText);
			}
			TableTamplate.initTableTamplate(
				headerText,
				moningText.ToArray(), afternoonText.ToArray(),
				restText, teacherMode, studentMode);
		}
	}
}
