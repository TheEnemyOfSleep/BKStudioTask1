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
        private string _originaExpression;
        private string _reversePolishExpression;
        private string _result;

        public string OriginalExpression
        {
            get
            {
                return _originaExpression;
            }
            set
            {
                _originaExpression = value;
                OnPropertyChanged(nameof(OriginalExpression));
            }
        }
        
        public string ReversePolishExpression
        {
            get
            {
                return _reversePolishExpression;
            }
            set
            {
                _reversePolishExpression = value;
                OnPropertyChanged(nameof(ReversePolishExpression));
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
            _originaExpression = String.Empty;
            _reversePolishExpression = String.Empty;
            _result = String.Empty;

            CalculateCommand = new CalculateCommand(reversePolishNotation, this);
        }
    }
}
