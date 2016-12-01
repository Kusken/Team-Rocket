﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using FootballManager.Admin.Extensions;
using FootballManager.Admin.Utility;
using FootballManager.Admin.View;

namespace FootballManager.Admin.ViewModel
{
    public class PlayerViewModel : ViewModelBase
    {
        private ObservableCollection<IExposablePlayer> players;
        private PlayerService playerService;
        private TeamService teamService;
        private ICommand openPlayerAddViewCommand;
        private ICommand deletePlayerCommand;
        private string searchText;

        public PlayerViewModel()
        {
            this.playerService = new PlayerService();
            this.teamService = new TeamService();

            this.LoadData();     

            Messenger.Default.Register<Player>(this, this.OnPlayerObjReceived);
        }



        #region Properties
        public ICommand OpenPlayerAddViewCommand
        {
            get
            {
                if (this.openPlayerAddViewCommand == null)
                {
                    this.openPlayerAddViewCommand = new RelayCommand(this.OpenPlayerAddView);
                }
                return this.openPlayerAddViewCommand;
            }
        }

        public ICommand DeletePlayerCommand
        {
            get
            {
                if (this.deletePlayerCommand == null)
                {
                    this.deletePlayerCommand = new RelayCommand(this.DeletePlayer);
                }
                return this.deletePlayerCommand;
            }            
        }

        public ObservableCollection<IExposablePlayer> Players
        {
            get { return players; }
            set
            {
                players = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged();
                FilterData();
            }
        }


        #endregion

        #region Methods

        public void FilterData()
        {
            Players = playerService.Search(SearchText).ToObservableCollection();
        }

        private void DeletePlayer(object obj)
        {
            IList playersSelectedIList = (IList)obj;
            List<Player> playersSelectedList = playersSelectedIList.Cast<Player>().ToList();

            foreach (var player in playersSelectedList)
            {
                this.players.Remove(player);
            }
        }

        private void OpenPlayerAddView(object obj)
        {
            var playerAddView = new PlayerAddView();
            playerAddView.ShowDialog();
        }

        private void OnPlayerObjReceived(Player player)
        {
            this.playerService.Add(player);
            this.LoadData();
        }

        public void LoadData()
        {
            var exposedPlayers = playerService.GetAllExposablePlayers();

            Players = exposedPlayers.ToObservableCollection();
        }
        #endregion

        #region Combobox population
        public IEnumerable<PlayerPosition> PlayerPositions
        {
            get { return Enum.GetValues(typeof(PlayerPosition)).Cast<PlayerPosition>(); }
        }

        public IEnumerable<PlayerStatus> PlayerStatuses
        {
            get { return Enum.GetValues(typeof(PlayerStatus)).Cast<PlayerStatus>(); }

        }

        public IEnumerable<string> TeamNames
        {
            get { return this.teamService.GetAllTeams().Select(x => x.Name.Value); } 
        }
        #endregion
    }
}
