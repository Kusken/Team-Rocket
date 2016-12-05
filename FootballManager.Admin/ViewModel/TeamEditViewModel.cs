﻿using Domain.Interfaces;
using Domain.Services;
using Domain.Value_Objects;
using FootballManager.Admin.Utility;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FootballManager.Admin.ViewModel
{
    public class TeamEditViewModel : ViewModelBase
    {
        private TeamService teamService;
        private IExposableTeam selectedTeam;
        private TeamName teamName;
        private ArenaName arenaName;
        private EmailAddress email;

        public TeamEditViewModel()
        {
            this.teamService = new TeamService();
            Messenger.Default.Register<IExposableTeam>(this, this.OnTeamObjectRecieved);
            this.SaveTeamChangesCommand = new RelayCommand(this.EditTeam);
        }

        public ICommand SaveTeamChangesCommand { get; }

        public IExposableTeam SelectedTeam
        {
            get { return this.selectedTeam; }
            set
            {
                if (this.selectedTeam != value)
                {
                    this.selectedTeam = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public TeamName TeamName
        {
            get { return this.teamName ?? new TeamName("Not Available"); }
            set
            {
                if (this.teamName != value)
                {
                    this.teamName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ArenaName ArenaName
        {
            get { return this.arenaName ?? new ArenaName("Not Available"); }
            set
            {
                if (this.arenaName != value)
                {
                    this.arenaName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public EmailAddress Email
        {
            get { return this.email ?? new EmailAddress("not_assigned@na.org"); }
            set
            {
                if (this.email != value)
                {
                    this.email = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private void OnTeamObjectRecieved(IExposableTeam team)
        {
            this.SelectedTeam = team;
            this.TeamName = this.SelectedTeam.Name;
            this.ArenaName = this.SelectedTeam.ArenaName;
            this.Email = this.SelectedTeam.Email;
        }

        private void EditTeam(object obj)
        {
            this.SelectedTeam.Name = this.teamName;
            this.SelectedTeam.ArenaName = this.arenaName;
            this.SelectedTeam.Email = this.email;
            this.teamService.Add(this.SelectedTeam);
            this.CloseDialog();
        }

        private void CloseDialog()
        {
            var window = Application.Current.Windows.OfType<Window>().
                FirstOrDefault(x => x.Name == "TeamEditViewDialog");
            window?.Close();
        }
    }
}