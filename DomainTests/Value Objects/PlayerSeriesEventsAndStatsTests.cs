﻿using Domain.Entities;
using Domain.Value_Objects;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DomainTests.Value_Objects
{
    [TestClass]
    public class PlayerSeriesEventsAndStatsTests
    {
        private DummySeries dummySeries;
        private Team teamOne;
        private Player playerOne;

        [TestInitialize]
        public void Init()
        {
            this.dummySeries = new DummySeries();
            this.teamOne = this.dummySeries.DummyTeams.DummyTeamOne;
            this.playerOne = this.dummySeries.DummyTeams.DummyTeamOne.Players.ElementAt(0);
        }

        [TestMethod]
        public void PlayerSeriesEventsIsUpdateWhenNewGoalIsAdded()
        {
            Assert.IsNotNull(this.playerOne.SeriesEvents[this.dummySeries.SeriesDummy.Id]);
            var playerOneGoalsPreAdd =
                this.playerOne.PresentableSeriesEvents[this.dummySeries.SeriesDummy.Id].Goals.Count();
            this.dummySeries.DummyGames.GameOne.Protocol.Goals.Add(new Goal(new MatchMinute(14), this.playerOne.TeamId,
                this.playerOne.Id));
            var playerOneGoalsPostAdd = this.playerOne.SeriesEvents[this.dummySeries.SeriesDummy.Id].Goals.Count();
            Assert.IsTrue(playerOneGoalsPostAdd - playerOneGoalsPreAdd == 1);
        }

        [TestMethod]
        public void PlayerSeriesEventsIsUpdatedWhenAssistIsAdded()
        {
            Assert.IsNotNull(this.playerOne.SeriesEvents[this.dummySeries.SeriesDummy.Id]);
            var playerOneAssistsPreAdd =
                this.playerOne.PresentableSeriesEvents[this.dummySeries.SeriesDummy.Id].Assists.Count();
            this.dummySeries.DummyGames.GameThree.Protocol.Assists.Add(new Assist(new MatchMinute(57), this.playerOne.Id));
            var playerOneAssistsPostAdd = this.playerOne.SeriesEvents[this.dummySeries.SeriesDummy.Id].Assists.Count();
            Assert.IsTrue(playerOneAssistsPostAdd - playerOneAssistsPreAdd == 1);
        }

        [TestMethod]
        public void PlayerSeriesEventsIsUpdatedWhenYellowCardIsAdded()
        {
            Assert.IsNotNull(this.playerOne.SeriesEvents[this.dummySeries.SeriesDummy.Id]);
            var playerOneCardsPreAdd =
                this.playerOne.PresentableSeriesEvents[this.dummySeries.SeriesDummy.Id].Cards.Count();
            this.dummySeries.DummyGames.GameThree.Protocol.Cards.Add(new Card(new MatchMinute(82), this.playerOne.Id, CardType.Yellow));
            var playerOneCardsPostAdd = this.playerOne.SeriesEvents[this.dummySeries.SeriesDummy.Id].Cards.Count();
            Assert.IsTrue(playerOneCardsPostAdd - playerOneCardsPreAdd == 1);
        }

        [TestMethod]
        public void PlayerSeriesEventsIsUpdatedWhenRedCardIsAdded()
        {
            Assert.IsNotNull(this.playerOne.SeriesEvents[this.dummySeries.SeriesDummy.Id]);
            var playerOneCardsPreAdd =
                this.playerOne.PresentableSeriesEvents[this.dummySeries.SeriesDummy.Id].Cards.Count();
            this.dummySeries.DummyGames.GameThree.Protocol.Cards.Add(new Card(new MatchMinute(82), this.playerOne.Id, CardType.Red));
            var playerOneCardsPostAdd = this.playerOne.SeriesEvents[this.dummySeries.SeriesDummy.Id].Cards.Count();
            Assert.IsTrue(playerOneCardsPostAdd - playerOneCardsPreAdd == 1);
        }

        [TestMethod]
        public void PlayerSeriesEventsIsUpdatedWhenPenaltyIsAdded()
        {
            Assert.IsNotNull(this.playerOne.SeriesEvents[this.dummySeries.SeriesDummy.Id]);
            var playerOnePenaltiesPreAdd =
                this.playerOne.PresentableSeriesEvents[this.dummySeries.SeriesDummy.Id].Penalties.Count();
            this.dummySeries.DummyGames.GameThree.Protocol.Penalties.Add(new Penalty(new MatchMinute(89), this.playerOne.Id));
            var playerOnePenaltiesPostAdd =
                this.playerOne.SeriesEvents[this.dummySeries.SeriesDummy.Id].Penalties.Count();
            Assert.IsTrue(playerOnePenaltiesPostAdd - playerOnePenaltiesPreAdd == 1);
        }

        [TestMethod]
        public void PlayerSeriesStatsReflectsGoalsAddedToEvents()
        {
            var goalsPreAdd = this.playerOne.SeriesStats[this.dummySeries.SeriesDummy.Id].GoalCount;
            this.PlayerSeriesEventsIsUpdateWhenNewGoalIsAdded();
            var goalsPostAdd = this.playerOne.SeriesStats[this.dummySeries.SeriesDummy.Id].GoalCount;
            Assert.IsTrue(goalsPostAdd - goalsPreAdd == 1);
        }

        [TestMethod]
        public void PlayerSeriesStatsReflectsAssistsAddedToEvents()
        {
            var assistsPreAdd = this.playerOne.SeriesStats[this.dummySeries.SeriesDummy.Id].AssistCount;
            this.PlayerSeriesEventsIsUpdatedWhenAssistIsAdded();
            var assistsPostAdd = this.playerOne.SeriesStats[this.dummySeries.SeriesDummy.Id].AssistCount;
            Assert.IsTrue(assistsPostAdd - assistsPreAdd == 1);
        }

        [TestMethod]
        public void PlayerSeriesStatsReflectsYellowCardsAddedToEvents()
        {
            var yellowCardsPreAdd = this.playerOne.SeriesStats[this.dummySeries.SeriesDummy.Id].YellowCardCount;
            this.PlayerSeriesEventsIsUpdatedWhenYellowCardIsAdded();
            var yellowCardsPostAdd = this.playerOne.SeriesStats[this.dummySeries.SeriesDummy.Id].YellowCardCount;
            Assert.IsTrue(yellowCardsPostAdd - yellowCardsPreAdd == 1);
        }

        [TestMethod]
        public void PlayerSeriesStatsReflectsRedCardsAddedToEvents()
        {
            var redCardsPreAdd = this.playerOne.SeriesStats[this.dummySeries.SeriesDummy.Id].RedCardCount;
            this.PlayerSeriesEventsIsUpdatedWhenRedCardIsAdded();
            var redCardsPostAdd = this.playerOne.SeriesStats[this.dummySeries.SeriesDummy.Id].RedCardCount;
            Assert.IsTrue(redCardsPostAdd - redCardsPreAdd == 1);
        }

        [TestMethod]
        public void PlayerSeriesStatsReflectsPenaltiesAddedToEvents()
        {
            var penaltiesPreAdd = this.playerOne.SeriesStats[this.dummySeries.SeriesDummy.Id].PenaltyCount;
            this.PlayerSeriesEventsIsUpdatedWhenPenaltyIsAdded();
            var penaltiesPostAdd = this.playerOne.SeriesStats[this.dummySeries.SeriesDummy.Id].PenaltyCount;
            Assert.IsTrue(penaltiesPostAdd - penaltiesPreAdd == 1);
        }

        [TestMethod]
        public void PlayerSeriesStatsGamesReflectsGameEvents()
        {
            var gameEvents = this.playerOne.SeriesEvents[this.dummySeries.SeriesDummy.Id].Games.Count();
            var gameStat = this.playerOne.SeriesStats[this.dummySeries.SeriesDummy.Id].GamesPlayedCount;
            Assert.AreEqual(gameEvents, gameStat);
        }
    }
}