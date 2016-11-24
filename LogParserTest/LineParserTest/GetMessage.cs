using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogParser;
using System;

public partial class LineParserTest
{
    [TestMethod]
    public void Test_GetMessageType_Whisper()
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

    [TestMethod]
    public void Test_GetType_Party()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void Test_GetType_Say()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void Test_GetType_Yell()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void Test_GetType_Officer()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void Test_GetType_Guild()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void Test_GetType_Loot()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void Test_GetType_Achievement()
    {
        Assert.Fail();
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
