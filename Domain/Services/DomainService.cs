﻿using Domain.Entities;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public static class DomainService
    {
        public static Player FindPlayerById(Guid id)
        {
            var playerService = new PlayerService();
            return playerService.FindById(id);
        }

        public static Team FindTeamById(Guid id)
        {
            var teamService = new TeamService();
            return teamService.FindById(id);
        }

        public static Game FindGameById(Guid id)
        {
            var gameService = new GameService();
            return gameService.FindById(id);
        }

        public static IEnumerable<Game> GetAllGames()
        {
            var gameService = new GameService();
            return gameService.GetAll();
        }

        public static Match FindMatchById(Guid id)
        {
            var matchService = new MatchService();
            return matchService.FindById(id);
        }

        public static Series FindSeriesById(Guid id)
        {
            var seriesService = new SeriesService();
            return seriesService.FindById(id);
        }

        public static void AddSeriesToTeam(Guid seriesId, IEnumerable<Guid> matchIds,
            IEnumerable<Team> teams)
        {
            var series = FindSeriesById(seriesId);
            var matches = new List<Match>();
            foreach (var matchId in matchIds)
            {
                matches.Add(FindMatchById(matchId));
            }

            foreach (var team in teams)
            {
                var matchIdsRelevantForTeam = matches.Where(x => x.HomeTeamId.Equals(team.Id)
                    || x.AwayTeamId.Equals(team.Id)).Select(x => x.Id);

                team.AddSeries(series, matchIdsRelevantForTeam);
                AddSeriesToPlayers(series, team.Players);
            }
        }

        public static void AddSeriesToPlayers(Series series,
            IEnumerable<Player> players)
        {
            foreach (var player in players)
            {
                player.AddSeries(series);
            }
        }

        public static IEnumerable<Game> GetTeamsGamesInSeries(Guid teamId,
            Guid seriesId)
        {
            return
                from game in GetAllGames()
                where game.SeriesId == seriesId
                && (game.Protocol.HomeTeamId == teamId
                || game.Protocol.AwayTeamId == teamId)
                select game;
        }



        public static IEnumerable<Goal> GetPlayersGoalsInSeries(Guid playerId,
            Guid seriesId)
        {
            var playerGoals = new List<Goal>();
            var playerGoalsInGames = GetAllGames().Where(x => x.SeriesId == seriesId)
                .Select(x => x.Protocol.Goals.Where(y => y.PlayerId == playerId))
                .ToList();
            playerGoalsInGames.ForEach(x => playerGoals.AddRange(x));
            return playerGoals;
        }

        public static IEnumerable<Assist> GetPlayerAssistInSeries(Guid playerId, Guid seriesId)
        {
            var playerAssists = new List<Assist>();
            var playerAssistInGames = GetAllGames().Where(x => x.SeriesId == seriesId)
                .Select(x => x.Protocol.Assists.Where(y => y.PlayerId == playerId))
                .ToList();
            playerAssistInGames.ForEach(x => playerAssists.AddRange(x));
            return playerAssists;
        }

        public static IEnumerable<Card> GetPlayerCardsInSeries(Guid playerId, Guid seriesId)
        {
            var playerCards = new List<Card>();
            var playerCardsInGames = GetAllGames().Where(x => x.SeriesId == seriesId)
                .Select(x => x.Protocol.Cards.Where(y => y.PlayerId == playerId))
                .ToList();
            playerCardsInGames.ForEach(x => playerCards.AddRange(x));
            return playerCards;
        }
        public static IEnumerable<Penalty> GetPlayerPenaltiesInSeries(Guid playerId, Guid seriesId)
        {
            var playerPenalties = new List<Penalty>();
            var playerPenaltiesInGames = GetAllGames().Where(x => x.SeriesId == seriesId)
                .Select(x => x.Protocol.Penalties.Where(y => y.PlayerId == playerId))
                .ToList();
            playerPenaltiesInGames.ForEach(x => playerPenalties.AddRange(x));
            return playerPenalties;
        }

        public static IEnumerable<Game> GetPlayerPlayedGamesInSeries(Guid playerId, Guid seriesId)
        {

            var player = FindPlayerById(playerId);
            return GetAllGames().Where(game => game.HomeTeamId == player.TeamId
                                                               || game.AwayTeamId == player.TeamId
                                                               && game.SeriesId == seriesId).ToList();

        }
    }
}