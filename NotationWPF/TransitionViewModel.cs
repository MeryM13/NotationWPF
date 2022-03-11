using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace NotationWPF
{
    internal class TransitionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private NumberTransition transition;

        public TransitionViewModel()
        {
            transition = new NumberTransition();
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

        public string NotationFrom
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

        public string NotationTo
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
                        NumberFrom = NumberTo = NotationFrom = NotationTo = String.Empty;
                    }, o => !string.IsNullOrEmpty(NumberFrom) || !string.IsNullOrEmpty(NumberTo) 
                    || !string.IsNullOrEmpty(NotationFrom) || !string.IsNullOrEmpty(NotationTo));
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
                            NumberTo = NotationLibrary.Calculate.Calc(NumberFrom, Int32.Parse(NotationFrom), Int32.Parse(NotationTo));
                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show(ex.Message);
                        }
                    }, o => !string.IsNullOrEmpty(NumberFrom) && !string.IsNullOrEmpty(NotationTo) && !string.IsNullOrEmpty(NotationFrom));
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
