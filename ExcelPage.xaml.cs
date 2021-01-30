using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace 排课助手
{
	/// <summary>
	/// 可用于自身或导航至 Frame 内部的空白页。
	/// </summary>
	/// 

	public sealed partial class ExcelPage : Page
	{
		//private List<ObservableCollection<ExcelCell>> ExcelCellList { get; set; } = new List<ObservableCollection<ExcelCell>>();

		public ObservableCollection<Row> ExcelColList  = new ObservableCollection<Row>();

		private void initTable(ExcelCell[,] cells)
		{
			ExcelGrid.ItemsSource = ExcelColList;
			for (int i = 0; i < cells.GetLength(1); i++)
			{
				Row cell = new Row() {
					rowNumber = i+1
				};

				cell.rowCell = new ExcelCell[cells.GetLength(0)];

				for (int j = 0; j < cells.GetLength(0); j++)
				{
					try
					{
						cell.rowCell[j] = cells[j,i];
					}
					catch (IndexOutOfRangeException)
					{
						cell.rowCell[j] = new ExcelCell();
					}
				}
				this.ExcelColList.Add(cell);

			}

			for (int i = 0; i < cells.GetLength(1); i++)
			{
				DataGridTextColumn columnText = new DataGridTextColumn()
				{
					Header = (char)('A' + i),
					Width = new DataGridLength(80),
					Binding = new Binding()
					{
						Path = new PropertyPath("rowCell["+i+"].text")
					}
				};
				ExcelGrid.Columns.Add(columnText);
			}

		}

		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedFrom(e);
			
			initTable((ExcelCell[,])e.Parameter);
		}

		public ExcelPage()
		{
			this.InitializeComponent();
			ExcelGrid.SelectedItems
		}

		public class Row
		{
			public int rowNumber { get; set; }
			public ExcelCell[] rowCell { get; set; }
		}

	}
}
