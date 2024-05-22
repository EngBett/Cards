using System.ComponentModel;

namespace Agc.GoodShepherd.Domain.Enums;

public enum BibleVersions
{
    [Description("KJV")]KingJamesVersion=1,
    [Description("NKJV")]NewKingJamesVersion=2,
    [Description("NIV")]NewInternationalVersion=3,
    [Description("RSV")]RevisedStandardVersion=4,
    [Description("Good News bible")]GoodNewsBible=5,
    [Description("Gideons International Version")]GideonsInternationalVersion=6,
    [Description("New American Bible")]NewAmericanBible=7
}