using Pack_My_Game.Language;
using System.Windows.Input;
using static Pack_My_Game.Language.LanguageManager;


namespace Pack_My_Game.IHM
{
    static class Commands
    {
        public static readonly RoutedUICommand ProcessCommand =
                new RoutedUICommand(Instance.Lang.Word_Process, "ProcessCmd", typeof(Commands));

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
        public static readonly RoutedUICommand OpenCheatCmd =
                new RoutedUICommand(Instance.Lang.Word_Open, "OpenCheatCmd", typeof(Commands));

        public static readonly RoutedUICommand DeleteCheatCmd =
                new RoutedUICommand(Instance.Lang.Word_Delete, "DeleteCheatCmd", typeof(Commands));

        // --- Manuals
        public static readonly RoutedUICommand OpenManualCmd =
                new RoutedUICommand(Instance.Lang.Word_Open, "OpenManualCmd", typeof(Commands));

        public static readonly RoutedUICommand DeleteManualCmd =
                new RoutedUICommand(Instance.Lang.Word_Delete, "DeleteManualCmd", typeof(Commands));

        // --- Musics
        public static readonly RoutedUICommand OpenMusicCmd =
                new RoutedUICommand(Instance.Lang.Word_Open, "OpenMusicCmd", typeof(Commands));

        public static readonly RoutedUICommand DeleteMusicCmd =
                new RoutedUICommand(Instance.Lang.Word_Delete, "DeleteMusicCmd", typeof(Commands));
        
        // --- Videos
        public static readonly RoutedUICommand OpenVideoCmd =
                new RoutedUICommand(Instance.Lang.Word_Open, "OpenVideoCmd", typeof(Commands));

        public static readonly RoutedUICommand DeleteVideoCmd = 
                new RoutedUICommand(Instance.Lang.Word_Delete, "DeleteVideoCmd", typeof(Commands));

    }
}
