using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinMeeting.ViewModel;

namespace XamarinMeeting.Model
{
   public class Meeting: BaseNotifyPropertyChanged
    {
        bool _isSelected;
        string _title, _description;
        DateTime _date;
        string _organiser;
        string _interviewer;
        string _attendee;
        string _email;
        string _grade;




        public ICommand SelectedCommand { get; private set; }



        public Meeting()
        {
            SelectedCommand = new Command(() => {
                IsSelected = !IsSelected;
            });
        }



        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedImage));
            }
        }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }



      /*  public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }*/



        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }




        public string OrganiserName
        {
            get => _organiser;
            set
            {
                _organiser = value;
                OnPropertyChanged();
            }
        }


        public string InterviewerName
        {
            get => _interviewer;
            set
            {
                _interviewer = value;
                OnPropertyChanged();
            }
        }


        public string AttendeeName
        {
            get => _attendee;
            set
            {
                _attendee = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Grade
        {
            get => _grade;
            set
            {
                _grade = value;
                OnPropertyChanged();
            }
        }


        public string SelectedImage => IsSelected ? "ic_check" : "ic_uncheck";
    }
}
