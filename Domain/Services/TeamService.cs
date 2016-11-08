﻿using Domain.Entities;
using Domain.Repositories;
using System.Collections.Generic;

namespace Domain.Services
{
    public class TeamService
    {
        private readonly TeamRepository repository = TeamRepository.instance;

        public void AddTeam(Team team)
        {
            this.repository.Add(team);
        }

        public IEnumerable<Team> GetAll()
        {
            return this.repository.GetAll();
        }
    }
}