using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Pack_My_Game.Language
{
    /// <summary>
    /// Gestion des abonnements par les DataModule
    /// </summary>
    class LangueChangedEventManager : WeakEventManager
    {
        /// <summary>
        /// Manager unique, permet de mettre des fonctions statiques appelables de n'importe où
        /// </summary>
        /// <remarks>
        /// Initialize un nouveau si aucun n'existe
        /// </remarks>
        private static LangueChangedEventManager CurrentManager
        {
            get
            {
                Type tManager = typeof(LangueChangedEventManager);
                var evManager = (LangueChangedEventManager)GetCurrentManager(tManager);
                if (evManager == null)
                {
                    evManager = new LangueChangedEventManager();
                    SetCurrentManager(tManager, evManager);
                }
                return evManager;
            }
        }

        #region obligatoire (weakeventmanager)
        protected override void StartListening(object source)
        {
            var manager = (LanguageManager)source;

            // Diffuse le signal que la langue a changé
            manager.LanguageChanged += DeliverEvent;
        }


        protected override void StopListening(object source)
        {
            if (source is LanguageManager)
            {
                var manager = (LanguageManager)source;
                manager.LanguageChanged -= DeliverEvent;
            }
        }
        #endregion

        #region listener

        /// <summary>
        /// Ajoute un élément à l'écoute de l'event
        /// </summary>
        /// <param name="currentManager">Le manager qui émet les events quand la langue change</param>
        /// <param name="listener">L'élement qui écoute le manager</param>
        internal static void AddListener(LanguageManager langueManager, IWeakEventListener listener)
        {
            CurrentManager.ProtectedAddListener(langueManager, listener);
        }


        internal static void RemoveListener(LanguageManager langueManager, IWeakEventListener listener)
        {
            CurrentManager.ProtectedRemoveListener(langueManager, listener);
        }

        #endregion



    }
}
