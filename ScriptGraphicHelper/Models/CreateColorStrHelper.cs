﻿using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using static System.Environment;

namespace ScriptGraphicHelper.Models
{
    public static class CreateColorStrHelper
    {
        public static bool IsAddRange { get; set; } = false;
        public static string Create(int index, ObservableCollection<ColorInfo> colorInfos, Range rect = null)
        {
            return index switch
            {
                0 => CompareStr(colorInfos),
                1 => DmFindStr(colorInfos, rect),
                2 => AjFindStr(colorInfos, rect),
                3 => AjCompareStr(colorInfos),
                4 => CdFindStr(colorInfos, rect),
                5 => CdCompareStr(colorInfos),
                6 => AutojsFindStr(colorInfos, rect),
                7 => EcFindStr(colorInfos, rect),
                8 => DiyFindStr(colorInfos, rect),
                9 => DiyCompareStr(colorInfos),
                10 => AnchorsCompareStr(colorInfos),
                11 => AnchorsCompareStr(colorInfos),
                12 => AnchorsCompareStrTest(colorInfos),
                13 => AnchorsCompareStrTest(colorInfos),
                _ => CompareStr(colorInfos),
            };
        }
        public static DiyFormat GetDiyFormat()
        {
            StreamReader sr = File.OpenText(CurrentDirectory + "\\diyFormat.json");
            string result = sr.ReadToEnd();
            sr.Close();
            return JsonConvert.DeserializeObject<DiyFormat>(result);
        }
        public static string DiyCompareStr(ObservableCollection<ColorInfo> colorInfos)
        {
            DiyFormat diyFormat = GetDiyFormat();
            string colorStr = string.Empty;
            foreach (ColorInfo colorInfo in colorInfos)
            {
                if (colorInfo.IsChecked)
                {
                    if (!diyFormat.IsBgr)
                    {
                        string res = diyFormat.FollowColorFormat;
                        string color = colorInfo.TheColor.R.ToString("x2") + colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.B.ToString("x2");
                        string offsetColor = colorInfo.OffsetColor;
                        if (res.IndexOf("{x}") != -1)
                        {
                            res = res.Replace("{x}", colorInfo.ThePoint.X.ToString());
                        }
                        if (res.IndexOf("{y}") != -1)
                        {
                            res = res.Replace("{y}", colorInfo.ThePoint.Y.ToString());
                        }
                        if (res.IndexOf("{color}") != -1)
                        {
                            res = res.Replace("{color}", color);
                        }
                        if (res.IndexOf("{offsetColor}") != -1 && offsetColor != "000000")
                        {
                            res = res.Replace("{offsetColor}", offsetColor);
                        }
                        else
                        {
                            res = res.Substring(0, res.IndexOf(color) + 6);
                        }
                        colorStr += res + ",";
                    }
                    else
                    {
                        string res = diyFormat.FollowColorFormat;
                        string color = colorInfo.TheColor.B.ToString("x2") + colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.R.ToString("x2");
                        string offsetColor = colorInfo.OffsetColor.Substring(4, 2) + colorInfo.OffsetColor.Substring(2, 2) + colorInfo.OffsetColor.Substring(0, 2);
                        if (res.IndexOf("{x}") != -1)
                        {
                            res = res.Replace("{x}", colorInfo.ThePoint.X.ToString());
                        }
                        if (res.IndexOf("{y}") != -1)
                        {
                            res = res.Replace("{y}", colorInfo.ThePoint.Y.ToString());
                        }
                        if (res.IndexOf("{color}") != -1)
                        {
                            res = res.Replace("{color}", color);
                        }
                        if (res.IndexOf("{offsetColor}") != -1 && offsetColor != "000000")
                        {
                            res = res.Replace("{offsetColor}", offsetColor);
                        }
                        else
                        {
                            res = res.Substring(0, res.IndexOf(color) + 6);
                        }
                        colorStr += res + ",";
                    }
                }
            }
            colorStr = "\"" + colorStr.Trim(',') + "\"";
            string result = diyFormat.CompareStrFormat;

            if (result.IndexOf("{colorStr}") != -1)
            {
                result = result.Replace("{colorStr}", colorStr);
            }
            return result;
        }
        public static string DiyFindStr(ObservableCollection<ColorInfo> colorInfos, Range rect)
        {
            DiyFormat diyFormat = GetDiyFormat();
            bool isInit = false;
            Point startPoint = new Point();
            string[] colorStr = new string[2];
            foreach (ColorInfo colorInfo in colorInfos)
            {
                if (colorInfo.IsChecked)
                {
                    if (!isInit)
                    {
                        isInit = true;
                        startPoint = colorInfo.ThePoint;
                        if (!diyFormat.IsBgr)
                        {
                            string res = diyFormat.FirstColorFormat;
                            string color = colorInfo.TheColor.R.ToString("x2") + colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.B.ToString("x2");
                            string offsetColor = colorInfo.OffsetColor;
                            if (res.IndexOf("{color}") != -1)
                            {
                                res = res.Replace("{color}", color);
                            }
                            if (res.IndexOf("{offsetColor}") != -1 && offsetColor != "000000")
                            {
                                res = res.Replace("{offsetColor}", offsetColor);
                            }
                            else
                            {
                                res = res.Substring(0, res.IndexOf(color) + 6);
                            }
                            colorStr[0] += "\"" + res + "\"";
                        }
                        else
                        {
                            string res = diyFormat.FirstColorFormat;
                            string color = colorInfo.TheColor.B.ToString("x2") + colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.R.ToString("x2");
                            string offsetColor = colorInfo.OffsetColor.Substring(4, 2) + colorInfo.OffsetColor.Substring(2, 2) + colorInfo.OffsetColor.Substring(0, 2);
                            if (diyFormat.FirstColorFormat.IndexOf("{color}") != -1)
                            {
                                res = res.Replace("{color}", color);
                            }
                            if (diyFormat.FirstColorFormat.IndexOf("{offsetColor}") != -1 && offsetColor != "000000")
                            {
                                res = res.Replace("{offsetColor}", offsetColor);
                            }
                            else
                            {
                                res = res.Substring(0, res.IndexOf(color) + 6);
                            }
                            colorStr[0] += "\"" + res + "\"";
                        }
                    }
                    else
                    {
                        double OffsetX = colorInfo.ThePoint.X - startPoint.X;
                        double OffsetY = colorInfo.ThePoint.Y - startPoint.Y;
                        if (!diyFormat.IsBgr)
                        {
                            string res = diyFormat.FollowColorFormat;
                            string color = colorInfo.TheColor.R.ToString("x2") + colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.B.ToString("x2");
                            string offsetColor = colorInfo.OffsetColor;
                            if (res.IndexOf("{x}") != -1)
                            {
                                res = res.Replace("{x}", OffsetX.ToString());
                            }
                            if (res.IndexOf("{y}") != -1)
                            {
                                res = res.Replace("{y}", OffsetY.ToString());
                            }
                            if (res.IndexOf("{color}") != -1)
                            {
                                res = res.Replace("{color}", color);
                            }
                            if (res.IndexOf("{offsetColor}") != -1 && offsetColor != "000000")
                            {
                                res = res.Replace("{offsetColor}", offsetColor);
                            }
                            else
                            {
                                res = res.Substring(0, res.IndexOf(color) + 6);
                            }
                            colorStr[1] += res + ",";
                        }
                        else
                        {
                            string res = diyFormat.FollowColorFormat;
                            string color = colorInfo.TheColor.B.ToString("x2") + colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.R.ToString("x2");
                            string offsetColor = colorInfo.OffsetColor.Substring(4, 2) + colorInfo.OffsetColor.Substring(2, 2) + colorInfo.OffsetColor.Substring(0, 2);
                            if (res.IndexOf("{x}") != -1)
                            {
                                res = res.Replace("{x}", OffsetX.ToString());
                            }
                            if (res.IndexOf("{y}") != -1)
                            {
                                res = res.Replace("{y}", OffsetY.ToString());
                            }
                            if (res.IndexOf("{color}") != -1)
                            {
                                res = res.Replace("{color}", color);
                            }
                            if (res.IndexOf("{offsetColor}") != -1 && offsetColor != "000000")
                            {
                                res = res.Replace("{offsetColor}", offsetColor);
                            }
                            else
                            {
                                res = res.Substring(0, res.IndexOf(color) + 6);
                            }
                            colorStr[1] += res + ",";
                        }
                    }
                }
            }
            colorStr[1] = "\"" + colorStr[1].Trim(',') + "\"";
            string result = diyFormat.FindStrFormat;
            if (result.IndexOf("{range}") != -1)
            {
                result = result.Replace("{range}", rect.ToStr());
            }
            if (result.IndexOf("{firstColorStr}") != -1)
            {
                result = result.Replace("{firstColorStr}", colorStr[0]);
            }
            if (result.IndexOf("{followColorStr}") != -1)
            {
                result = result.Replace("{followColorStr}", colorStr[1]);
            }
            return result;
        }
        public static string DmFindStr(ObservableCollection<ColorInfo> colorInfos, Range rect)
        {
            string result = string.Empty;
            if (IsAddRange)
            {
                result = rect.ToStr() + ",";
            }
            bool isInit = false;
            Point startPoint = new Point();
            foreach (ColorInfo colorInfo in colorInfos)
            {
                if (colorInfo.IsChecked)
                {
                    if (!isInit)
                    {
                        isInit = true;
                        startPoint = colorInfo.ThePoint;
                        if (colorInfo.OffsetColor == "000000")
                            result += "\"" + colorInfo.TheColor.R.ToString("x2") + colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.B.ToString("x2") + "\",\"";
                        else
                            result += "\"" + colorInfo.TheColor.R.ToString("x2") + colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.B.ToString("x2") + "-" + colorInfo.OffsetColor + "\",\"";
                    }
                    else
                    {
                        double OffsetX = colorInfo.ThePoint.X - startPoint.X;
                        double OffsetY = colorInfo.ThePoint.Y - startPoint.Y;
                        if (colorInfo.OffsetColor == "000000")
                            result += OffsetX.ToString() + "|" + OffsetY.ToString() + "|" + colorInfo.TheColor.R.ToString("x2") + colorInfo.TheColor.G.ToString("x2") +
                            colorInfo.TheColor.B.ToString("x2") + ",";
                        else
                            result += OffsetX.ToString() + "|" + OffsetY.ToString() + "|" + colorInfo.TheColor.R.ToString("x2") + colorInfo.TheColor.G.ToString("x2") +
                            colorInfo.TheColor.B.ToString("x2") + "-" + colorInfo.OffsetColor + ",";
                    }
                }
            }
            result = result.Trim(',');
            result += "\"";
            return result;
        }
        public static string CdFindStr(ObservableCollection<ColorInfo> colorInfos, Range rect)
        {
            string result = string.Empty;
            Point startPoint = new Point();
            foreach (ColorInfo colorInfo in colorInfos)
            {
                if (colorInfo.IsChecked)
                {
                    if (result == string.Empty)
                    {
                        startPoint = colorInfo.ThePoint;
                        if (colorInfo.OffsetColor == "000000")
                            result += "0x" + colorInfo.TheColor.R.ToString("x2") + colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.B.ToString("x2") + ",\"";
                        else
                            result += "0x" + colorInfo.TheColor.R.ToString("x2") + colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.B.ToString("x2") + "|0x" + colorInfo.OffsetColor + ",\"";
                    }
                    else
                    {
                        double OffsetX = colorInfo.ThePoint.X - startPoint.X;
                        double OffsetY = colorInfo.ThePoint.Y - startPoint.Y;
                        if (colorInfo.OffsetColor == "000000")
                            result += OffsetX.ToString() + "|" + OffsetY.ToString() + "|0x" + colorInfo.TheColor.R.ToString("x2") + colorInfo.TheColor.G.ToString("x2") +
                            colorInfo.TheColor.B.ToString("x2") + ",";
                        else
                            result += OffsetX.ToString() + "|" + OffsetY.ToString() + "|0x" + colorInfo.TheColor.R.ToString("x2") + colorInfo.TheColor.G.ToString("x2") +
                            colorInfo.TheColor.B.ToString("x2") + "|0x" + colorInfo.OffsetColor + ",";
                    }
                }
            }
            result = result.Trim(',');
            if (IsAddRange)
            {
                result += "\",90," + rect.ToStr();
            }
            else
            {
                result += "\"";
            }
            return result;
        }
        public static string AjFindStr(ObservableCollection<ColorInfo> colorInfos, Range rect)
        {
            string result = string.Empty;
            if (IsAddRange)
            {
                result = rect.ToStr() + ",";
            }
            bool isInit = false;
            Point startPoint = new Point();
            foreach (ColorInfo colorInfo in colorInfos)
            {
                if (colorInfo.IsChecked)
                {
                    if (!isInit)
                    {
                        isInit = true;
                        startPoint = colorInfo.ThePoint;
                        if (colorInfo.OffsetColor == "000000")
                            result += "\"" + colorInfo.TheColor.B.ToString("x2") + colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.R.ToString("x2") + "\",\"";
                        else
                        {
                            string offsetColor = colorInfo.OffsetColor.Substring(4, 2) + colorInfo.OffsetColor.Substring(2, 2) + colorInfo.OffsetColor.Substring(0, 2);
                            result += "\"" + colorInfo.TheColor.B.ToString("x2") + colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.R.ToString("x2") + "-" + offsetColor + "\",\"";
                        }
                    }
                    else
                    {
                        double OffsetX = colorInfo.ThePoint.X - startPoint.X;
                        double OffsetY = colorInfo.ThePoint.Y - startPoint.Y;
                        if (colorInfo.OffsetColor == "000000")
                            result += OffsetX.ToString() + "|" + OffsetY.ToString() + "|" + colorInfo.TheColor.B.ToString("x2") + colorInfo.TheColor.G.ToString("x2") +
                            colorInfo.TheColor.R.ToString("x2") + ",";
                        else
                        {
                            string offsetColor = colorInfo.OffsetColor.Substring(4, 2) + colorInfo.OffsetColor.Substring(2, 2) + colorInfo.OffsetColor.Substring(0, 2);
                            result += OffsetX.ToString() + "|" + OffsetY.ToString() + "|" + colorInfo.TheColor.B.ToString("x2") + colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.R.ToString("x2") + "-" + offsetColor + ",";
                        }
                    }
                }
            }
            result = result.Trim(',');
            result += "\"";
            return result;
        }
        public static string AutojsFindStr(ObservableCollection<ColorInfo> colorInfos, Range rect)
        {
            string result = string.Empty;
            Point startPoint = new Point();
            foreach (ColorInfo colorInfo in colorInfos)
            {
                if (colorInfo.IsChecked)
                {
                    if (result == string.Empty)
                    {
                        startPoint = colorInfo.ThePoint;
                        result = "\"#" + colorInfo.TheColor.R.ToString("x2") + colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.B.ToString("x2") + "\",[";
                    }
                    else
                    {
                        double OffsetX = colorInfo.ThePoint.X - startPoint.X;
                        double OffsetY = colorInfo.ThePoint.Y - startPoint.Y;
                        result += "[" + OffsetX.ToString() + "," + OffsetY.ToString() + ",\"#" + colorInfo.TheColor.R.ToString("x2") + colorInfo.TheColor.G.ToString("x2") +
                            colorInfo.TheColor.B.ToString("x2") + "\"],";
                    }
                }
            }
            result = result.Trim(',');
            if (IsAddRange)
            {
                result += "],{region:[" + rect.ToStr(1) + "],threshold:[26]}";
            }
            else
            {
                result += "]";
            }
            return result;

        }
        public static string EcFindStr(ObservableCollection<ColorInfo> colorInfos, Range rect)
        {
            string result = string.Empty;
            Point startPoint = new Point();
            foreach (ColorInfo colorInfo in colorInfos)
            {
                if (colorInfo.IsChecked)
                {
                    if (result == string.Empty)
                    {
                        startPoint = colorInfo.ThePoint;
                        if (colorInfo.OffsetColor == "000000")
                            result += "\"0x" + colorInfo.TheColor.R.ToString("x2") + colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.B.ToString("x2") + "\",\"";
                        else
                            result += "\"0x" + colorInfo.TheColor.R.ToString("x2") + colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.B.ToString("x2") + "-0x" + colorInfo.OffsetColor + "\",\"";
                    }
                    else
                    {
                        double OffsetX = colorInfo.ThePoint.X - startPoint.X;
                        double OffsetY = colorInfo.ThePoint.Y - startPoint.Y;
                        if (colorInfo.OffsetColor == "000000")
                            result += OffsetX.ToString() + "|" + OffsetY.ToString() + "|0x" + colorInfo.TheColor.R.ToString("x2") + colorInfo.TheColor.G.ToString("x2") +
                            colorInfo.TheColor.B.ToString("x2") + ",";
                        else
                            result += OffsetX.ToString() + "|" + OffsetY.ToString() + "|0x" + colorInfo.TheColor.R.ToString("x2") + colorInfo.TheColor.G.ToString("x2") +
                           colorInfo.TheColor.B.ToString("x2") + "-0x" + colorInfo.OffsetColor + ",";
                    }
                }
            }
            result = result.Trim(',');
            if (IsAddRange)
            {
                result += "\",0.9," + rect.ToStr();
            }
            else
            {
                result += "\"";
            }

            return result;
        }
        public static string CompareStr(ObservableCollection<ColorInfo> colorInfos)
        {
            string result = "\"";
            foreach (ColorInfo colorInfo in colorInfos)
            {
                if (colorInfo.IsChecked)
                {
                    if (colorInfo.OffsetColor == "000000")
                        result += colorInfo.ThePoint.X.ToString() + "|" + colorInfo.ThePoint.Y.ToString() + "|" + colorInfo.TheColor.R.ToString("x2") +
                        colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.B.ToString("x2") + ",";
                    else
                        result += colorInfo.ThePoint.X.ToString() + "|" + colorInfo.ThePoint.Y.ToString() + "|" + colorInfo.TheColor.R.ToString("x2") +
                        colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.B.ToString("x2") + "-" + colorInfo.OffsetColor + ",";
                }
            }
            result = result.Trim(',');
            result += "\"";
            return result;
        }
        public static string AjCompareStr(ObservableCollection<ColorInfo> colorInfos)
        {
            string result = "\"";
            foreach (ColorInfo colorInfo in colorInfos)
            {
                if (colorInfo.IsChecked)
                {
                    if (colorInfo.OffsetColor == "000000")
                        result += colorInfo.ThePoint.X.ToString() + "|" + colorInfo.ThePoint.Y.ToString() + "|" + colorInfo.TheColor.B.ToString("x2") + colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.R.ToString("x2") + ",";
                    else
                    {
                        string offsetColor = colorInfo.OffsetColor.Substring(4, 2) + colorInfo.OffsetColor.Substring(2, 2) + colorInfo.OffsetColor.Substring(0, 2);
                        result += colorInfo.ThePoint.X.ToString() + "|" + colorInfo.ThePoint.Y.ToString() + "|" + colorInfo.TheColor.B.ToString("x2") + colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.R.ToString("x2") + "-" + offsetColor + ",";
                    }
                }
            }
            result = result.Trim(',');
            result += "\"";
            return result;
        }
        public static string CdCompareStr(ObservableCollection<ColorInfo> colorInfos)
        {
            string result = "{";
            foreach (ColorInfo colorInfo in colorInfos)
            {
                if (colorInfo.IsChecked)
                {
                    result += "{" + colorInfo.ThePoint.X.ToString() + "," + colorInfo.ThePoint.Y.ToString() + ",0x" + colorInfo.TheColor.R.ToString("x2") + colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.B.ToString("x2") + "},";
                }
            }
            result = result.Trim(',');
            result += "}";
            return result;
        }

