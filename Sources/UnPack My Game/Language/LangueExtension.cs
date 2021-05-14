using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Xml.Linq;

namespace UnPack_My_Game.Language
{
    /// <summary>
    /// Ajoute une fonction de traduction au xaml
    /// </summary>
    public class LangueExtension : MarkupExtension
    {
        private string _Key;

        public LangueExtension(string key)
        {
            _Key = key;
        }


        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(_Key))
                throw new InvalidOperationException("The key property cannot be null or empty");

            // --- On crée une nouvelle liaison autonome
            var binding = new Binding("Value")
            {
                Source = new DataModule(_Key),
            };
            return binding.ProvideValue(serviceProvider);
        }

    }
}

