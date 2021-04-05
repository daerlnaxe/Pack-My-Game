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

namespace UnPack_My_Game.Graph
{
    /// <summary>
    /// Logique d'interaction pour W_Common.xaml
    /// </summary>
    public partial class W_Common : Window
    {

        /// <summary>
        /// Title Showed on Main Window
        /// </summary>
        public string Title
        {
            get
            {
                System.Reflection.Assembly asmbly = System.Reflection.Assembly.GetExecutingAssembly();
                return $"{asmbly.GetName().Name} - {asmbly.GetName().Version}";
            }
        }

        /// <summary>
        /// Page to Show
        /// </summary>
        public Page ActivePage { get; set; }

        public W_Common()
        {
            InitializeComponent();
            DataContext = this;
        }



    }
}
