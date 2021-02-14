using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
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
using 排课助手.Package;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace 排课助手.MyView
{
	public sealed partial class InfoTipTextBox : UserControl
	{
		public enum Mode { Edit,Normal}
		private Mode _boxMode { get; set; }
		public Mode BoxMode 
		{
			get 
			{
				return _boxMode;
			}
			set
			{
				_boxMode = value;
			} 
		}
		public List<Teacher> TeacherAdvice { get; set; }
		public List<SchoolClass> ClassAdvice { get; set; }
		public List<string> SubjectAdvice { get; set; }
		public CourseContainer CourseContainer { get; set; }
		public enum Type { Teacher,Class,Subject}
		public Type ContentType { get; set; }
		private string _text { set; get; }
		public string Text 
		{
			get
			{
				//string text = "";
				//switch (ContentType)
				//{
				//	case Type.Teacher:
				//		Teacher teacher = CourseContainer as Teacher;
				//		text = teacher.Name;
				//		break;
				//	case Type.Class:
				//		SchoolClass Class = CourseContainer as SchoolClass;
				//		text = Class.Name;
				//		break;

				//	case Type.Subject:
				//		text = _text;
				//		break;
				//	default:
				//		text = "";
				//		break;
				//}
				return _text;

			}
			set
			{
				//if(CourseContainer ==null)
				//	_text = value;
				//else
				//	switch (ContentType)
				//	{
				//		case Type.Teacher:
				//			Teacher teacher = CourseContainer as Teacher;
				//			_text = teacher.Name;
				//			break;
				//		case Type.Class:
				//			SchoolClass Class = CourseContainer as SchoolClass;
				//			_text = Class.Name;
				//			break;
				//		case Type.Subject:
				//			_text = value;
				//			break;
				//		default:
				//			_text = value;
				//			break;
				//	}
				_text = value;
			}
		}

		public InfoTipTextBox()
		{
			this.InitializeComponent();
			Loaded += InfoTipTextBox_Loaded;
		}

		private void InfoTipTextBox_Loaded(object sender, RoutedEventArgs e)
		{
			//if (BoxMode == Mode.Edit)
				//InitEdit();
			//else
				InitNormal();
		}

		public void InitEdit()
		{
			//grid.Children.Clear();
			//MenuFlyout menu = new MenuFlyout();

			//switch (ContentType)
			//{
			//	case Type.Teacher:
			//		foreach (Teacher item in TeacherAdvice)
			//		{
			//			MenuFlyoutItem flyoutItem = new MenuFlyoutItem();
			//			flyoutItem.SetBinding(MenuFlyoutItem.TextProperty, new Binding()
			//			{
			//				Source = item,
			//				Path = new PropertyPath("Name")
			//			});

			//			menu.Items.Add(flyoutItem);

			//		}
			//		break;
			//	case Type.Class:
					
			//			foreach (SchoolClass item in ClassAdvice)
			//			{
			//				MenuFlyoutItem flyoutItem = new MenuFlyoutItem();
			//				flyoutItem.SetBinding(MenuFlyoutItem.TextProperty, new Binding()
			//				{
			//					Source = item,
			//					Path = new PropertyPath("Name")
			//				});

			//				menu.Items.Add(flyoutItem);

			//			}					
			//		break;
			//	case Type.Subject:
			//		foreach (string item in SubjectAdvice)
			//		{
			//			MenuFlyoutItem flyoutItem = new MenuFlyoutItem();
			//			flyoutItem.SetBinding(MenuFlyoutItem.TextProperty, new Binding()
			//			{
			//				Source = item,
			//				Path = new PropertyPath("item")
			//			});

			//			menu.Items.Add(flyoutItem);

			//		}
			//		break;
			//	default:
			//		break;
			//}
			
			//DropDownButton downButton = new DropDownButton() { DataContext = this};
			//downButton.Flyout = menu;
			//grid.Children.Add(downButton);
		}

		public void InitNormal()
		{
			grid.Children.Clear();
			TextBlock text = new TextBlock();
			grid.Children.Add(text);

		}

		private void DropDownButton_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
