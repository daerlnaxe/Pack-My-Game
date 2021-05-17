using Pack_My_Game.Language;
using System.Windows.Input;
using static Pack_My_Game.Language.LanguageManager;


namespace Pack_My_Game.IHM
{
    static class Commands
    {
        public static readonly RoutedUICommand ProcessCommand =
                new RoutedUICommand(Instance.Lang.Word_Process, "ProcessCmd", typeof(Commands));

        public static readonly RoutedUICommand SelectCommand =
            new RoutedUICommand(Instance.Lang.Word_Select, "SelectCmd", typeof(Commands));

        public static readonly RoutedUICommand SubmitCommand =
                new RoutedUICommand(Instance.Lang.Word_Submit, "SubmitCmd", typeof(Commands));

        public static readonly RoutedUICommand ResetCommand =
                new RoutedUICommand(Instance.Lang.Word_Reset, "ResetCmd", typeof(Commands));

        public static readonly RoutedUICommand DeleteCmd =
                new RoutedUICommand(Instance.Lang.Word_Delete, "DeleteCmd", typeof(Commands));

        // --- recherche
        public static readonly RoutedUICommand SearchCmd =
                new RoutedUICommand(Instance.Lang.Word_Search, "SearchCmd", typeof(Commands));


        // --- Cheats
        public static readonly RoutedUICommand AddCheatCmd =
                new RoutedUICommand(Instance.Lang.Word_Add, "AddCheadCmd", typeof(Commands));

        public static readonly RoutedUICommand OpenCheatCmd =
                new RoutedUICommand(Instance.Lang.Word_Open, "OpenCheatCmd", typeof(Commands));

        /*public static readonly RoutedUICommand OpenSrc2CheatCmd =
                new RoutedUICommand(Instance.Lang.Word_Open, "OpenSrc2CheatCmd", typeof(Commands));*/

        public static readonly RoutedUICommand DeleteCheatCmd =
                new RoutedUICommand(Instance.Lang.Word_Delete, "DeleteCheatCmd", typeof(Commands));

        // --- Jeux
        public static readonly RoutedUICommand AddGameCmd =
                new RoutedUICommand(Instance.Lang.Word_Add, "AddGameCmd", typeof(Commands));

        // --- Images
        public static readonly RoutedUICommand AddImageCmd =
                new RoutedUICommand(Instance.Lang.Word_Add, "AddImageCmd", typeof(Commands));
              
        public static readonly RoutedUICommand OpenImageCmd =
                new RoutedUICommand(Instance.Lang.Word_Open, "OpenImageCmd", typeof(Commands));

        public static readonly RoutedUICommand DeleteImageCmd =
                new RoutedUICommand(Instance.Lang.Word_Delete, "DeleteImageCmd", typeof(Commands));

        // --- Manuals
        public static readonly RoutedUICommand AddManualCmd =
        new RoutedUICommand(Instance.Lang.Word_Add, "AddManualCmd", typeof(Commands));

        public static readonly RoutedUICommand OpenManualCmd =
                new RoutedUICommand(Instance.Lang.Word_Open, "OpenManualCmd", typeof(Commands));

        /*public static readonly RoutedUICommand OpenManualCmd =
        new RoutedUICommand(Instance.Lang.Word_Open, "OpenSrc2ManualCmd", typeof(Commands));*/

        public static readonly RoutedUICommand DeleteManualCmd =
                new RoutedUICommand(Instance.Lang.Word_Delete, "DeleteManualCmd", typeof(Commands));

        // --- Musics
        public static readonly RoutedUICommand AddMusicCmd =
                new RoutedUICommand(Instance.Lang.Word_Add, "AddMusicCmd", typeof(Commands));

        public static readonly RoutedUICommand OpenMusicCmd =
                new RoutedUICommand(Instance.Lang.Word_Open, "OpenMusicCmd", typeof(Commands));

/*        public static readonly RoutedUICommand OpenSrc2MusicCmd =
        new RoutedUICommand(Instance.Lang.Word_Open, "OpenSrc2MusicCmd", typeof(Commands));*/

        public static readonly RoutedUICommand DeleteMusicCmd =
                new RoutedUICommand(Instance.Lang.Word_Delete, "DeleteMusicCmd", typeof(Commands));

        // --- Videos
        public static readonly RoutedUICommand AddVideoCmd =
                new RoutedUICommand(Instance.Lang.Word_Add, "AddVideoCmd", typeof(Commands));

        public static readonly RoutedUICommand OpenVideoCmd =
                new RoutedUICommand(Instance.Lang.Word_Open, "OpenVideoCmd", typeof(Commands));

        /*public static readonly RoutedUICommand OpenSrc2VideoCmd =
        new RoutedUICommand(Instance.Lang.Word_Open, "OpenSrc2VideoCmd", typeof(Commands));*/

        public static readonly RoutedUICommand DeleteVideoCmd = 
                new RoutedUICommand(Instance.Lang.Word_Delete, "DeleteVideoCmd", typeof(Commands));

    }
}
