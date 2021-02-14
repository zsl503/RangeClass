using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using 排课助手.Package;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace 排课助手
{
	/// <summary>
	/// 可用于自身或导航至 Frame 内部的空白页。
	/// </summary>
	/// 


	public sealed partial class Editor : Page
	{
		public School school = new School();
		public TableTemplate tableTemplate = new TableTemplate();
		public CourseContainer courses { get; set; }
		public string TableName{get;set;}

		private void Cell_Drop(object sender, DragEventArgs e)
		{
			Grid grid = (Grid)sender;
		}

		private void Cell_DragEnter(object sender, DragEventArgs e)
		{
			e.AcceptedOperation = DataPackageOperation.Copy;
		}

		private void initTeacherList()
		{
			TreeViewNode node = new TreeViewNode() { Content = "全部老师" };
			List<Teacher> teachers = School.Teachers.ToList();
			teachers.Sort();
			foreach (Teacher item in teachers)
			{
				TreeViewNode children = new TreeViewNode() { Content = item};
				node.Children.Add(children);
			}
			TeacherList.RootNodes.Add(node);
		}

		private void initGradeClassList(GradeType gradeType)
		{
			TreeViewNode node = new TreeViewNode() { Content = gradeType.ToString()+"年级" };
			List<SchoolClass> schoolClasses = School.GetClass(gradeType);
			schoolClasses.Sort();
			foreach(SchoolClass item in schoolClasses)
			{
				TreeViewNode children = new TreeViewNode() { Content = item};
				node.Children.Add(children);
			}
			ClassList.RootNodes.Add(node);
			TreeViewList treeViewList = new TreeViewList();
			
		}

		private void initClassList()
		{
			TreeViewNode node = new TreeViewNode() { Content = "全部班级"};
			List<SchoolClass> schoolClasses = School.GetClass();
			schoolClasses.Sort();
			foreach (SchoolClass item in schoolClasses)
			{
				TreeViewNode children = new TreeViewNode() { Content = item };
				node.Children.Add(children);
			}
			ClassList.RootNodes.Add(node);
			initGradeClassList(GradeType.一);
			initGradeClassList(GradeType.二);
			initGradeClassList(GradeType.三);
			initGradeClassList(GradeType.四);
			initGradeClassList(GradeType.五);
			initGradeClassList(GradeType.六);
		}

		public Editor()
		{
			this.InitializeComponent();
			School.SchoolClasses.Add(new SchoolClass() { grade = GradeType.一, ClassIndex = 1 });
			School.SchoolClasses.Add(new SchoolClass() { grade = GradeType.一, ClassIndex = 2 });
			School.SchoolClasses.Add(new SchoolClass() { grade = GradeType.二, ClassIndex = 1 });
			School.SchoolClasses.Add(new SchoolClass() { grade = GradeType.三, ClassIndex = 1 });
			Teacher teacher = new Teacher() { Name = "吴少宏" };
			School.Teachers.Add(teacher);
			School.Subjects.Add("语文");
			//School.Subjects.Add("数学");
			//School.Subjects.Add("英语");
			//School.Subjects.Add("英语");

			//Teacher teacher2 = new Teacher() { Name = "吴少宏2" };
			//School.Teachers.Add(teacher2);
			SchoolClass schoolClass = new SchoolClass() { grade = GradeType.一, ClassIndex = 1 };
			School.SchoolClasses.Add(schoolClass);
			new Course(TimeType.上午, Week.一, 2) { Class = schoolClass,Teacher = teacher,Subject = "语文"};
			courses = schoolClass;
			TableName = schoolClass.ToString();
			initTeacherList();
			initClassList();
		}

		private void ClassList_DragStarting(UIElement sender, DragStartingEventArgs args)
		{

			//args.Data.SetData(RandomAccessStreamReference.CreateFromFile,sender.get)

		}

		private void ClassList_DragItemsStarting(TreeView sender, TreeViewDragItemsStartingEventArgs args)
		{
			args.Data.RequestedOperation = DataPackageOperation.Copy;
			Console.WriteLine(sender);
		}

		private void ClassList_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
		{
			SchoolClass Class = ((TreeViewNode)args.InvokedItem).Content as SchoolClass;
			if (Class != null)
			{
				Table.Children.Clear();
				Table.Children.Add(new CourseTable() { CourseContainer = Class, Header = Class.Name });
			}
		}

		private void TeacherList_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
		{
			Teacher teacher = ((TreeViewNode)args.InvokedItem).Content as Teacher;
			if (teacher != null)
			{
				Table.Children.Clear();
				Table.Children.Add(new CourseTable() { CourseContainer = teacher, Header = teacher.Name });
			}
		}
	}
}
