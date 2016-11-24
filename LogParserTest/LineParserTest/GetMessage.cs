using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogParser;
using System;

public partial class LineParserTest
{
    [TestMethod]
    public void Test_GetType_Whisper()
    {
        Line line1 = LineParser.ParseLine("9/12 20:03:37.387  To SomeDrek-MeRealm: this is. some,/ messAGE*>12$");
        Line line2 = LineParser.ParseLine("9/12 20:03:37.387  To SomeDrek: this is. some,/ messAGE*>12$");
        Line line3 = LineParser.ParseLine("9/12 20:03:37.387  SomeDrek-MeRealm whispers: może stad że wszyscy inni sa");
        Line line4 = LineParser.ParseLine("9/12 20:03:37.387  SomeDrek whispers: może stąd że wszyscy inni sa ęółśćźżń");

        Assert.AreEqual(MessageType.Whisper, line1.Type);
        Assert.AreEqual(MessageType.Whisper, line2.Type);
        Assert.AreEqual(MessageType.Whisper, line3.Type);
        Assert.AreEqual(MessageType.Whisper, line4.Type);
    }

    [TestMethod]
    public void Test_GetType_Raid()
    {
        Line line1 = LineParser.ParseLine("9/20 18:02:17.640  |Hchannel:raid|h[Raid]|h Selane: Caerthir, Aeltharian, trzyma");
        Line line2 = LineParser.ParseLine("9/20 18:02:17.640  |Hchannel:RAID|h[Raid]|h Selane: Caerthir, Aeltharian, trzyma");
        Line line3 = LineParser.ParseLine("9/20 18:02:17.640  |Hchannel:Raid|h[Raid]|h Selane: Caerthir, Aeltharian, trzyma");
        Line line4 = LineParser.ParseLine("10/16 16:52:29.743  Legacy Raid Difficulty set to 10 Player.");
        Line line5 = LineParser.ParseLine("10/16 16:52:29.743  Raid Difficulty set to 10 Player.");
        Line line6 = LineParser.ParseLine("10/11 18:32:26.171  Милэлса-Голдринн has left the raid group.");
        Line line7 = LineParser.ParseLine("10/11 18:32:28.179  Syslei-DunModr has joined the raid group.");
        Line line8 = LineParser.ParseLine("10/4 18:29:05.718  |Hchannel:raid|h[Raid Leader]|h Aeltharian: Pffm.");

        Assert.AreEqual(MessageType.Raid, line1.Type);
        Assert.AreEqual(MessageType.Raid, line2.Type);
        Assert.AreEqual(MessageType.Raid, line3.Type);
        Assert.AreEqual(MessageType.Raid, line4.Type);
        Assert.AreEqual(MessageType.Raid, line5.Type);
        Assert.AreEqual(MessageType.Raid, line6.Type);
        Assert.AreEqual(MessageType.Raid, line7.Type);
        Assert.AreEqual(MessageType.Raid, line8.Type);
    }

    //[TestMethod]
    //public void Test_GetType_Party()
    //{
    //    Assert.Fail();
    //}

    [TestMethod]
    public void Test_GetType_Say()
    {
        Line line1 = LineParser.ParseLine("12/20 13:58:36.250  Finky says: A jak wygladal?");

        Assert.AreEqual(MessageType.Say, line1.Type);
    }

    [TestMethod]
    public void Test_GetType_Yell()
    {
        Line line1 = LineParser.ParseLine("12/20 14:28:55.593  Kaizoku yells: Ktos chce sie napic?!");

        Assert.AreEqual(MessageType.Yell, line1.Type);
    }

    [TestMethod]
    public void Test_GetType_Officer()
    {
        Line line1 = LineParser.ParseLine("2/2 20:46:38.736  |Hchannel:o|h[Officer]|h Ceiren: tu ziom");
        Line line2 = LineParser.ParseLine("2/2 20:46:38.736  |Hchannel:o|h[officer]|h Ceiren: tu ziom");
        Line line3 = LineParser.ParseLine("2/2 20:46:38.736  |Hchannel:o|h[OFFICER]|h Ceiren: tu ziom");

        Assert.AreEqual(MessageType.Officer, line1.Type);
        Assert.AreEqual(MessageType.Officer, line2.Type);
        Assert.AreEqual(MessageType.Officer, line3.Type);
    }

    [TestMethod]
    public void Test_GetType_Guild()
    {
        Line line1 = LineParser.ParseLine("6/18 19:03:08.609  |Hchannel:Guild|h[Guild]|h Saevron: najwyzej dojda w trakcie");

        Assert.AreEqual(MessageType.Guild, line1.Type);
    }

    //[TestMethod]
    //public void Test_GetType_Loot()
    //{
    //    Assert.Fail();
    //}

    [TestMethod]
    public void Test_GetType_Achievement()
    {
        Line line1 = LineParser.ParseLine("9/11 18:38:10.984  Cennes has earned the achievement Explore The Barrens!");

        Assert.AreEqual(MessageType.Achievement, line1.Type);
    }

    [TestMethod]
    public void Test_GetType_System()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void Test_GetType_Instance()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void Test_GetType_CommandOutput()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void Test_GetType_Roll()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void Test_GetType_GenericChat()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void Test_GetType_GeneralChat()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void Test_GetType_CustomChat()
    {
        Assert.Fail();
    }
}
