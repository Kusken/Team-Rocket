﻿using System;
using Domain.Entities;
using Domain.Value_Objects;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IExposableTeam
    {
        Guid Id { get; }
        TeamName Name { get; }
        IEnumerable<Guid> PlayerIds { get; }
        ArenaName ArenaName { get; }
        EmailAddress Email { get; }
        
    }
}