using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogParser;

public partial class LineParserTest
{
    [TestMethod]
    public void Test_GetType_Emote()
    {
        var lines = new List<Line>(6);
        lines.Add(LineParser.ParseLine("8/29 09:34:49.812  Finky says: I pare lat by nam zajelo wydobycie."));
        lines.Add(LineParser.ParseLine("10/12 18:18:06.046  Nelwo has come online."));
        lines.Add(LineParser.ParseLine("10/14 21:55:27.140  To Ysbail: o ja, 55 lvl :P"));
        lines.Add(LineParser.ParseLine("10/8 10:28:17.225  [2. Trade] Fuztail: just write no manlets allowed "));
        lines.Add(LineParser.ParseLine("10/8 15:25:11.146  |Hchannel:GUILD|h[Guild]|h Swistaq-ArgentDawn: jest jakas meta/"));
        lines.Add(LineParser.ParseLine("10/10 19:44:42.366  You receive loot: Spaulders of Kor'kron Fealty."));

        var emoteLines = new List<Line>(4);
        Line line1 = LineParser.ParseLine("10/29 16:50:22.515  Finky podrapal sie po glowie.");
        Line line2 = LineParser.ParseLine("11/7 16:54:10.281  Nelwo has gone offline.");
        Line line3 = LineParser.ParseLine("11/7 16:54:30.812  Nelwo has come online.");
        Line line4 = LineParser.ParseLine("5/6 23:29:51.792  Nelwo zaczal poprawiac siodlo");

        Assert.AreEqual(MessageType.Emote, line1.Type);
        Assert.AreEqual(MessageType.Emote, line2.Type);
        Assert.AreEqual(MessageType.Emote, line3.Type);
        Assert.AreEqual(MessageType.Emote, line4.Type);
    }
}

