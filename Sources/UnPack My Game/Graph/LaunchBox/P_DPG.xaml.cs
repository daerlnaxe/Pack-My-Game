using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using UnPack_My_Game.Models.LaunchBox;
using UnPack_My_Game.Resources;
using System.Linq;
using System.Diagnostics;

namespace UnPack_My_Game.Graph.LaunchBox
{
    /// <summary>
    /// Logique d'interaction pour P_DPG_Main.xaml
    /// </summary>
    public partial class P_DPG : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly RoutedUICommand AddCmd = new RoutedUICommand(Lang.Add, "AddCmd", typeof(P_DPG));
        public static readonly RoutedUICommand RemoveCmd = new RoutedUICommand(Lang.Remove, "RemoveCmd", typeof(P_DPG));
        public static readonly RoutedUICommand ProcessCmd = new RoutedUICommand(Lang.Process, "ProcessCmd", typeof(P_DPG));

        private I_DPG _Model;
        public I_DPG Model
        {
            get => _Model;
            set
            {
                _Model = value;
                DataContext = value;

                if (value == null)
                    ShowCenter = Visibility.Hidden;
                else
                    ShowCenter = Visibility.Visible;
            }
        }

        private Visibility _ShowCenter = Visibility.Hidden;
        public Visibility ShowCenter
        {
            get => _ShowCenter;
            set
            {
                _ShowCenter = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ShowCenter)));

            }
        }

        public P_DPG()
        {
            //Model = new M_DPGFileZip();
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Crée un dpg à partir de fichiers compressés
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DPGFile_Click(object sender, RoutedEventArgs e)
        {
            //DataContext = new M_DPGFileZip();
            Model = new M_DPGFileZip();

        }

        private void DPGFolder_Click(object sender, RoutedEventArgs e)
        {
            //DataContext = null;
            Model = null;
        }

        private void Can_Add(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model != null;
        }

        private void Exec_Add(object sender, ExecutedRoutedEventArgs e)
        {
            Model.Add();
        }

        private void Can_Remove(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model != null && Model.Elements.Any();
        }

        private void Exec_Remove(object sender, ExecutedRoutedEventArgs e)
        {
            Model.RemoveElements((IList<object>)e.Parameter);
        }

        private void Remove_Element(object sender, RoutedEventArgs e)
        {
            Model.RemoveElement(((Button)sender).Tag);
        }

        private void Surprendsmoi(object sender, DependencyPropertyChangedEventArgs e)
        {
            Debug.WriteLine("Je suis surpris");
        }


        public List<object> hitResultsList { get; private set; } = new List<object>();



        // Return the result of the hit test to the callback.
        public HitTestResultBehavior MyHitTestResult(HitTestResult result)
        {
            Debug.WriteLine(result.ToString());
            // Add the hit test result to the list that will be processed after the enumeration.
            hitResultsList.Add(result.VisualHit);

            // Set the behavior to return visuals at all z-order levels.
            return HitTestResultBehavior.Continue;
        }
        /*
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Retrieve the coordinate of the mouse position.
            Point pt = e.GetPosition((UIElement)sender);

            // Clear the contents of the list used for hit test results.
            hitResultsList.Clear();

            // Set up a callback to receive the hit test result enumeration.
            VisualTreeHelper.HitTest(lvItem, null,
                new HitTestResultCallback(MyHitTestResult),
                new PointHitTestParameters(pt));

            Debug.WriteLine(hitResultsList.Count());

            // Perform actions on the hit test results list.
            if (hitResultsList.Count > 0)
            {
                Console.WriteLine("Number of Visuals Hit: " + hitResultsList.Count);
            }
        }
        */


        #region Selection
        bool mouseDown = false; // Set to 'true' when mouse is held down.
        Point mouseDownPos; // The point where the mouse button was clicked down.
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            /*  // Capture and track the mouse.
              mouseDown = true;
              mouseDownPos = e.GetPosition(lvItem);
              lvItem.CaptureMouse();

              // Initial placement of the drag selection box.         
              Canvas.SetLeft(selectionBox, mouseDownPos.X);
              Canvas.SetTop(selectionBox, mouseDownPos.Y);
              selectionBox.Width = 0;
              selectionBox.Height = 0;

              // Make the drag selection box visible.
              selectionBox.Visibility = Visibility.Visible;*/
        }




        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {/*
            if (mouseDown)
            {
                // When the mouse is held down, reposition the drag selection box.

                Point mousePos = e.GetPosition(lvItem);

                if (mouseDownPos.X < mousePos.X)
                {
                    Canvas.SetLeft(selectionBox, mouseDownPos.X);
                    selectionBox.Width = mousePos.X - mouseDownPos.X;
                }
                else
                {
                    Canvas.SetLeft(selectionBox, mousePos.X);
                    selectionBox.Width = mouseDownPos.X - mousePos.X;
                }

                if (mouseDownPos.Y < mousePos.Y)
                {
                    Canvas.SetTop(selectionBox, mouseDownPos.Y);
                    selectionBox.Height = mousePos.Y - mouseDownPos.Y;
                }
                else
                {
                    Canvas.SetTop(selectionBox, mousePos.Y);
                    selectionBox.Height = mouseDownPos.Y - mousePos.Y;
                }
            }*/
        }


        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {        // Release the mouse capture and stop tracking it.
            /* mouseDown = false;
             lvItem.ReleaseMouseCapture();

             // Hide the drag selection box.
             selectionBox.Visibility = Visibility.Collapsed;

             Point mouseUpPos = e.GetPosition(lvItem);


             // TODO: 
             //
             // The mouse has been released, check to see if any of the items 
             // in the other canvas are contained within mouseDownPos and 
             // mouseUpPos, for any that are, select them!
             //
             foreach (object elem in lvItem.Items)
             {

             }
         }
            */
            #endregion
        }

        #region process
        private void Can_Process(object sender, CanExecuteRoutedEventArgs e)
        {
            if(Model!=null                )
            e.CanExecute = !Model.HasErrors;
        }

        private void Exec_Process(object sender, ExecutedRoutedEventArgs e)
        {
            Model.Process();
        }
        #endregion
    }
}

