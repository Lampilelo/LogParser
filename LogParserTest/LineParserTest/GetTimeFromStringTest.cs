using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogParser;
using System;

[TestClass]
public partial class LineParserTest
{
    //TODO: Rework!
    [TestMethod]
    public void Test_GetTimeFromString_ReturnsDefault_OnNonCompatibleString()
    {
        Line line1 = LineParser.ParseLine("");
        Line line2 = LineParser.ParseLine(null);
        Line line3 = LineParser.ParseLine("blablabla");
        Line line4 = LineParser.ParseLine("32423423");
        Line line5 = LineParser.ParseLine("g1");
        Line line6 = LineParser.ParseLine("234 sdf 23");

        Assert.Fail();
    }

   
}