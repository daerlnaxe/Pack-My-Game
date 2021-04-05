using LaunchBox_XML.Container;
using LaunchBox_XML.Container.Game;
using Pack_My_Game.Cont;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pack_My_Game.IHM
{
    /// <summary>
    /// Affiche 
    /// </summary>
    public partial class W_Games : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Language Lang => Common.ObjectLang;


        public ObservableCollection<ShortGame> SelectedGames { get; set; }

        public W_Games()
        {
            InitializeComponent();
            DataContext = this;
        }


        private void Game_WheelMouse(object sender, MouseWheelEventArgs e)
        {
            sv.ScrollToVerticalOffset(sv.VerticalOffset - e.Delta);

        }

        private void Remove_Game(object sender, RoutedEventArgs e)
        {
            var game = (ShortGame)((Button)sender).Tag;
            SelectedGames.Remove(game);
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedGames)));
        }

        private void Can_Valide(object sender, CanExecuteRoutedEventArgs e)
        {
            if(SelectedGames != null)
            e.CanExecute = SelectedGames.Count > 0;
        }

        private void Exec_Valide(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
