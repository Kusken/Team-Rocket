﻿using Domain.Services;
using Domain.Value_Objects;
using System;
using System.Linq;

namespace Domain.Entities
{
    [Serializable]
    public class PlayerStats : ValueObject<PlayerStats>
    {
        private Guid seriesId;
        private Guid playerId;
        private Guid teamId;

        private PlayerEvents SeriesEvents
        {
            get
            {
                var player = DomainService.FindPlayerById(this.playerId);
                return player.SeriesEvents[this.seriesId];
            }
        }

        public string PlayerName
        {
            get
            {
                return DomainService.FindPlayerById(this.playerId).Name.ToString();
            }
        }

        public string TeamName
        {
            get
            {
                return DomainService.FindTeamById(this.teamId).Name.ToString();
            }
        }

        public int GoalCount
        {
            get
            {
                return this.SeriesEvents.Goals.Count();
            }
        }

        public int AssistCount
        {
            get
            {
                return this.SeriesEvents.Assists.Count();
            }
        }

        public int YellowCardCount
        {
            get
            {
                var cards = this.SeriesEvents.Cards;
                return cards.Where(x => x.CardType.Equals(CardType.Yellow)).Count();
            }
        }

        public int RedCardCount
        {
            get
            {
                var cards = this.SeriesEvents.Cards;
                return cards.Where(x => x.CardType.Equals(CardType.Red)).Count();
            }
        }

        public int PenaltyCount
        {
            get
            {
                return this.SeriesEvents.Penalties.Count();
            }
        }

        public int GamesPlayedCount
        {
            get
            {
                return this.SeriesEvents.Games.Count();
            }
        }

        public PlayerStats(Guid seriesId, Guid teamId, Guid playerId)
        {
            this.seriesId = seriesId;
            this.playerId = playerId;
            this.teamId = teamId;
        }
    }
}