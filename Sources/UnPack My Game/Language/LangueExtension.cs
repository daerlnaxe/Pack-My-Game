using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Xml.Linq;

namespace UnPack_My_Game.Language
{
    /// <summary>
    /// Ajoute une fonction de traduction au xaml
    /// </summary>
    class LangueExtension : MarkupExtension
    {
        public string _Key;
        private WeakReference targetObjectRef;
        private object targetProperty;


        public LangueExtension(string key)
        {
            _Key = key;
        }


        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(_Key))
                throw new InvalidOperationException("The key property cannot be null or empty");

            // On garde une référence sur l'object qui a appelé et sur sa cible
            // (weak ne bloque pas le garbage collector)
            // Cela va permettre de mettre à jour si la langue change
            IProvideValueTarget target = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            if (target != null)
            {
                if (target.TargetObject != null && target.TargetProperty != null)
                {
                    this.targetObjectRef = new WeakReference(target.TargetObject);
                    this.targetProperty = target.TargetProperty;
                   LanguageManager.UICultureChanged += LanguageManager_UICultureChanged;
                }
            }

            // On passe la valeur
            return LanguageManager.Getvalue(_Key);
        }

        private void LanguageManager_UICultureChanged(object sender, EventArgs e)
        {
            // Si l'objet cible n'a pas été ramassé par le garbage collector on se désabonne
            if (!targetObjectRef.IsAlive)
            {
                LanguageManager.UICultureChanged -= LanguageManager_UICultureChanged;
                return;
            }

            // On récupère la nouvelle valeur
            object value = LanguageManager.Getvalue(_Key);

            // On assigne la nouvelle valeur
            if (targetProperty is DependencyProperty)
            {
                DependencyObject obj = targetObjectRef.Target as DependencyObject;
                DependencyProperty prop = targetProperty as DependencyProperty;
                obj.SetValue(prop, value);
            }
            else
            {
                object obj = targetObjectRef.Target;
                PropertyInfo prop = targetProperty as PropertyInfo;
                prop.SetValue(obj, value, null);
            }
        }
    }
}
