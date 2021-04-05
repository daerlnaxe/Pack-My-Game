using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Pack_My_Game.Resources;

namespace Pack_My_Game.Models
{

    public delegate void BasicHandler(object sender);
    public class A_Err : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Errors
        private Dictionary<string, List<string>> _Errors = new Dictionary<string, List<string>>();

        public virtual bool HasErrors => _Errors.Any();

        protected void Add_Error(string error, [CallerMemberName] string propertyName = null)
        {
            if (!_Errors.ContainsKey(propertyName))
                _Errors.Add(propertyName, new List<string>());

            _Errors[propertyName].Add(error);

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected void Remove_Error([CallerMemberName] string propertyName = null)
        {
            _Errors.Remove(propertyName);
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected void Test_NullValue(string value, [CallerMemberName] string propertyName = null)
        {
            Remove_Error(propertyName);

            if (string.IsNullOrEmpty(value))
                Add_Error(Lang.NullValue, propertyName);
        }

        protected void Test_HasElement<T>(IEnumerable<T> Elements, [CallerMemberName] string propertyName = null )where T: class
        {
            Remove_Error(propertyName);

            if (Elements.Count() <= 0)
                Add_Error(Lang.NoElem, propertyName);
        }


        public virtual IEnumerable GetErrors(string propertyName)
        {
            if (_Errors.ContainsKey(propertyName))
                return _Errors[propertyName];

            return null;
        }
        #endregion Errors
    }
}
