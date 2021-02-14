using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 排课助手.Package
{
	public enum ErrorType { NoTeacher,NoCourse,NoClass,NoSubject}
	public enum TimeType { 上午, 下午 }

	public enum Week { 一 =1, 二 =2, 三= 3, 四 = 4, 五 =5}
	public enum GradeType { 一 = 1, 二 = 2, 三 = 3, 四 = 4, 五 = 5, 六 =6};
	public class Teacher :CourseContainer, IComparable
	{
		public string Name { get; set; }
		public int maxCourse;
		public HashSet<string> Subjects { set; get; } = new HashSet<string>();

		public void AddCourse(Course course)
		{
			Subjects.Add(course.Subject);
			Add(course);
		}

		public int CompareTo(object obj)
		{
			return Name.CompareTo(((Teacher)obj).Name);
		}

		public override bool Equals(object obj)
		{
			return this.Name.Equals(((Teacher)obj).Name);
		}

		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		public override string ToString()
		{
			return Name + "  " + GetCourses.Count + "/" + maxCourse;
		}

	}

	public class Course
	{
		public enum DifferentType { Class, Teacher, CourseType, Time }
		private SchoolClass _Class { get; set; }
		public SchoolClass Class
		{
			get
			{
				return _Class;
			}
			set
			{
				if (_Class != null)
					_Class.RemoveCourse(this);
				if (value == null)
				{
					_Class = null;
				}
				else
				{
					value.AddCourse(this);
					_Class = value;
				}
			}
		}
		private Teacher _teacher { get; set; }
		public Teacher Teacher
		{
			get
			{
				return _teacher;
			}
			set
			{
				if (_teacher != null)
					_teacher.RemoveCourse(this);

				if (value == null)
				{
					_teacher = null;
				}
				else
				{
					value.AddCourse(this);
					_teacher = value;
				}
			}
		}
		private string _subject { get; set; }
		public string Subject
		{
			get
			{
				return _subject;
			}
			set
			{
				if (School.Subjects.Contains(Subject) || Subject == null)
					_subject = value;
				else
					throw new Exception(ErrorType.NoCourse.ToString());
			}
		}
		public TimeType TimeType { get; }
		public Week Week { get; }
		public List<Teacher> GetAdviceTeachers
		{
			get
			{
				if (Subject == null)
					return School.GetAdvice(Class);
				else
					return School.GetAdvice(Class, Subject);
			}
		}

		public List<SchoolClass> GetAdviceClasses
		{
			get
			{
				if (Subject == null)
					return School.GetAdvice(Teacher, "");
				else
					return School.GetAdvice(Teacher, Subject);

			}
		}

		public List<string> GetAdviceSubjects
		{
			get
			{
				if (Teacher != null)
					return School.GetAdvice(Teacher);
				else
					return School.Subjects.ToList();
			}
		}

		public int TimeIndex { get; }

		public Course(TimeType timeType, Week week, int timeIndex)
		{
			this.TimeType = timeType;
			this.Week = week;
			this.TimeIndex = timeIndex;
		}

		public DifferentType[] findDifferent(Course course)
		{
			List<DifferentType> differentTypes = new List<DifferentType>();
			if (!Equals(course))
				differentTypes.Add(DifferentType.Time);
			else if (Class.Equals(course.Class))
				differentTypes.Add(DifferentType.Class);
			else if (Teacher.Equals(course.Teacher))
				differentTypes.Add(DifferentType.Teacher);
			else
				differentTypes.Add(DifferentType.CourseType);
			return differentTypes.ToArray();
		}

		//仅比较时间
		public override bool Equals(object obj)
		{
			Course course = (Course)obj;
			return Class.Equals(course.Class)&&TimeType.Equals(course.TimeType) && Week.Equals(course.Week) && TimeIndex.Equals(course.TimeIndex);
		}

		public override int GetHashCode()
		{
			int temp;
			if (Class == null)
				temp = base.GetHashCode();
			else
				temp = Class.GetHashCode();
			return temp*1000+(this.TimeType.ToString()+this.Week.ToString()+TimeIndex.ToString()).GetHashCode();
		}
	}

	public class CourseContainer
	{
		private HashSet<Course> _courses { set; get; } = new HashSet<Course>();
		public HashSet<Course> GetCourses { get { return _courses; } }
		protected void Add(Course course)
		{
			_courses.Add(course);
		}

		public void RemoveCourse(Course course)
		{
			_courses.Remove(course);
		}
	}

	public class SchoolClass:CourseContainer,IComparable
	{
		public int ClassIndex { get; set; }
		public int maxCourse;
		public GradeType grade { get; set; }
		public string Name 
		{
			get
			{
				return grade.ToString() + "(" + ClassIndex + ")班";
			}
		}
		public HashSet<Teacher> Teachers { get; set; } = new HashSet<Teacher>();

		public void AddCourse(Course course)
		{
			Teachers.Add(course.Teacher);
			Add(course);
		}

		public int CompareTo(object obj)
		{
			SchoolClass temp = (SchoolClass)obj;
			if (grade.CompareTo(temp.grade) > 0)
				return 1;
			else if (grade.CompareTo(temp.grade) == 0)
				return ClassIndex.CompareTo(temp.ClassIndex);
			else return -1;
		}

		public override bool Equals(object obj)
		{
			SchoolClass schoolClass = (SchoolClass)obj;
			return ClassIndex.Equals(schoolClass.ClassIndex)&&grade.Equals(schoolClass.grade);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return grade.ToString()+"("+ClassIndex+")班" + "  "+ GetCourses.Count+"/" + maxCourse;
		}
	}

	public class School
	{
		public static HashSet<Teacher> Teachers { get; set; } = new HashSet<Teacher>();
		public static HashSet<SchoolClass> SchoolClasses { get; set; } = new HashSet<SchoolClass>();
		public static HashSet<string> Subjects { get; set; } = new HashSet<string>();
		public HashSet<Teacher> GetTeachers { get { return Teachers; } } 
		public HashSet<SchoolClass> GetSchoolClasses { get { return SchoolClasses; } }
		public HashSet<string> GetCourseType { get { return Subjects; } }
		
		public static List<SchoolClass> GetClass(GradeType gradeType,int index = 0)
		{
			List<SchoolClass> rightList = new List<SchoolClass>();
			foreach(SchoolClass item in SchoolClasses)
			{
				if (gradeType == item.grade && (index == 0 || index == item.ClassIndex))
					rightList.Add(item);
			}
			return rightList;
		}

		public static List<SchoolClass> GetClass(int index = 0)
		{
			if (index == 0)
				return SchoolClasses.ToList<SchoolClass>();
			List<SchoolClass> rightList = new List<SchoolClass>();
			foreach (SchoolClass item in SchoolClasses)
			{
				if (index == item.ClassIndex)
					rightList.Add(item);
			}
			return rightList;
		}
		public static List<Teacher> GetAdvice(SchoolClass schoolClass,string subject = "")
		{
			HashSet<Teacher> resTeachers = new HashSet<Teacher>();
			foreach (Teacher item in schoolClass.Teachers)
			{
				if (subject == "" || item.Subjects.Contains(subject))
					resTeachers.Add(item);
			}
			return resTeachers.ToList();
		}

		public static List<string> GetAdvice(Teacher teacher)
		{
			return teacher.Subjects.ToList();
		}

		public static List<SchoolClass> GetAdvice(Teacher teacher,string subject = "")
		{
			HashSet<SchoolClass> resClasses = new HashSet<SchoolClass>();
			foreach (SchoolClass item in School.SchoolClasses)
			{
				if (item.Teachers.Contains(teacher))
					resClasses.Add(item);
			}
			return resClasses.ToList();
		}
	}
}
