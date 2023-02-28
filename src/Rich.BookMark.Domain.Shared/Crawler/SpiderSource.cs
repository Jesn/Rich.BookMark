using System.ComponentModel;

namespace Rich.BookMark.Crawler;

/// <summary>
/// 数据采集来源
/// </summary>
public enum SpiderSourceEnum
{
    /// <summary>
    /// 集知盒子
    /// https://www.jizhihezi.com
    /// </summary>
    [Description("集知盒子")] jizhihezi = 0,
}