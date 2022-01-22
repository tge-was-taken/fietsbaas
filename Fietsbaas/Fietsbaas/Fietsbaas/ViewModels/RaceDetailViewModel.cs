﻿using Fietsbaas.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fietsbaas.ViewModels
{
    public class RaceDetailViewModel : BaseDetailViewModel
    {
        private string name;
        private string description;
        private string stageName; 
        private ObservableCollection<Stage> stages;

        public string Name 
        { 
            get => name; 
            set => SetProperty( ref name, value ); 
        }

        public string Description
        { 
            get => description; 
            set => SetProperty( ref description, value );
        }

        public string StageName
        {
            get => stageName;
            set => SetProperty( ref stageName, value );
        }

        public ObservableCollection<Stage> Stages
        {
            get => stages;
            set => SetProperty( ref stages, value );
        }

        public Command TeamCommand { get; set; }

        public RaceDetailViewModel()
        {
            Title = "Stages";
            TeamCommand = new Command( ExecuteTeamCommand );
        }

        private async void ExecuteTeamCommand( object obj )
        {
            await Shell.Current.GoToAsync( $"race/team/index?raceid={Id}" );
        }

        protected override void OnLoad( int id )
        {
            var race = Db.Races.Find( id );
            if ( race != null )
            {
                Name = race.Name;
                Description = race.Description;
                stages = new ObservableCollection<Stage>(Db.Stages.Where(x => x.RaceId == id ));
            }
        }
    }
}
