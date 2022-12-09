using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfBKStudio1.Models;
using WpfBKStudio1.ViewModels;

namespace WpfBKStudio1.Commands
{
    public class CalculateCommand: CommandBase
    {
        private ReversePN _reversePN;
        private ReversePolishNotationViewModel _reversePolishNotationViewModel;

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_reversePolishNotationViewModel.OriginalExpression) &&
                   base.CanExecute(parameter);
        }

        public CalculateCommand(ReversePN reversePN, ReversePolishNotationViewModel reversePolishNotationViewModel)
        {
            _reversePN = reversePN;
            _reversePolishNotationViewModel = reversePolishNotationViewModel;
            _reversePolishNotationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object parameter)
        {
            try
            {
                _reversePolishNotationViewModel.ReversePolishExpression = _reversePN.GetReversePolishNotation(_reversePolishNotationViewModel.OriginalExpression);
                _reversePolishNotationViewModel.Result = _reversePN.Calculate();
            } catch (FormatException e)
            {
                MessageBox.Show(e.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            } catch (Exception)
            {
                MessageBox.Show("Unknown problem", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ReversePolishNotationViewModel.OriginalExpression))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
