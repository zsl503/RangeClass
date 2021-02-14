using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using 排课助手.Package;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace 排课助手
{
	/// <summary>
	/// 可用于自身或导航至 Frame 内部的空白页。
	/// </summary>
	/// 



	public sealed partial class CourseTemplate : Page
	{
		
		public void SetMoningCouse(Boolean isAdd, int setIndex)
		{
			if (isAdd)
			{
				if (TableTemplate.MoningText.Count <= setIndex)
					TableTemplate.MoningText.Add(new MoningText() { Text= "第一节课" });

				RowDefinition row = new RowDefinition();
				TableMoning.RowDefinitions.Add(row);
				TextBox textBox = new TextBox() ;

				textBox.SetBinding(TextBox.TextProperty, new Binding()
				{
					Source = TableTemplate.MoningText[setIndex],
					Path = new PropertyPath("Text"),
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

		public void SetAfternoonCouse(Boolean isAdd, int setIndex)
		{
			if (isAdd)
			{
				if (TableTemplate.AfternoonText.Count <= setIndex)
					TableTemplate.AfternoonText.Add(new AfternoonText() { Text = "第一节课" });


				RowDefinition row = new RowDefinition();
				TableAfternoon.RowDefinitions.Add(row);
				TextBox textBox = new TextBox();
				
				textBox.SetValue(Grid.RowProperty, setIndex);
				textBox.SetValue(Grid.ColumnProperty, 0);
				textBox.SetBinding(TextBox.TextProperty, new Binding()
				{
					Source = TableTemplate.AfternoonText[setIndex],
					Path = new PropertyPath("Text"),
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

		private void SetHeader(int setIndex,string text)
		{
			if (TableTemplate.HeaderText.Count <= setIndex)
				TableTemplate.HeaderText.Add(new HeaderText() { Text = text });

			TextBox textBox = new TextBox();

			textBox.SetValue(Grid.ColumnProperty, setIndex);
			textBox.SetBinding(TextBox.TextProperty, new Binding()
			{
				Source = TableTemplate.HeaderText[setIndex],
				Path = new PropertyPath("Text"),
				Mode = BindingMode.TwoWay
			});


			headerBox.Children.Add(textBox);
		}

		private void ReInitTable()
		{
			TableTemplate.Clear();
			InitTable();
		}

		private void InitTable()
		{
			moningCourse.Value = TableTemplate.MoningText.Count;
			afternoonCourse.Value = TableTemplate.AfternoonText.Count;

			for (int i = 0; i < 4; i++)
			{
				SetMoningCouse(true, i);
			}
			for (int i = 0; i < 3; i++)
			{
				SetAfternoonCouse(true, i);
			}
			SetHeader(0, "");
			SetHeader(1, "星期一");
			SetHeader(2, "星期二");
			SetHeader(3, "星期三");
			SetHeader(4, "星期四");
			SetHeader(5, "星期五");

			TextBox textBox = new TextBox();
			TableTemplate temp = new TableTemplate();
			textBox.SetBinding(TextBox.TextProperty, new Binding()
			{
				Source = temp,
				Path = new PropertyPath("restText"),
				Mode = BindingMode.TwoWay
			});

			noonBox.Children.Add(textBox);
		}

		public CourseTemplate()
		{
			this.InitializeComponent();
			InitTable();
		}

		private void MoningCourse_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
		{
			if (TableMoning == null)
				return;
			if (e.NewValue > e.OldValue)
				for (int i = (int)e.OldValue; i < e.NewValue; i++)
					SetMoningCouse(true, i);
			else
				for (int i = (int)e.OldValue - 1; i >= e.NewValue; i--)
					SetMoningCouse(false, i);
		}

		private void AfternoonCourse_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
		{
			if (TableMoning == null)
				return;
			if (e.NewValue > e.OldValue)
				for (int i = (int)e.OldValue; i < e.NewValue; i++)
					SetAfternoonCouse(true, i);
			else
				for (int i = (int)e.OldValue - 1; i >= e.NewValue; i--)
					SetAfternoonCouse(false, i);
		}

		private void Button_Tapped(object sender, TappedRoutedEventArgs e)
		{
			if (Frame.CanGoBack)
			{
				Frame.GoBack();
			}
		}
	}
}
