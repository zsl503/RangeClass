using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
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
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using 排课助手.Package;
using Windows.Storage.Pickers;
using Windows.Storage;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace 排课助手
{
    public sealed class Column
    {
        public string Context { get; set; }
    }
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //ExcelCell[,] excelCell;
        public MainPage()
        {
            this.InitializeComponent();


        }

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
            Frame root = Window.Current.Content as Frame;
            //excelCell = await ExcelAsync();
            //root.Navigate(typeof(ExcelPage), excelCell);
            root.Navigate(typeof(CourseTable));

        }

        async System.Threading.Tasks.Task<ExcelCell[,]> ExcelAsync()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.CommitButtonText = "选中此文件";
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            openPicker.FileTypeFilter.Add(".xlsx");
            openPicker.FileTypeFilter.Add(".xls");
            // * 代表全部
            // openPicker.FileTypeFilter.Add("*");

            // 弹出文件选择窗口
            StorageFile file = await openPicker.PickSingleFileAsync();
            Stream fileStream = await file.OpenStreamForReadAsync();
            IWorkbook workbook = null;  //新建IWorkbook对象  
            //new FileStream(fileName, FileMode.Open, FileAccess.Read);
            if (file.Name.IndexOf(".xlsx") > 0) // 2007版本  
			{
				workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook  
			}
			else if (file.Name.IndexOf(".xls") > 0) // 2003版本  
			{
				workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook  
			}

            ISheet sheet = workbook.GetSheetAt(0);  //获取第一个工作表  
            IRow row;// = sheet.GetRow(0);            //新建当前工作表行数据  
            int maxColNum = 0;
            for (int i = 0; i < sheet.LastRowNum; i++)  //对工作表每一行  
            {
                row = sheet.GetRow(i);   //row读入第i行数据  
                if (row != null)
                {
                    if (row.LastCellNum > maxColNum)
                        maxColNum = row.LastCellNum;
                }
            }

            ExcelCell[,] temp = new ExcelCell[maxColNum, sheet.LastRowNum];

			for (int i = 0; i < temp.GetLength(0); i++)
			{
				for (int j = 0; j < temp.GetLength(1); j++)
				{
                    temp[i,j] = new ExcelCell() { text = "" };
                    if (sheet.GetRow(j) != null && sheet.GetRow(j).GetCell(i) != null)
                        temp[i, j].text = sheet.GetRow(j).GetCell(i).ToString();
                }
			}

            for (int i = 0; i < sheet.LastRowNum; i++)  //对工作表每一行  
            {
                row = sheet.GetRow(i);   //row读入第i行数据  
                if (row != null)
                {
                    for (int j = 0; j < row.LastCellNum; j++)  //对工作表每一列  
                    {
                        temp[j,i] = new ExcelCell(); 
                        if (row.GetCell(j) != null)
                            temp[j,i].text = row.GetCell(j).ToString();
                        else temp[j,i].text = "";
                    }
                }
            }
			//Console.ReadLine();
			fileStream.Close();
			workbook.Close();

			return temp;
        }
	}


}
