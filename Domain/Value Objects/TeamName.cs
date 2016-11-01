﻿using Domain.Helper_Classes;
using System;

namespace Domain.Value_Objects
{
    public class TeamName : ValueObject
    {
        public string Value { get; }

        public TeamName(string teamName)
        {
            if (teamName.IsValidTeamName(true))
            {
                this.Value = teamName;
            }
            else
            {
                throw new FormatException("Not a valid teamname");
            }
        }

        public static bool TryParse(string teamName, out TeamName result)
        {
            try
            {
                result = new TeamName(teamName);
                return true;
            }
            catch (FormatException)
            {
                result = null;
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            var item = obj as TeamName;
            return item.Value == this.Value;
        }

        public override int GetHashCode()
        {
            return (this.Value).GetHashCode();
        }

        public static bool operator !=(TeamName TeamNameOne, TeamName TeamNameTwo)
        {
            return TeamNameOne.Value != TeamNameTwo.Value;
        }

        public static bool operator ==(TeamName TeamNameOne, TeamName TeamNameTwo)
        {
            return TeamNameOne.Value == TeamNameTwo.Value;
        }

        public override string ToString()
        {
            return $"{this.Value}";
        }
    }
}