﻿using System;
using System.Collections.Generic;
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
using UnPack_My_Game.Models;

namespace UnPack_My_Game.Graph.LaunchBox
{
    /// <summary>
    /// Logique d'interaction pour P_LBMethod.xaml
    /// </summary>
    public partial class P_LBMethod : Page, I_Method
    {
        public A_Method Model { get; set; } = new M_LBMethod();


        public P_LBMethod()
        {
            InitializeComponent();
            DataContext = Model;
        }
    }
}