        public static string AnchorsCompareStr(ObservableCollection<ColorInfo> colorInfos)
        {
            string result = "[" + colorInfos[0].Width.ToString() + "," + colorInfos[0].Height.ToString() + ",\r\n[";
            foreach (ColorInfo colorInfo in colorInfos)
            {
                if (colorInfo.IsChecked)
                {
                    if (colorInfo.Anchors == "L")
                        result += "[left,";
                    else if (colorInfo.Anchors == "C")
                        result += "[center,";
                    else if (colorInfo.Anchors == "R")
                        result += "[right,";

                    if (colorInfo.OffsetColor == "000000")
                    {
                        result += colorInfo.ThePoint.X.ToString() + "," + colorInfo.ThePoint.Y.ToString() + ",0x" + colorInfo.TheColor.R.ToString("x2") +
                        colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.B.ToString("x2") + "],\r\n";
                    }
                    else
                    {
                        result += colorInfo.ThePoint.X.ToString() + "," + colorInfo.ThePoint.Y.ToString() + ",0x" + colorInfo.TheColor.R.ToString("x2") +
                        colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.B.ToString("x2") + ",0x" + colorInfo.OffsetColor + "],\r\n";
                    }
                }
            }
            result = result.Trim(",\r\n".ToCharArray());
            result += "]\r\n]";
            return result;
        }
        public static string AnchorsCompareStrTest(ObservableCollection<ColorInfo> colorInfos)
        {
            string result = "\"";
            foreach (ColorInfo colorInfo in colorInfos)
            {
                if (colorInfo.IsChecked)
                {
                    result += colorInfo.Anchors + "|" + colorInfo.ThePoint.X.ToString() + "|" + colorInfo.ThePoint.Y.ToString() + "|" + colorInfo.TheColor.R.ToString("x2") +
                        colorInfo.TheColor.G.ToString("x2") + colorInfo.TheColor.B.ToString("x2") + ",";
                }
            }
            result = result.Trim(',');
            result += "\"";
            return result;
        }
    }
}
