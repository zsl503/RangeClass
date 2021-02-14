using Microsoft.Toolkit.Uwp.UI.Controls;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
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
using 排课助手.Package;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace 排课助手
{

	public sealed partial class CourseTable : UserControl
	{

		private CourseContainer _courseContainer { get; set; } = new CourseContainer();

		public CourseContainer CourseContainer
		{
			get
			{
				return _courseContainer;
			}
			set
			{
				_courseContainer = value;
				InitTable();
			}
		}

		public TableTemplate tableTemplate { get; set; } = new TableTemplate();

		double HeaderFontSize
		{
			get
			{
				return FontSize + 2;
			}
		}

		public string Header { get; set; }
		
		CourseCell[,] moningCourseCells;
		
		public enum Mode { Teacher, Class };
		
		public Mode TableMode { get; set; } = Mode.Class;
		
		CourseCell[,] afternoonCourseCells;

		public void InitTable()
		{
			
			if (CourseContainer == null)
				return;
			moningCourseCells = new CourseCell[5, TableTemplate.MoningText.Count];
			afternoonCourseCells = new CourseCell[5, TableTemplate.AfternoonText.Count];

			int moningBeginIndex = 1, moningEndIndex = TableTemplate.MoningText.Count;
			int afternoonBeginIndex = TableTemplate.MoningText.Count + 2, afternoonEndIndex = TableTemplate.MoningText.Count + TableTemplate.AfternoonText.Count + 1;

			Grid.SetRow(noon, moningEndIndex + 1);

			for (int i = afternoonEndIndex ; i>=0; i--)
				if(i!= moningEndIndex + 1)
					TableData.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
				else
					TableData.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star), MaxHeight = 80});

			foreach (Course item in CourseContainer.GetCourses)
			{
				CourseCell course = new CourseCell() { FontSize = this.FontSize, Course = item, CellMode = CourseCell.Mode.Full };
				course.SetValue(Grid.RowProperty, item.TimeIndex - 1);
				course.SetValue(Grid.ColumnProperty, (int)item.Week);
				if (item.TimeType == TimeType.上午)
					moningCourseCells[(int)item.Week - 1, item.TimeIndex - 1] = course;
				else
					afternoonCourseCells[(int)item.Week - 1, item.TimeIndex - 1] = course;
			}

			for (int i = moningEndIndex; i >= moningBeginIndex; i--)
			{
				for (int j = 5 - 1; j >= 0; j--)
				{
					if (moningCourseCells[j, i - moningBeginIndex] == null)
					{
						CourseCell course = new CourseCell() { FontSize = this.FontSize, CellMode = CourseCell.Mode.None };
						moningCourseCells[j, i - moningBeginIndex] = course;
					}
					moningCourseCells[j, i - moningBeginIndex].SetValue(Grid.RowProperty, i);
					moningCourseCells[j, i - moningBeginIndex].SetValue(Grid.ColumnProperty, j + 1);
					TableData.Children.Add(moningCourseCells[j, i - moningBeginIndex]);

				}
				CourseCell item = new CourseCell() { FontSize = HeaderFontSize, Text = TableTemplate.MoningText[i - moningBeginIndex].Text };
				Grid.SetRow(item, i);
				Grid.SetColumn(item, 0);
				TableData.Children.Add(item);
			}

			for (int i = afternoonEndIndex; i >= afternoonBeginIndex; i--)
			{
				for (int j = 5 - 1; j >= 0; j--)
				{
					if (afternoonCourseCells[j, i - afternoonBeginIndex] == null)
					{
						CourseCell course = new CourseCell() { FontSize = this.FontSize, CellMode = CourseCell.Mode.None };
						afternoonCourseCells[j, i - afternoonBeginIndex] = course;
					}
					afternoonCourseCells[j, i - afternoonBeginIndex].SetValue(Grid.RowProperty, i);
					afternoonCourseCells[j, i - afternoonBeginIndex].SetValue(Grid.ColumnProperty, j + 1);
					TableData.Children.Add(afternoonCourseCells[j, i - afternoonBeginIndex]);

				}
				CourseCell item = new CourseCell() { FontSize = HeaderFontSize, Text = TableTemplate.AfternoonText[i-afternoonBeginIndex].Text };
				Grid.SetRow(item, i);
				Grid.SetColumn(item, 0);
				TableData.Children.Add(item);
			}

			for (int i = 0; i < TableTemplate.HeaderText.Count; i++)
			{
				CourseCell item = new CourseCell() {  FontSize = HeaderFontSize, CellMode = CourseCell.Mode.Text, Text = TableTemplate.HeaderText[i].Text };
				Grid.SetColumn(item, i);
				Grid.SetRow(item, 0);
				TableData.Children.Add(item);
			}
		}

		public CourseTable()
		{
			this.InitializeComponent();
		}
	}
}
