﻿using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;

namespace Domain.Services
{
    public class PlayerService
    {
        private readonly PlayerRepository repository = PlayerRepository.instance;
        private readonly IEnumerable<Player> allPlayers;

        public PlayerService()
        {
            allPlayers = repository.GetAll();
        }

        public void Add(Player player)
        {
            this.repository.Add(player);
        }

        public IEnumerable<IPresentablePlayer> GetTopScorers(Guid seriesId)
        {
            return allPlayers.ToList().OrderByDescending(p => p.SeriesStats[seriesId]).Take(15);
        }

        public IEnumerable<IPresentablePlayer> GetTopAssists(Guid seriesId)
        {
            return GetAll().OrderByDescending(p => p.SeriesStats[seriesId].AssistCount).Take(15);
        }

        public IEnumerable<IPresentablePlayer> GetTopYellowCards(Guid seriesId)
        {
            return GetAll().OrderByDescending(p => p.SeriesStats[seriesId].YellowCardCount).Take(5);
        }

        public IEnumerable<IPresentablePlayer> GetTopRedCards(Guid seriesId)
        {
            return allPlayers.ToList().OrderByDescending(p => p.SeriesStats[seriesId].RedCardCount).Take(5);
        }
        
        public IEnumerable<Player> GetAll()
        {
            return this.repository.GetAll();
        }

        public Player FindById(Guid playerId)
        {
            return allPlayers.ToList().Find(p => p.Id.Equals(playerId));
        }

        public IEnumerable<IPresentablePlayer> FindPlayer(string searchText, StringComparison comp)
        {
            var result = allPlayers.Where(x =>
                x.Name.ToString().Contains(searchText, comp) ||
                x.DateOfBirth.Value.ToString().Contains(searchText, comp));

            return result;
        }

        public string GetPlayerName(Guid playerId)
        {
            var result = allPlayers.Where(x => x.Id == playerId).Select(x => x.Name.ToString()).First();
            return result;
        }

        public Guid GetPlayerTeamId(Guid playerId)
        {
            var result = allPlayers.Where(x => x.Id == playerId).Select(x => x.TeamId).First();
            return result;
        }

        //public IEnumerable<List<Guid>> GetPlayerGamesPlayedIds(Guid playerId)
        //{
        //    var involvedInEvents = allPlayers.Where(x => x.Id == playerId).Select(x => x.Events);
        //    var result = involvedInEvents.Select(x => x.Games);
        //    return result;
        //}

        //public int GetPlayerTotalYellowCards(Guid playerId)
        //{
        //    var playerStats = allPlayers.Where(x => x.Id == playerId).Select(x => x.Stats);
        //    var result = playerStats.Select(x => x.YellowCardCount).First();
        //    return result;
        //}

        //public int GetPlayerTotalRedCards(Guid playerId)
        //{
        //    var playerStats = allPlayers.Where(x => x.Id == playerId).Select(x => x.Stats);
        //    var result = playerStats.Select(x => x.RedCardCount).First();
        //    return result;
        //}

        //public int GetPlayerTotalGoals(Guid playerId)
        //{
        //    var playerStats = allPlayers.Where(x => x.Id == playerId).Select(x => x.Stats);
        //    var result = playerStats.Select(x => x.GoalCount).First();
        //    return result;
        //}

        //public int GetPlayerTotalAssists(Guid playerId)
        //{
        //    var playerStats = allPlayers.Where(x => x.Id == playerId).Select(x => x.Stats);
        //    var result = playerStats.Select(x => x.AssistCount).First();
        //    return result;
        //}

        //public int GetPlayerTotalPenalties(Guid playerId)
        //{
        //    var playerStats = allPlayers.Where(x => x.Id == playerId).Select(x => x.Stats);
        //    var result = playerStats.Select(x => x.AssistCount).First();
        //    return result;
        //}
    }
}