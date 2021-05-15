using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace UnPack_My_Game.Language
{
    /// <summary>
    /// A pour charge de contenir la valeur et changer quand la langue change
    /// </summary>
    /// <remarks>
    /// Les weaks events permettent de s'abonner sans compromettre le garbage collector
    /// </remarks>
    class DataModule : IWeakEventListener, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Clé de liaison pour la valeur à traduire
        /// </summary>
        private string _key;


        /// <summary>
        /// Renvoie la valeur correspondant à la clé
        /// </summary>
        public object Value => LanguageManager.Instance.GetValue(_key);


        /// <summary>
        /// Initialise avec une clé correspondant à la valeur à traduire 
        /// Abonnement à l'event changement de langue
        /// </summary>
        /// <param name="key">The key.</param>
        public DataModule(string key)
        {
            _key = key;
            LangueChangedEventManager.AddListener( LanguageManager.Instance, this);
        }

        /// <summary>
        /// Signale la mise à jour de la valeur quand un changement de langue se produit
        /// </summary>
        /// <param name="managerType"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(LangueChangedEventManager))
            {
                // On signale pour que l'UI mette à jour
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));

                // on intercepte si ça concerne le manager de langue
                return true;
            }
            return false;
        }

        // ---

        /// <summary>
        /// On enlève l'écoute
        /// </summary>
        ~DataModule()
        {
            LangueChangedEventManager.RemoveListener(LanguageManager.Instance, this);
        }
    }
}
