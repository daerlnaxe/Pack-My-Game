using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Common_PMG.Models
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
        protected Dictionary<string, List<string>> _Errors = new Dictionary<string, List<string>>();

        public virtual bool HasErrors => _Errors.Any();

        protected void Add_Error(string error, [CallerMemberName] string propertyName = null)
        {
            if (!_Errors.ContainsKey(propertyName))
                _Errors.Add(propertyName, new List<string>());

            _Errors[propertyName].Add(error);

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected void Remove_Error(string error, [CallerMemberName] string propertyName = null)
        {
            if (!_Errors.ContainsKey(propertyName))
                return;

            _Errors[propertyName].Remove(error);

            if (!_Errors[propertyName].Any())
                _Errors.Remove(propertyName);

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }


        protected void Test_NullValue(object value, [CallerMemberName] string propertyName = null)
        {
            string errSentence = "Null";

            Remove_Error(errSentence, propertyName);

            if (value == null)
                Add_Error(errSentence, propertyName);
        }

        /// <summary>
        /// Test string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        protected void Test_NullValue(string value, [CallerMemberName] string propertyName = null)
        {
            string errSentence = "Null";

            Remove_Error(errSentence, propertyName);

            if (string.IsNullOrEmpty(value))
                Add_Error(errSentence, propertyName);
        }

        /// <summary>
        /// Test collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Elements"></param>
        /// <param name="propertyName"></param>
        protected void Test_HasElement<T>(IEnumerable<T> Elements, [CallerMemberName] string propertyName = null) where T : class
        {
            string errSentence = "No element";
            Remove_Error(errSentence, propertyName);

            if (Elements.Count() <= 0)
                Add_Error(errSentence, propertyName);
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
