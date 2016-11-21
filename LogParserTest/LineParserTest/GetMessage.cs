using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogParser;
using System;

public partial class LineParserTest
{
    [TestMethod]
    public void Test_GetType_Whisper()
    {
        MessageType type1 = LineParser.GetType("To SomeDrek-MeRealm: this is. some,/ messAGE*>12$");
        MessageType type2 = LineParser.GetType("To SomeDrek: this is. some,/ messAGE*>12$");
        MessageType type3 = LineParser.GetType("SomeDrek-MeRealm whispers: może stad że wszyscy inni sa");
        MessageType type4 = LineParser.GetType("SomeDrek whispers: może stąd że wszyscy inni sa ęółśćźżń");

        Assert.AreEqual(MessageType.Whisper, type1);
        Assert.AreEqual(MessageType.Whisper, type2);
        Assert.AreEqual(MessageType.Whisper, type3);
        Assert.AreEqual(MessageType.Whisper, type4);
    }

    [TestMethod]
    public void Test_GetType_Raid()
    {
        Assert.Fail();
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
