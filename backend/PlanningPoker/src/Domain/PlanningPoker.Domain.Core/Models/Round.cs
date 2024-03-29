﻿using PlanningPoker.Domain.Core.Interfaces;

namespace PlanningPoker.Domain.Core.Models
{
    public class Round : IModel
    {
        public Round() { }

        public Round(Guid id, Guid gameId, string name, bool active)
        {
            SetInitialValues(id, gameId, name, active);
        }

        public Round(Guid gameId, string name)
        {
            SetInitialValues(Guid.NewGuid(), gameId, name, true);
        }

        public void SetInitialValues(Guid id, Guid gameId, string name, bool active)
        {
            Id = id;
            GameId = gameId;
            Name = name?.Trim();
            Active = active;
        }


        public Guid Id { get; private set; }
        public Guid GameId { get; private set; }
        public Game Game { get; private set; }
        public string Name { get; private set; }
        public bool Active { get; private set; }


        public void Inactivate() => Active = false;
    }
}
