using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogParser;
using System;

[TestClass]
public partial class LineParserTest
{
    [TestMethod]
    public void Test_GetTimeFromString_ReturnsDefault_OnNonCompatibleString()
    {
        DateTime dateTime1 = LineParser.GetTimeFromString("");
        DateTime dateTime2 = LineParser.GetTimeFromString(null);
        DateTime dateTime3 = LineParser.GetTimeFromString("blablabla");
        DateTime dateTime4 = LineParser.GetTimeFromString("32423423");
        DateTime dateTime5 = LineParser.GetTimeFromString("g1");
        DateTime dateTime6 = LineParser.GetTimeFromString("234 sdf 23");

        Assert.AreEqual(default(DateTime), dateTime1);
        Assert.AreEqual(default(DateTime), dateTime2);
        Assert.AreEqual(default(DateTime), dateTime3);
        Assert.AreEqual(default(DateTime), dateTime4);
        Assert.AreEqual(default(DateTime), dateTime5);
        Assert.AreEqual(default(DateTime), dateTime6);
    }

   
}