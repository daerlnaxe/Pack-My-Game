using DxTranslation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Pack_My_Game.Language
{
    internal class LangProvider : ILangProvider
    {
        public static string CurrentVersion => "1.0.0.0";

        private const string _FileName = "UnpackMyGame.Lang.json";

        /// <summary>
        /// Conteneur de langage
        /// </summary>
        public static LangContent LangContent { get; private set; }

        public IEnumerable<CultureInfo> Languages
        {
            get
            {
                var languages = new List<CultureInfo>();

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
                                languages.Add(new CultureInfo(l));

                            break;
                        }
                }

                return languages;
            }
        }

        public LangProvider()
        {
            throw new Exception("beee");

        }


        public void Initialize(string langTag)
        {
            string appFolder = AppDomain.CurrentDomain.BaseDirectory;

            CheckFile(appFolder, "en-US");
            CheckFile(appFolder, "fr-FR");

            /*
            // Si la valeur n'existe pas, 
            var language = Languages.FirstOrDefault(x => x.Name.Equals(langTag));

            if (language == null)
                language= Languages.FirstOrDefault(x => x.Name.Equals("en-US"));

            Thread.CurrentThread.CurrentUICulture = language;*/
        }



        public void ChangeLanguage(CultureInfo value)
        {
            string langFile = MakeFileLink(value.Name);

            //_LangContent = LangContent.Read(langFile);
            LangContent = LangContent.Load(langFile);
        }

        public object TranslateValue(string key)
        {


            if (LangContent == null)
                LangContent = LangContent.Default();

            var property = typeof(LangContent).GetProperty(key);
            if (property != null)
                return property.GetValue(LangContent);
            else
                return $"problem_{key}";
        }


        // Vérifie si le fichier existe et est à jour, sinon fait le nécessaire
        static void CheckFile(string appFolder, string langName)
        {
            LangContent langObj;
            string langFile = Path.Combine(appFolder, langName, _FileName);

            try
            {
                langObj = LangContent.Load(langFile);

                if (string.IsNullOrEmpty(langObj.Version) || !langObj.Version.Equals(Common.LangVersion))
                    throw new Exception("Bad Version");
            }
            catch
            {
                switch (langName)
                {
                    case "fr-FR":
                        langObj = LangContent.DefaultFrench();
                        break;
                    default:
                        langObj = LangContent.Default();
                        break;
                }

                langObj.Save(langFile);
            }

        }

        private string MakeFileLink(string value)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, value, _FileName);
        }


    }
}
