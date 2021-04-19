using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace UnPack_My_Game.Models
{
    class MyButtonCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public string Name { get; }

        Action<object> _execteMethod;

        public MyButtonCommand(string nom,Action<object> execteMethod)
        {
            Name = nom;
            _execteMethod = execteMethod;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execteMethod(parameter);   
        }
    }
}
