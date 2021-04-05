using System.Windows;
using System.ComponentModel;
using Pack_My_Game.Models;
using System.Windows.Input;

namespace Pack_My_Game.IHM
{
    /// <summary>
    /// Logique d'interaction pour GameNAme.xaml
    /// </summary>
    public partial class W_ChooseName : Window
    {
        public M_ChooseName Model { get; set; }

        public static readonly RoutedUICommand SubmitCmd = new RoutedUICommand("Submit", "SubmitCmd", typeof(W_ChooseName));

        public W_ChooseName()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
        }

        private void Can_Submit(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Model != null)
                e.CanExecute = !Model.HasErrors;
        }

        private void Exec_Submit(object sender, ExecutedRoutedEventArgs e)
        {
            if (Model.Verif())
            {
                DialogResult = true;
                this.Close();

            }
        }
    }
}
