using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace UnPack_My_Game.Language
{
    /*
     * Thread.CurrentThread.CurrentUICulture: langue actuelle de l'os
     * CurrentCulture pour les dates et les devises
     * CurrentUICulture 
     */
    static class LanguageManager
    {
        private const string _FileName = "UnpackMyGame.Lang.json";
        private const string _Version = "1.0.0.0";


        public static LangProvider Lang;

        #region events
        /// <summary>
        /// Liste des manager d'events
        /// </summary>
        private static HashSet<EventHandler> _UICultureChangedHandlers = new HashSet<EventHandler>();

        /// <summary>
        /// (Des)Abonnement à l'event
        /// </summary>
        public static event EventHandler UICultureChanged
        {
            add
            {
                _UICultureChangedHandlers.Add(value);
            }
            remove
            {
                _UICultureChangedHandlers.Remove(value);
            }
        }

        /// <summary>
        /// Signale à tous les abonnés que la culture a changé
        /// </summary>
        private static void OnUICultureChanged()
        {
            Debug.WriteLine(CurrentLanguage.Name);

            foreach (EventHandler handler in _UICultureChangedHandlers)
            {
                handler(typeof(LanguageManager), EventArgs.Empty);
            }
        }



        #endregion

        public static CultureInfo CurrentLanguage
        {

            get { return Thread.CurrentThread.CurrentUICulture; }
            set
            {
                if (value != Thread.CurrentThread.CurrentUICulture)
                {
                    Thread.CurrentThread.CurrentUICulture = value;
                    ChangeLanguage(value);
                }
            }
        }

        public static List<CultureInfo> Langues = new List<CultureInfo>();

        /// <summary>
        /// Initialisation du module de langage
        /// </summary>
        internal static void Init()
        {
            string appFolder = AppDomain.CurrentDomain.BaseDirectory;

            CheckFile(appFolder, "en-US");
            CheckFile(appFolder, "fr-FR");

            List<string> supportedLanguages = new List<string>();

            // Liste des localisations
            var localZ = CultureInfo.GetCultures(CultureTypes.AllCultures).Select(x => x.Name);

            // On vérifie les langages supportés
            foreach (string d in Directory.EnumerateDirectories(AppDomain.CurrentDomain.BaseDirectory))
            {
                string folder = Path.GetFileName(d);
                foreach (var l in localZ)
                    if (l.Equals(folder))
                    {
                        supportedLanguages.Add(l);
                        Langues.Add(new CultureInfo(l));
                        break;
                    }
            }

            ChangeLanguage(CurrentLanguage);

            // Vérifie si le fichier existe et est à jour, sinon fait le nécessaire
            static void CheckFile(string appFolder, string langName)
            {
                LangProvider langObj;
                string langFile = Path.Combine(appFolder, langName, _FileName);

                try
                {
                    langObj = LangProvider.Read(langFile);

                    if (string.IsNullOrEmpty(langObj.Version) || !langObj.Version.Equals(Common.LangVersion))
                        throw new Exception("Bad Version");
                }
                catch
                {
                    switch (langName)
                    {
                        case "fr-FR":
                            langObj = LangProvider.DefaultFrench();
                            break;
                        default:
                            langObj = LangProvider.Default();
                            break;
                    }

                    langObj.Save(langFile);
                }

            }
        }



        private static void ChangeLanguage(CultureInfo value)
        {
            string langFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, value.Name, _FileName);
            if(!File.Exists(langFile))
                langFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "en-US", _FileName);

            Lang = LangProvider.Read(langFile);

            OnUICultureChanged();

        }
          
        internal static object Getvalue(string key)
        {
            if (Lang == null)
                Lang = LangProvider.Default();

            return typeof(LangProvider).GetProperty(key).GetValue(Lang);
        }
    }
}
