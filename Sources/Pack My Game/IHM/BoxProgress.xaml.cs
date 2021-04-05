using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
    /// Logique d'interaction pour BoxProgress.xaml
    /// </summary>
    public partial class BoxProgress : Window
    {
		public bool Submit { get; set; }

        public BoxProgress()
        {
            InitializeComponent();
        }

		

		private async void button1_Click(object sender, RoutedEventArgs e)
		{
			List<string> myCommandList = new List<string>();
			for (int i = 0; i < 10; i++) myCommandList.Add(i.ToString());

			Submit = false;

			await DoWork(myCommandList);

			Submit = true;
		}

		private async Task DoWork(List<string> commandList)
		{
			//IProgress<int> progress = new Progress<int>((i) => progressBar1.Value = (int)(progressBar1.Maximum * i / commandList.Count));
			await Task.Run(() =>
			{
				int i = 0;
				foreach (string c in commandList)
				{
					ValidateCommand(c);
					progressBar1.Value = ++i;
				}
			});
		}

		private void ValidateCommand(string myCommand)
		{
			// Faire les calculs avec myCommand...
			Thread.Sleep(1000); // Pour simuler
			Debug.WriteLine("Mee");
		}


    }
}
