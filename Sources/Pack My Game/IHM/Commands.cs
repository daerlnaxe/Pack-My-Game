using Pack_My_Game.Language;
using System.Windows.Input;

namespace Pack_My_Game.IHM
{
    static class Commands
    {
        public static readonly RoutedUICommand ProcessCommand =
            new RoutedUICommand(LanguageManager.Instance.Lang.Word_Process, "Process", typeof(Commands));

        public static readonly RoutedUICommand SubmitCommand =
            new RoutedUICommand(LanguageManager.Instance.Lang.Word_Submit, "Submit", typeof(Commands));
    }
}
