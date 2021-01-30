using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 排课助手
{
	public class TableTamplate
	{
		static public string[] headerText;
		static public string[] moningText;
		static public string[] afternoonText;
		static public string restText;
		static public int teacherMode;
		static public int studentMode;
		static public void initTableTamplate(string[] headerText,
			string[] moningText, string[] afternoonText,
			string restText="午休",int teacherMode=0,int studentMode=0)
		{
			TableTamplate.headerText = headerText;
			TableTamplate.moningText = moningText;
			TableTamplate.afternoonText = afternoonText;
			TableTamplate.restText = restText;
			TableTamplate.teacherMode = teacherMode;
			TableTamplate.studentMode = studentMode;
		}
	}
}
