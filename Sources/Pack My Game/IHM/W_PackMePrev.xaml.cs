﻿using Pack_My_Game.Language;
using Pack_My_Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Logique d'interaction pour W_PackMeRes.xaml
    /// </summary>
    public partial class W_PackMePrev : Window
    {
        public M_PackMePrev Model;

        public W_PackMePrev()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;

        }

        #region Games

        // ...

        #endregion

        #region Images


        private void Can_EligImage(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.EligibleImages.Any();
        }

        private void Can_Image(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.ImagesCollection.Any();
        }

        private void Exec_AddImage(object sender, ExecutedRoutedEventArgs e)
        {
            lvImages.Visibility = Visibility.Collapsed;
            Model.AddImage();
            lvImages.Visibility = Visibility.Visible;
        }

        private void Exec_OpenEligImage(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter != null)
                Model.OpenImage((string)e.Parameter);
        }

        private void Exec_OpenImage(object sender, ExecutedRoutedEventArgs e)
        {
            Model.OpenImage(Model.ImageSelected.CurrentPath);
        }

        private void Exec_RemoveImage(object sender, ExecutedRoutedEventArgs e)
        {
            Model.RemoveImage();
        }
        #endregion

        #region Manuels
        /*private void Can_Manual(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.ManualsCollection.Any();
        }*/

        private void Exec_AddManual(object sender, ExecutedRoutedEventArgs e)
        {

            Model.AddManual();
        }


        /*private void Can_EligManual(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.EligibleManuals.Any();
        }*/


        private void Exec_OpenManual(object sender, ExecutedRoutedEventArgs e)
        {
            Model.OpenManual((string)e.Parameter);
        }
        private void Exec_RemoveManual(object sender, ExecutedRoutedEventArgs e)
        {
            Model.RemoveManual();
        }

        #endregion

        #region Musique
        /*private void Can_Music(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.MusicsCollection.Any();

        }*/
        private void Exec_AddMusic(object sender, ExecutedRoutedEventArgs e)
        {
            Model.AddMusic();
        }
        private void Exec_OpenMusic(object sender, ExecutedRoutedEventArgs e)
        {
            Model.OpenMusic((string)e.Parameter);
        }

        private void Exec_RemoveMusic(object sender, ExecutedRoutedEventArgs e)
        {
            Model.RemoveMusic();
        }

        /*private void Can_AddMusic(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.EligibleMusics.Any();
        }*/


        #endregion

        #region Video
        /*private void Can_Video(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.VideosCollection.Any();
        }*/
        private void Exec_AddVideo(object sender, ExecutedRoutedEventArgs e)
        {
            Model.AddVideo();
        }
        private void Exec_OpenVideo(object sender, ExecutedRoutedEventArgs e)
        {
            Model.OpenVideo((string)e.Parameter);
        }

        private void Exec_RemoveVideo(object sender, ExecutedRoutedEventArgs e)
        {
            Model.RemoveVideo();
        }


        /*private void Can_AddVideo(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.EligibleVideos.Any();
        }*/


        #endregion

        #region Cheats
        /*private void Can_Cheat(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.CheatsCollection.Any();
        }*/

        private void Exec_AddCheat(object sender, ExecutedRoutedEventArgs e)
        {
            Model.AddCheat();
        }

        private void Exec_OpenCheat(object sender, ExecutedRoutedEventArgs e)
        {
            Model.OpenCheatInDefaultEditor((string)e.Parameter);
        }

        private void Exec_RemoveCheat(object sender, ExecutedRoutedEventArgs e)
        {
            Model.RemoveCheat();
        }
        /*private void Can_AddCheat(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.EligibleCheats.Any();
        }*/


        #endregion


        private void Reset(object sender, RoutedEventArgs e)
        {
            Model.Init();
        }


        #region Recherche
        private void Recherche_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            /*
            if (e.Key == Key.Space && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                Model.SearchString = string.Empty;
                e.Handled = true;
            }*/

            if (e.Key == Key.Enter)
            {
                Model.FillWords(Model.SearchString);
            }
        }

        private void Remove_Word(object sender, RoutedEventArgs e)
        {
            Model.RemoveWord(((Button)sender).Tag);
        }


        private void Can_Search(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Model != null)
                e.CanExecute = Model.WordsToSearch.Any();
        }

        private void Exec_SelectBox(object sender, ExecutedRoutedEventArgs e)
        {
            //Keyboard.ClearFocus();
            Keyboard.Focus(SearchBox);
            //SearchBox.SelectionStart = 0;
            //SearchBox.SelectionLength = 0;
            Model.SearchString = string.Empty;
        }

        private void Exec_ResetWords(object sender, ExecutedRoutedEventArgs e)
        {
            Model.ResetSearch();
        }

        private void Exec_Search(object sender, ExecutedRoutedEventArgs e)
        {
            Model.SearchFiles();
        }

        #endregion

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (Model.Apply_Modifs())
            {
                DialogResult = true;
                this.Close();
            }

        }



        /*
        /// <summary>
        /// Gère le scroll sur l'image pour n'afficher que ce qui est nécessaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = (FrameworkElement)sender;
            var visibleAreaEntered = false;
            var visibleAreaLeft = false;
            var invisibleItemDisplayed = 0;

            foreach (Common_PMG.Container.Game.DataRepImg image in lvImages.Items)
            {
                if (image.IsSelected)
                    continue;

                var listBoxItem = (FrameworkElement)lvImages.ItemContainerGenerator.ContainerFromItem(image);

                if (visibleAreaLeft == false && IsFullyOrPartiallyVisible(listBoxItem, scrollViewer))
                {
                    visibleAreaEntered = true;
                }
                else if (visibleAreaEntered)
                {
                    visibleAreaLeft = true;
                }


                if (visibleAreaEntered)
                {
                    if (visibleAreaLeft && ++invisibleItemDisplayed > 10)
                        break;

                    image.IsSelected = true;
                }

                //
                bool IsFullyOrPartiallyVisible(FrameworkElement child, FrameworkElement scrollViewer)
                {
                    var childTransform = child.TransformToAncestor(scrollViewer);
                    var childRectangle = childTransform.TransformBounds(
                                              new Rect(new Point(0, 0), child.RenderSize));
                    var ownerRectangle = new Rect(new Point(0, 0), scrollViewer.RenderSize);
                    return ownerRectangle.IntersectsWith(childRectangle);
                }
            }
        }*/
    }
}
