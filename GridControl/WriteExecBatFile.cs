﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Common;
using System.Runtime.InteropServices;
using System.Diagnostics;
using SysDAL;
using System.Data;
namespace GridControl
{
    public class WriteExecBatFile
    {
        public static bool UpdateExecBatFileByTemplateExecsingle(string fileName, string ComputeUnit, string start, string end, string timeNums, string datName, string outrainTilepath, string yearmmddForID)
        {
            DirectoryInfo info = new DirectoryInfo(fileName);
            String apppath = info.Parent.FullName;

            if (!Directory.Exists(apppath))
            {
                return false;
            }


            //！ 写之前先创建
            FileStream fileStreamWriter = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter(fileStreamWriter, Encoding.GetEncoding("GB2312"));

            string p = System.IO.Directory.GetDirectoryRoot(fileName);
            string Contents = p.Substring(0, p.Length - 1) + "\r\ncd " + apppath;
            //! 写出到输出文件流中。
            streamWriter.WriteLine(Contents);

            //! 写参数
            string newLine = String.Format("DCFDProc.exe {0} 60 60 1 1 ", ComputeUnit);
            string parasLine = String.Format("-m grid -exec false -t forecast -usegroovy true -methodtopo wata -computernode {0} -s {1} -c {2} -datTimes {3} -curDatGridName {4} -gridRainRoot {5} -gridFBCRoot {6} -yearid {7}",
                                             HookHelper.computerNode, start, end, timeNums, datName, outrainTilepath, HookHelper.rainSRCDirectory.Replace('\\', '/'), yearmmddForID);

            if (HookHelper.isGridout)
            {
                parasLine = String.Format("-m grid -exec false -t forecast -usegroovy true -methodtopo wata -computernode {0} -s {1} -c {2} -datTimes {3} -curDatGridName {4} -gridRainRoot {5} -gridFBCRoot {6} -yearid {7} -isGridout {8}",
                                             HookHelper.computerNode, start, end, timeNums, datName, outrainTilepath, HookHelper.rainSRCDirectory.Replace('\\', '/'), yearmmddForID, true);
            }

            streamWriter.WriteLine(newLine + parasLine);
            //! 结束----
            //! 结束读取流
            streamWriter.Close();
            fileStreamWriter.Close();

            return true;
        }
        
    }

}
