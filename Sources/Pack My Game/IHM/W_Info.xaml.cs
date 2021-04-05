using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pack_My_Game.IHM
{
    /// <summary>
    /// Logique d'interaction pour W_Info.xaml
    /// </summary>
    public partial class W_Info : Window
    {
        public string Message { get; set; }

        public string Texteuh { get; set; }

        public W_Info()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
