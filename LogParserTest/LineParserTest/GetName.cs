using LogParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

public partial class LineParserTest
{
    [TestMethod]
    public void Test_GetName_Whisper()
    {
        Line line1 = LineParser.ParseLine("9/12 20:03:37.387  To SomeDrek-MeRealm: this is. some,/ messAGE*>12$");
        Line line2 = LineParser.ParseLine("9/12 20:03:37.387  To SomeDrek: this is. some,/ messAGE*>12$");
        Line line3 = LineParser.ParseLine("9/12 20:03:37.387  SomeDrek-MeRealm whispers: może stad że wszyscy inni sa");
        Line line4 = LineParser.ParseLine("9/12 20:03:37.387  SomeDrek whispers: może stąd że wszyscy inni sa ęółśćźżń");

        Assert.AreEqual(line1.Name, "SomeDrek");
        Assert.AreEqual(line2.Name, "SomeDrek");
        Assert.AreEqual(line3.Name, "SomeDrek");
        Assert.AreEqual(line4.Name, "SomeDrek");
    }
    [TestMethod]
    public void Test_GetName_Raid()
    {
        Line line1 = LineParser.ParseLine("9/20 18:02:17.640  |Hchannel:raid|h[Raid]|h Selane: Caerthir, Aeltharian, trzyma");
        Line line2 = LineParser.ParseLine("9/20 18:02:17.640  |Hchannel:RAID|h[Raid]|h Selane: Caerthir, Aeltharian, trzyma");
        Line line3 = LineParser.ParseLine("9/20 18:02:17.640  |Hchannel:Raid|h[Raid]|h Selane: Caerthir, Aeltharian, trzyma");
        Line line4 = LineParser.ParseLine("10/16 16:52:29.743  Legacy Raid Difficulty set to 10 Player.");
        Line line5 = LineParser.ParseLine("10/16 16:52:29.743  Raid Difficulty set to 10 Player.");
        Line line6 = LineParser.ParseLine("10/11 18:32:26.171  Милэлса-Голдринн has left the raid group.");
        Line line7 = LineParser.ParseLine("10/11 18:32:28.179  Syslei-DunModr has joined the raid group.");
        Line line8 = LineParser.ParseLine("10/4 18:29:05.718  |Hchannel:raid|h[Raid Leader]|h Aeltharian: Pffm.");
        Line line9 = LineParser.ParseLine("10/8 18:29:03.760  [Raid Warning] Lìandra-Outland: WAIT FOR THE DAMN 2 TOWE");

        Assert.AreEqual(line1.Name, "Selane");
        Assert.AreEqual(line2.Name, "Selane");
        Assert.AreEqual(line3.Name, "Selane");
        Assert.AreEqual(line4.Name, null);
        Assert.AreEqual(line5.Name, null);
        Assert.AreEqual(line6.Name, "Милэлса");
        Assert.AreEqual(line7.Name, "Syslei");
        Assert.AreEqual(line8.Name, "Aeltharian");
        Assert.AreEqual(line9.Name, "Lìandra");
    }
    [TestMethod]
    public void Test_GetName_Party()
    {
        Line line1 = LineParser.ParseLine("10/4 20:19:00.031  Kerrin joins the party.");
        Line line2 = LineParser.ParseLine("10/4 20:24:24.984  Kerrin leaves the party.");
        Line line3 = LineParser.ParseLine("10/4 20:24:27.953  Kerrin has invited you to join a group.");
        Line line4 = LineParser.ParseLine("10/8 15:38:01.546  |Hchannel:Party|h[Party]|h Attan: No wiec planujemy ");
        Line line5 = LineParser.ParseLine("5/23 14:29:33.779  Your group has been disbanded.");
        Line line6 = LineParser.ParseLine("6/3 21:49:53.050  Dungeon Difficulty set to 5 Player (Heroic).");
        Line line7 = LineParser.ParseLine("6/3 21:50:30.500  Dungeon Difficulty set to 5 Player.");
        Line line8 = LineParser.ParseLine("6/4 12:40:26.254  You have invited Thorvald to join your group.");
        Line line9 = LineParser.ParseLine("10/8 18:18:49.072  Party converted to Raid");
        Line line10 = LineParser.ParseLine("6/13 15:38:00.500  Kaizoku is already in a group.");
        Line line11 = LineParser.ParseLine("1/25 18:27:06.617  |Hchannel:party|h[Party Leader]|h : Najlepiej dzis wieczorem.");
        Line line12 = LineParser.ParseLine("5/28 17:47:05.314  |Hchannel:party|h[Party Leader]|h Attan: Z ogniem...");

        Assert.AreEqual(line1.Name, "Kerrin");
        Assert.AreEqual(line2.Name, "Kerrin");
        Assert.AreEqual(line3.Name, "Kerrin");
        Assert.AreEqual(line4.Name, "Attan");
        Assert.AreEqual(line5.Name, null);
        Assert.AreEqual(line6.Name, null);
        Assert.AreEqual(line7.Name, null);
        Assert.AreEqual(line8.Name, "Thorvald");
        Assert.AreEqual(line9.Name, null);
        Assert.AreEqual(line10.Name, "Kaizoku");
        Assert.AreEqual(line11.Name, null);
        Assert.AreEqual(line12.Name, "Attan");

    }
    [TestMethod]
    public void Test_GetName_Say()
    {
        Line line1 = LineParser.ParseLine("12/20 13:58:36.250  Finky says: A jak wygladal?");
        Line line2 = LineParser.ParseLine("12/20 13:58:36.250  Finky-Krzeslo says: A jak wygladal?");

        Assert.AreEqual(line1.Name, "Finky");
        Assert.AreEqual(line2.Name, "Finky");
    }
    [TestMethod]
    public void Test_GetName_Yell()
    {
        Line line1 = LineParser.ParseLine("12/20 14:28:55.593  Kaizoku yells: Ktos chce sie napic?!");
        Line line2 = LineParser.ParseLine("12/20 14:28:55.593  Kaizoku-Realm yells: Ktos chce sie napic?!");

        Assert.AreEqual(line1.Name, "Kaizoku");
        Assert.AreEqual(line2.Name, "Kaizoku");
    }
    [TestMethod]
    public void Test_GetName_Officer()
    {
        Line line1 = LineParser.ParseLine("2/2 20:46:38.736  |Hchannel:o|h[Officer]|h Ceiren: tu ziom");

        Assert.AreEqual(line1.Name, "Ceiren");

    }
    [TestMethod]
    public void Test_GetName_Guild()
    {
        Line line1 = LineParser.ParseLine("6/18 19:03:08.609  |Hchannel:Guild|h[Guild]|h Saevron: najwyzej dojda w trakcie");

        Assert.AreEqual(line1.Name, "Saevron");
    }
    [TestMethod]
    public void Test_GetName_Loot()
    {
        Line line1 = LineParser.ParseLine("1/31 21:08:14.459  Looting changed to Group Loot.");
        Line line2 = LineParser.ParseLine("1/31 21:08:14.459  Loot threshold set to Uncommon.");
        Line line3 = LineParser.ParseLine("2/1 15:33:45.526  You receive loot: Runed Saronite Plate.");
        Line line4 = LineParser.ParseLine("2/6 14:04:16.810  Geril receives loot: Super Healing Potion.");
        Line line5 = LineParser.ParseLine("2/4 16:56:06.037  Your share of the loot is 26 Silver, 80 Copper.");
        Line line6 = LineParser.ParseLine("9/10 14:18:30.906  You loot 1 Silver, 32 Copper");

        Assert.AreEqual(line1.Name, null);
        Assert.AreEqual(line2.Name, null);
        Assert.AreEqual(line3.Name, null);
        Assert.AreEqual(line4.Name, "Geril");
        Assert.AreEqual(line5.Name, null);
        Assert.AreEqual(line6.Name, null);

    }
    [TestMethod]
    public void Test_GetName_Achievement()
    {
        Line line1 = LineParser.ParseLine("9/11 18:38:10.984  Cennes has earned the achievement Explore The Barrens!");

        Assert.AreEqual(line1.Name, "Cennes");

    }
    [TestMethod]
    public void Test_GetName_Instance()
    {
        Line line1 = LineParser.ParseLine("9/25 09:30:42.032  Kerrin is not in your instance.");
        Line line2 = LineParser.ParseLine("10/2 12:44:50.609  You are now saved to this instance");
        Line line3 = LineParser.ParseLine("10/8 11:47:15.914  |Hchannel:INSTANCE_CHAT|h[Instance]|h Ùberdøb-Stormrage: dont do that påls");
        Line line4 = LineParser.ParseLine("10/8 11:48:13.987  |Hchannel:INSTANCE_CHAT|h[Instance Leader]|h Grothil-ArgentDawn: thanks!");
        Line line5 = LineParser.ParseLine("10/8 12:08:23.059  Hazbuk-Bloodhoof has left the instance group.");
        Line line6 = LineParser.ParseLine("10/8 17:57:07.015  Unmindful-GrimBatol has joined the instance group.");
        Line line7 = LineParser.ParseLine("10/8 18:20:07.349  Varagoth-Silvermoon has joined the battle");

        Assert.AreEqual(line1.Name, "Kerrin");
        Assert.AreEqual(line2.Name, null);
        Assert.AreEqual(line3.Name, "Ùberdøb");
        Assert.AreEqual(line4.Name, "Grothil");
        Assert.AreEqual(line5.Name, "Hazbuk");
        Assert.AreEqual(line6.Name, "Unmindful");
        Assert.AreEqual(line7.Name, "Varagoth");
    }
    [TestMethod]
    public void Test_GetName_Roll()
    {
        Line line1 = LineParser.ParseLine("2/6 13:36:12.114  Greed Roll - 25 for Keleseth's Blade of Evocation by Rogrim");
        Line line2 = LineParser.ParseLine("2/6 14:03:44.930  Need Roll - 13 for Vrykul Shackles by Geril");
        Line line3 = LineParser.ParseLine("2/6 14:03:44.930  Geril won: Vrykul Shackles");
        Line line4 = LineParser.ParseLine("2/6 14:03:43.570  Mirkle has selected Need for: Vrykul Shackles");
        Line line5 = LineParser.ParseLine("2/6 14:03:41.452  Sylom has selected Greed for: Vrykul Shackles");
        Line line6 = LineParser.ParseLine("2/6 14:03:43.952  You have selected Greed for: Vrykul Shackles");
        Line line7 = LineParser.ParseLine("2/18 20:48:58.499  You have selected Need for: Ominous Dagger of the Elder");
        Line line8 = LineParser.ParseLine("9/20 18:59:11.296  Torhill rolls 39 (1-100)");

        Assert.AreEqual(line1.Name, null);
        Assert.AreEqual(line2.Name, null);
        Assert.AreEqual(line3.Name, "Geril");
        Assert.AreEqual(line4.Name, "Mirkle");
        Assert.AreEqual(line5.Name, "Sylom");
        Assert.AreEqual(line6.Name, null);
        Assert.AreEqual(line7.Name, null);
        Assert.AreEqual(line8.Name, "Torhill");
    }
    [TestMethod]
    public void Test_GetName_GenericChat()
    {
        Line line1 = LineParser.ParseLine("9/23 19:44:41.433  [5. iron] Rahgar joined channel.");
        Line line2 = LineParser.ParseLine("9/23 19:44:25.738  [7. xtensionxtooltip2] Gerric-RedemptorHominis: RPB1~CSCAN~32");
        Line line3 = LineParser.ParseLine("9/23 19:41:07.398  [5. iron] Muraadin-RedemptorHominis left channel.");
        Line line4 = LineParser.ParseLine("9/23 14:45:31.767  [4. Noir] Finky-RedemptorHominis: w tym wdzianku wygladas");
        Line line5 = LineParser.ParseLine("10/8 11:32:45.095  [1. General] Vanu: Could someone doing the storm drake assault ");
        Line line6 = LineParser.ParseLine("10/8 15:48:30.446  [2. Trade] Eowale: Alt-F4");
        Line line7 = LineParser.ParseLine("10/8 11:00:20.943  [3. LocalDefense] : ");
        Line line8 = LineParser.ParseLine("10/8 11:02:31.444  [4. LookingForGroup] Nhilila-ArgentDawn: Shut up mercy");

        Assert.AreEqual(line1.Name, "Rahgar");
        Assert.AreEqual(line1.Name, "Gerric");
        Assert.AreEqual(line1.Name, "Muraadin");
        Assert.AreEqual(line1.Name, "Finky");
        Assert.AreEqual(line1.Name, "Vanu");
        Assert.AreEqual(line1.Name, "Eowale");
        Assert.AreEqual(line1.Name, null);
        Assert.AreEqual(line1.Name, "Nhilila");
    }
}

