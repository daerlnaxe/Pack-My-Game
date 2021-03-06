﻿using System;
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
    public class LanguageManager
    {
        public event EventHandler LanguageChanged;

        /// <summary>
        /// permet d'appeler le manager actuel de n'importe où
        /// </summary>
        private static LanguageManager _CurrentManager;
        public static LanguageManager Instance
        {
            get 
            {
                if (_CurrentManager == null)
                {
                    _CurrentManager = new LanguageManager();
                    _CurrentManager.CurrentLanguage = Thread.CurrentThread.CurrentCulture;
                }

                return _CurrentManager;
            }  
        }

        private const string _FileName = "UnpackMyGame.Lang.json";
        //private const string _Version = "1.0.0.0";


        public LangProvider Lang;


        public CultureInfo CurrentLanguage
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

        public static List<CultureInfo> Langues { get; set; } = new List<CultureInfo>();



        /// <summary>
        /// Initialisation du module de langage
        /// </summary>
        internal static void Init()
        {
            string appFolder = AppDomain.CurrentDomain.BaseDirectory;

            CheckFile(appFolder, "en-US");
            CheckFile(appFolder, "fr-FR");

            Langues.Clear();
            // Liste des localisations
            var localZ = CultureInfo.GetCultures(CultureTypes.AllCultures).Select(x => x.Name);

            // On vérifie les langages supportés
            foreach (string d in Directory.EnumerateDirectories(AppDomain.CurrentDomain.BaseDirectory))
            {
                string folder = Path.GetFileName(d);
                foreach (var l in localZ)
                    if (l.Equals(folder))
                    {
                        if (File.Exists(MakeFileLink(l)))
                            Langues.Add(new CultureInfo(l));

                        break;
                    }
            }



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
                            langObj = LangProvider.MakeDefault();
                            break;
                    }

                    langObj.Save(langFile);
                }

            }
        }

        internal void ChangeLanguage(string langTag)
        {
            // Si la valeur n'existe pas, 
            var language = Langues.FirstOrDefault(x => x.Name.Equals(langTag));

            if (language == null)
                language = Langues.FirstOrDefault(x => x.Name.Equals("en-US"));

            CurrentLanguage = language;
        }


        private void ChangeLanguage(CultureInfo value)
        {
            string langFile = MakeFileLink(value.Name);
      /*      if (!File.Exists(langFile))
                langFile = MakeFileLink("en-US");*/

            Lang = LangProvider.Read(langFile);

            LanguageChanged?.Invoke(this, EventArgs.Empty);

        }

        internal object GetValue(string key)
        {
            if (Lang == null)
                Lang = LangProvider.MakeDefault();

            var property = typeof(LangProvider).GetProperty(key);
            if (property != null)
                return property.GetValue(Lang);
            else
                return $"problem_{key}";
        }

        private static string MakeFileLink(string value)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, value, _FileName);
        }
    }
}
