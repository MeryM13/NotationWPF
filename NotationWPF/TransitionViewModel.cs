using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace NotationWPF
{
    internal class TransitionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private NumberTransition transition;
        IList<int> notations = new List<int>()
        { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22,
            23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36 };

        public TransitionViewModel()
        {
            transition = new NumberTransition();
            transition.NotationFrom = transition.NotationTo = 10;
        }

        public string NumberFrom
        {
            get { return transition.NumberFrom; }
            set
            {
                if (transition.NumberFrom != value)
                {
                    transition.NumberFrom = value;
                    OnPropertyChanged("NumberFrom");
                }
            }
        }

        public IList<int> Notations
        {
            get { return notations; }
            set { notations = value; }
        }

        public int NotationFrom
        {
            get { return transition.NotationFrom; }
            set
            {
                if (transition.NotationFrom != value)
                {
                    transition.NotationFrom = value;
                    OnPropertyChanged("NotationFrom");
                }
            }
        }

        public int NotationTo
        {
            get { return transition.NotationTo; }
            set
            {
                if (transition.NotationTo != value)
                {
                    transition.NotationTo = value;
                    OnPropertyChanged("NotationTo");
                }
            }
        }

        public string NumberTo
        {
            get { return transition.NumberTo; }
            set
            {
                if (transition.NumberTo != value)
                {
                    transition.NumberTo = value;
                    OnPropertyChanged("NumberTo");
                }
            }
        }

        private RelayCommand? clearCommand;
        public RelayCommand ClearCommand
        {
            get
            {
                return clearCommand ??= new RelayCommand(obj =>
                    {
                        NumberFrom = NumberTo = String.Empty;
                        NotationFrom = NotationTo = 10;
                    }, o => !string.IsNullOrEmpty(NumberFrom) || !string.IsNullOrEmpty(NumberTo)
                    || NotationTo != 10 || NotationFrom != 10);
            }
        }

        private RelayCommand? calculateCommand;
        public RelayCommand CalculateCommand
        {
            get
            {
                return calculateCommand ??= new RelayCommand(obj =>
                    {
                        try
                        {
                            NumberTo = NotationLibrary.Calculate.Calc(NumberFrom, NotationFrom, NotationTo);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }, o => !string.IsNullOrEmpty(NumberFrom));
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
