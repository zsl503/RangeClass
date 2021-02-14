using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using 排课助手.MyView;
using 排课助手.Package;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace 排课助手
{
	public sealed partial class CourseCell : UserControl
	{
		public enum Mode { None, Full,Text }
		private Mode _mode{get;set;}
		public Mode CellMode
		{
			get
			{
				return _mode;
			}
			set
			{
				//if(IsLoaded)
				//	switch (value)
				//	{
				//		case Mode.Full: FullCell();break;
				//		case Mode.None: NoneCell();break;
				//		case Mode.Text: 
				//			if(Text!=null)
				//				TextCell();
				//			break;
				//	}
				_mode = value;
			}
		}

		private void EditCell()
		{

			Cell.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
			TextPanel.Children.Clear();

			InfoTipTextBox ClassText = new InfoTipTextBox()
			{
				CourseContainer = Course.Class,
				ContentType = InfoTipTextBox.Type.Class,
				FontSize = this.FontSize,
				//ClassAdvice= School.GetAdvice(Course.Teacher,""),
				BoxMode = InfoTipTextBox.Mode.Normal
			};

			//InfoTipTextBox TeacherText = new InfoTipTextBox()
			//{
			//	CourseContainer = Course.Teacher,
			//	ContentType = InfoTipTextBox.Type.Teacher,
			//	FontSize = this.FontSize,
			//	TeacherAdvice = School.GetAdvice(Course.Class, ""),
			//	BoxMode = InfoTipTextBox.Mode.Edit

			//};

			//InfoTipTextBox SubjectText = new InfoTipTextBox()
			//{
			//	ContentType = InfoTipTextBox.Type.Subject,
			//	FontSize = this.FontSize,
			//	SubjectAdvice = School.GetAdvice(Course.Teacher),
			//	BoxMode = InfoTipTextBox.Mode.Edit,
			//	Text = Course.Subject
			//};
			TextPanel.Children.Add(ClassText);
			//TextPanel.Children.Add(TeacherText);
			//TextPanel.Children.Add(SubjectText);
		}

		private void TextCell() 
		{
			Cell.Background = new SolidColorBrush(Windows.UI.Colors.AliceBlue);
			TextPanel.Children.Clear();
			TextBlock Text = new TextBlock() { Text = this.Text,FontSize = this.FontSize };
			TextPanel.Children.Add(Text);
		}

		private void FullCell()
		{
			Cell.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);

			TextPanel.Children.Clear();
			InfoTipTextBox ClassText = new InfoTipTextBox()
			{
				CourseContainer = Course.Class,
				ContentType = InfoTipTextBox.Type.Class,
				FontSize = this.FontSize,
				BoxMode = InfoTipTextBox.Mode.Normal
			}; 

			InfoTipTextBox TeacherText = new InfoTipTextBox()
			{
				CourseContainer = Course.Teacher,
				ContentType = InfoTipTextBox.Type.Teacher,
				FontSize = this.FontSize,
				BoxMode = InfoTipTextBox.Mode.Normal

			};


			InfoTipTextBox SubjectText = new InfoTipTextBox()
			{
				ContentType = InfoTipTextBox.Type.Subject,
				FontSize = this.FontSize,
				Text = Course.Subject,
				BoxMode = InfoTipTextBox.Mode.Normal

			};

			TextPanel.Children.Add(ClassText);
			TextPanel.Children.Add(TeacherText);
			TextPanel.Children.Add(SubjectText);

		}

		private void NoneCell()
		{
			Cell.Background = new SolidColorBrush(Windows.UI.Colors.LightPink);
			TextPanel.Children.Clear();
			TextBlock Text = new TextBlock() { Text="空",FontSize = this.FontSize };
			TextPanel.Children.Add(Text);
		}

		public Course Course { get; set; }

		private string _test { get; set; }
		public string Text 
		{
			get 
			{
				return _test; 
			} 
			set 
			{
				_test = value;

				CellMode = Mode.Text;
			}
		}
		public CourseCell()
		{
			this.InitializeComponent();
			Loaded += CourseCell_Loaded;
		}

		private void CourseCell_Loaded(object sender, RoutedEventArgs e)
		{
			switch (CellMode)
			{
				case Mode.Full: FullCell(); EditCell(); break;//这里没问题
				case Mode.None: NoneCell(); break;
				case Mode.Text:
					if (Text != null)
						TextCell();
					break;
			}
		}

		private void Cell_Tapped(object sender, TappedRoutedEventArgs e)
		{
			//Cell.IsTapEnabled = false;
			 
			EditCell();//这里报错
		}
	}
}
