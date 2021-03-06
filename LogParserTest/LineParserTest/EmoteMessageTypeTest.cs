﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogParser;

public partial class LineParserTest
{
    [TestMethod]
    public void Test_GetType_NotDefinedOrSystem_BeforeParseEmote()
    {
        LineParser.ClearNameSet();
        Line line1 = LineParser.ParseLine("10/29 16:50:22.515  Finky podrapal sie po glowie.");
        Line line2 = LineParser.ParseLine("11/7 16:54:10.281  Nelwo has gone offline.");
        Line line3 = LineParser.ParseLine("11/7 16:54:30.812  Nelwo has come online.");
        Line line4 = LineParser.ParseLine("5/6 23:29:51.792  Nelwo zaczal poprawiac siodlo");

        Assert.AreEqual(MessageType.NotDefined, line1.Type);
        Assert.AreEqual(MessageType.System, line2.Type);
        Assert.AreEqual(MessageType.System, line3.Type);
        Assert.AreEqual(MessageType.NotDefined, line4.Type);
    }

    [TestMethod]
    public void Test_GetType_Emote()
    {
        LineParser.ClearNameSet();
        // Populate nameSet with data
        LineParser.ParseLine("8/29 09:34:49.812  Finky says: I pare lat by nam zajelo wydobycie.");
        LineParser.ParseLine("2/1 15:20:04.045  Nelwo whispers: questy sie robi");
        LineParser.ParseLine("10/14 21:55:27.140  To Ysbail: o ja, 55 lvl :P");
        LineParser.ParseLine("10/8 10:28:17.225  [2. Trade] Fuztail: just write no manlets allowed ");
        LineParser.ParseLine("10/8 15:25:11.146  |Hchannel:GUILD|h[Guild]|h Swistaq-ArgentDawn: jest jakas meta/");
        LineParser.ParseLine("10/10 19:44:42.366  You receive loot: Spaulders of Kor'kron Fealty.");
        
        Line line1 = LineParser.ParseLine("10/29 16:50:22.515  Finky podrapal sie po glowie.");
        Line line2 = LineParser.ParseLine("11/7 16:54:10.281  Nelwo has gone offline.");
        Line line3 = LineParser.ParseLine("11/7 16:54:30.812  Nelwo has come online.");
        Line line4 = LineParser.ParseLine("5/6 23:29:51.792  Nelwo zaczal poprawiac siodlo");

        LineParser.PrepareEmoteParsing();
        line1 = LineParser.ParseEmote(line1);
        line2 = LineParser.ParseEmote(line2);
        line3 = LineParser.ParseEmote(line3);
        line4 = LineParser.ParseEmote(line4);

        Assert.AreEqual(MessageType.Emote, line1.Type);
        Assert.AreEqual(MessageType.System, line2.Type);
        Assert.AreEqual(MessageType.System, line3.Type);
        Assert.AreEqual(MessageType.Emote, line4.Type);
    }

    [TestMethod]
    public void Test_GetName_Emote()
    {
        LineParser.ClearNameSet();
        // Fill nameSet with data
        LineParser.ParseLine("8/29 09:34:49.812  Finky says: I pare lat by nam zajelo wydobycie.");
        LineParser.ParseLine("2/1 15:20:04.045  Nelwo whispers: questy sie robi");
        LineParser.ParseLine("10/14 21:55:27.140  To Ysbail: o ja, 55 lvl :P");
        LineParser.ParseLine("10/8 10:28:17.225  [2. Trade] Fuztail: just write no manlets allowed ");
        LineParser.ParseLine("10/8 15:25:11.146  |Hchannel:GUILD|h[Guild]|h Swistaq-ArgentDawn: jest jakas meta/");
        LineParser.ParseLine("10/10 19:44:42.366  You receive loot: Spaulders of Kor'kron Fealty.");

        Line line1 = LineParser.ParseLine("10/29 16:50:22.515  Finky podrapal sie po glowie.");
        Line line2 = LineParser.ParseLine("11/7 16:54:10.281  Nelwo has gone offline.");
        Line line3 = LineParser.ParseLine("11/7 16:54:30.812  Nelwo has come online.");
        Line line4 = LineParser.ParseLine("5/6 23:29:51.792  Nelwo zaczal poprawiac siodlo");

        LineParser.PrepareEmoteParsing();
        line1 = LineParser.ParseEmote(line1);
        line2 = LineParser.ParseEmote(line2);
        line3 = LineParser.ParseEmote(line3);
        line4 = LineParser.ParseEmote(line4);

        Assert.AreEqual(line1.Name, "Finky");
        Assert.AreEqual(line2.Name, null);
        Assert.AreEqual(line3.Name, null);
        Assert.AreEqual(line4.Name, "Nelwo");
    }

    [TestMethod]
    public void Test_Emote_ThrowsException_OnNotPrepareEmoteParsing()
    {
        LineParser.ClearNameSet();
        // Fill nameSet with data
        LineParser.ParseLine("8/29 09:34:49.812  Finky says: I pare lat by nam zajelo wydobycie.");

        Line line1 = LineParser.ParseLine("10/29 16:50:22.515  Finky podrapal sie po glowie.");
        Assert.ThrowsException<Exception>( () => LineParser.ParseEmote(line1));
    }
}

