﻿using Domain.Entities;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainTests.Entities.Tests
{
    [TestClass]
    public class ShirtNumberTests
    {
        private PlayerService playerService;
        private TeamService teamService;
        private IEnumerable<Player> players;
        private IEnumerable<Team> teams;
        private Player playerOne;
        private Player playerTwo;
        private Team teamOne;
        private Team teamTwo;

        [TestInitialize]
        public void Init()
        {
            this.playerService = new PlayerService();
            this.teamService = new TeamService();
            this.players = this.playerService.GetAllPlayers();
            this.teams = this.teamService.GetAll();
            this.playerOne = this.players.FirstOrDefault();
            this.playerTwo = this.players.ElementAt(1);
            this.teamOne = this.teams.FirstOrDefault();
            this.teamTwo = this.teams.ElementAt(1);
            this.playerOne.TeamId = this.teamOne.Id;
            this.teamOne.AddPlayerId(this.playerOne.Id);
            this.playerTwo.TeamId = this.teamOne.Id;
            this.teamOne.AddPlayerId(this.playerTwo.Id);
        }

        [TestMethod]
        public void ShirtNumberIsEqualToValidEntry()
        {
            this.playerOne.ShirtNumber = new ShirtNumber(this.playerOne.TeamId, 9);
            this.playerTwo.ShirtNumber = new ShirtNumber(this.playerTwo.TeamId, 20);
            Assert.IsTrue(this.playerOne.ShirtNumber.Value == 9);
            Assert.IsTrue(this.playerTwo.ShirtNumber.Value == 20);
        }

        [TestMethod]
        [ExpectedException(typeof(ShirtNumberAlreadyInUseException))]
        public void ShirtNumberCanThrowAlreadyInUseExceptionIfAlreadyUsed()
        {
            this.playerOne.ShirtNumber = new ShirtNumber(this.playerOne.TeamId, 9);
            this.playerTwo.ShirtNumber = new ShirtNumber(this.playerTwo.TeamId, 9);
        }

        [TestMethod]
        public void ShirtNumberCanBeAssignedAfterBeingUnAssigned()
        {
            this.playerOne.ShirtNumber = new ShirtNumber(this.playerOne.TeamId, 55);
            Assert.IsTrue(this.playerOne.ShirtNumber.Value == 55);
            this.teamOne.RemovePlayerId(this.playerOne.Id);
            this.playerOne.TeamId = Guid.Empty;
            this.playerTwo.ShirtNumber = new ShirtNumber(this.playerTwo.TeamId, 55);
            Assert.IsTrue(this.playerTwo.ShirtNumber.Value == 55);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ShirtNumberThrowsIndexOutOfRangeExceptionIfNumberIsGreaterThanNinteyNine()
        {
            this.playerOne.ShirtNumber = new ShirtNumber(this.playerOne.TeamId, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ShirtNumberThrowsIndexOutOfRangeExceptionIfNumberIsLessThanZero()
        {
            this.playerOne.ShirtNumber = new ShirtNumber(this.playerOne.TeamId, -1);
        }

        [TestMethod]
        public void ShirtNumberGetPropertyCanReturnNull()
        {
            this.playerOne.ShirtNumber = new ShirtNumber(this.teamOne.Id, null);
            Assert.IsNull(this.playerOne.ShirtNumber.Value);
        }

        [TestMethod]
        public void ShirtNumberTeamIdCanChangeWhenPlayerTeamIdChange()
        {
            this.playerTwo.ShirtNumber = new ShirtNumber(this.teamOne.Id, 7);
            Assert.IsTrue(this.playerTwo.ShirtNumber.Value == 7);
            this.playerTwo.TeamId = this.teamTwo.Id;
            Assert.IsTrue(this.playerTwo.ShirtNumber.Value == null);
            this.playerTwo.ShirtNumber = new ShirtNumber(9);
            Assert.IsTrue(this.playerTwo.ShirtNumber.Value == 9);
        }
    }
}