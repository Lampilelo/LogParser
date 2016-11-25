using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogParser;
using System;
using System.Collections.Generic;

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
        List<Line> lines = new List<Line>(9);
        lines.Add(LineParser.ParseLine("9/20 18:02:17.640  |Hchannel:raid|h[Raid]|h Selane: Caerthir, Aeltharian, trzyma"));
        lines.Add(LineParser.ParseLine("9/20 18:02:17.640  |Hchannel:RAID|h[Raid]|h Selane: Caerthir, Aeltharian, trzyma"));
        lines.Add(LineParser.ParseLine("9/20 18:02:17.640  |Hchannel:Raid|h[Raid]|h Selane: Caerthir, Aeltharian, trzyma"));
        lines.Add(LineParser.ParseLine("10/16 16:52:29.743  Legacy Raid Difficulty set to 10 Player."));
        lines.Add(LineParser.ParseLine("10/16 16:52:29.743  Raid Difficulty set to 10 Player."));
        lines.Add(LineParser.ParseLine("10/11 18:32:26.171  Милэлса-Голдринн has left the raid group."));
        lines.Add(LineParser.ParseLine("10/11 18:32:28.179  Syslei-DunModr has joined the raid group."));
        lines.Add(LineParser.ParseLine("10/4 18:29:05.718  |Hchannel:raid|h[Raid Leader]|h Aeltharian: Pffm."));
        lines.Add(LineParser.ParseLine("10/8 18:29:03.760  [Raid Warning] Lìandra-Outland: WAIT FOR THE DAMN 2 TOWE"));

        foreach (Line line in lines)
        {
            Assert.AreEqual(MessageType.Raid, line.Type);
        }
    }

    [TestMethod]
    public void Test_GetType_Party()
    {
        List<Line> lines = new List<Line>(11);
        lines.Add(LineParser.ParseLine("10/4 20:19:00.031  Kerrin joins the party."));
        lines.Add(LineParser.ParseLine("10/4 20:24:24.984  Kerrin leaves the party."));
        lines.Add(LineParser.ParseLine("10/4 20:24:27.953  Kerrin has invited you to join a group."));
        lines.Add(LineParser.ParseLine("10/8 15:38:01.546  |Hchannel:Party|h[Party]|h Attan: No wiec planujemy "));
        lines.Add(LineParser.ParseLine("5/23 14:29:33.779  Your group has been disbanded."));
        lines.Add(LineParser.ParseLine("6/3 21:49:53.050  Dungeon Difficulty set to 5 Player (Heroic)."));
        lines.Add(LineParser.ParseLine("6/3 21:50:30.500  Dungeon Difficulty set to 5 Player."));
        lines.Add(LineParser.ParseLine("6/4 12:40:26.254  You have invited Thorvald to join your group."));
        lines.Add(LineParser.ParseLine("10/8 18:18:49.072  Party converted to Raid"));
        lines.Add(LineParser.ParseLine("6/13 15:38:00.500  Kaizoku is already in a group."));
        lines.Add(LineParser.ParseLine("1/25 18:27:06.617  |Hchannel:party|h[Party Leader]|h : Najlepiej dzis wieczorem."));

        foreach (Line line in lines)
        {
            Assert.AreEqual(MessageType.Party, line.Type);
        }
    }

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

    [TestMethod]
    public void Test_GetType_Loot()
    {
        List<Line> lines = new List<Line>(6);
        lines.Add(LineParser.ParseLine("1/31 21:08:14.459  Looting changed to Group Loot."));
        lines.Add(LineParser.ParseLine("1/31 21:08:14.459  Loot threshold set to Uncommon."));
        lines.Add(LineParser.ParseLine("2/1 15:33:45.526  You receive loot: Runed Saronite Plate."));
        lines.Add(LineParser.ParseLine("2/6 14:04:16.810  Geril receives loot: Super Healing Potion."));
        lines.Add(LineParser.ParseLine("2/4 16:56:06.037  Your share of the loot is 26 Silver, 80 Copper."));
        lines.Add(LineParser.ParseLine("9/10 14:18:30.906  You loot 1 Silver, 32 Copper"));


        foreach (Line line in lines)
        {
            Assert.AreEqual(MessageType.Loot, line.Type);
        }
    }

    [TestMethod]
    public void Test_GetType_Achievement()
    {
        Line line1 = LineParser.ParseLine("9/11 18:38:10.984  Cennes has earned the achievement Explore The Barrens!");

        Assert.AreEqual(MessageType.Achievement, line1.Type);
    }

    [TestMethod]
    public void Test_GetType_Instance()
    {
        List<Line> lines = new List<Line>(7);
        lines.Add(LineParser.ParseLine("9/25 09:30:42.032  Kerrin is not in your instance."));
        lines.Add(LineParser.ParseLine("10/2 12:44:50.609  You are now saved to this instance"));
        lines.Add(LineParser.ParseLine("10/8 11:47:15.914  |Hchannel:INSTANCE_CHAT|h[Instance]|h Ùberdøb-Stormrage: dont do that påls"));
        lines.Add(LineParser.ParseLine("10/8 11:48:13.987  |Hchannel:INSTANCE_CHAT|h[Instance Leader]|h Grothil-ArgentDawn: thanks!"));
        lines.Add(LineParser.ParseLine("10/8 12:08:23.059  Hazbuk-Bloodhoof has left the instance group."));
        lines.Add(LineParser.ParseLine("10/8 17:57:07.015  Unmindful-GrimBatol has joined the instance group."));
        lines.Add(LineParser.ParseLine("10/8 18:20:07.349  Varagoth-Silvermoon has joined the battle"));

        foreach (Line line in lines)
        {
            Assert.AreEqual(MessageType.Instance, line.Type);
        }
    }

    [TestMethod]
    public void Test_GetType_Roll()
    {
        List<Line> lines = new List<Line>(7);
        lines.Add(LineParser.ParseLine("2/6 13:36:12.114  Greed Roll - 25 for Keleseth's Blade of Evocation by Rogrim"));
        lines.Add(LineParser.ParseLine("2/6 14:03:44.930  Need Roll - 13 for Vrykul Shackles by Geril"));
        lines.Add(LineParser.ParseLine("2/6 14:03:44.930  Geril won: Vrykul Shackles"));
        lines.Add(LineParser.ParseLine("2/6 14:03:43.570  Mirkle has selected Need for: Vrykul Shackles"));
        lines.Add(LineParser.ParseLine("2/6 14:03:41.452  Sylom has selected Greed for: Vrykul Shackles"));
        lines.Add(LineParser.ParseLine("2/6 14:03:43.952  You have selected Greed for: Vrykul Shackles"));
        lines.Add(LineParser.ParseLine("2/18 20:48:58.499  You have selected Need for: Ominous Dagger of the Elder"));

        foreach (Line line in lines)
        {
            Assert.AreEqual(MessageType.Roll, line.Type);
        }
    }

    [TestMethod]
    public void Test_GetType_GenericChat()
    {
        List<Line> lines = new List<Line>(8);
        lines.Add(LineParser.ParseLine("9/23 19:44:41.433  [5. iron] Rahgar joined channel."));
        lines.Add(LineParser.ParseLine("9/23 19:44:25.738  [7. xtensionxtooltip2] Gerric-RedemptorHominis: RPB1~CSCAN~32"));
        lines.Add(LineParser.ParseLine("9/23 19:41:07.398  [5. iron] Muraadin-RedemptorHominis left channel."));
        lines.Add(LineParser.ParseLine("9/23 14:45:31.767  [4. Noir] Finky-RedemptorHominis: w tym wdzianku wygladas"));
        lines.Add(LineParser.ParseLine("10/8 11:32:45.095  [1. General] Vanu: Could someone doing the storm drake assault "));
        lines.Add(LineParser.ParseLine("10/8 15:48:30.446  [2. Trade] Eowale: Alt-F4"));
        lines.Add(LineParser.ParseLine("10/8 11:00:20.943  [3. LocalDefense] : "));
        lines.Add(LineParser.ParseLine("10/8 11:02:31.444  [4. LookingForGroup] Nhilila-ArgentDawn: Shut up mercy"));

        foreach (Line line in lines)
        {
            Assert.AreEqual(MessageType.GenericChat, line.Type);
        }
    }

    [TestMethod]
    public void Test_GetType_GeneralChat()
    {
        throw new NotImplementedException();
    }

    [TestMethod]
    public void Test_GetType_CustomChat()
    {
        throw new NotImplementedException();
    }

    [TestMethod]
    public void Test_GetType_System()
    {
        throw new NotImplementedException();
    }

    //[TestMethod]
    //public void Test_GetType_CommandOutput()
    //{
    //    throw new NotImplementedException();
    //}

}
