using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfBKStudio1.Commands;
using WpfBKStudio1.Models;

namespace WpfBKStudio1.ViewModels
{
    public class ReversePolishNotationViewModel : ViewModelBase
    {
        private string _originaExpretion;
        private string _reversePolishExpretion;
        private string _result;

        public string OriginalExpretion
        {
            get
            {
                return _originaExpretion;
            }
            set
            {
                _originaExpretion = value;
                OnPropertyChanged(nameof(OriginalExpretion));
            }
        }
        
        public string ReversePolishExpretion
        {
            get
            {
                return _reversePolishExpretion;
            }
            set
            {
                _reversePolishExpretion = value;
                OnPropertyChanged(nameof(ReversePolishExpretion));
            }
        }

        public string Result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        public ICommand CalculateCommand { get; }

        public ReversePolishNotationViewModel(ReversePN reversePolishNotation)
        {
            _originaExpretion = String.Empty;
            _reversePolishExpretion = String.Empty;
            _result = String.Empty;

            CalculateCommand = new CalculateCommand(reversePolishNotation, this);
        }
    }
}
