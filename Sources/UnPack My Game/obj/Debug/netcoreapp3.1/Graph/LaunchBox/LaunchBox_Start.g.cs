﻿#pragma checksum "..\..\..\..\..\Graph\LaunchBox\LaunchBox_Start.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9232A917EB9EC295203D16E3ACDBFC1A29B8C870"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using DxTBoxCore.Languages;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using UnPack_My_Game.Graph.LaunchBox;
using UnPack_My_Game.Resources;


namespace UnPack_My_Game.Graph.LaunchBox {
    
    
    /// <summary>
    /// LaunchBox_Start
    /// </summary>
    public partial class LaunchBox_Start : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/UnPack My Game;component/graph/launchbox/launchbox_start.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Graph\LaunchBox\LaunchBox_Start.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 15 "..\..\..\..\..\Graph\LaunchBox\LaunchBox_Start.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.Process_CanExec);
            
            #line default
            #line hidden
            
            #line 15 "..\..\..\..\..\Graph\LaunchBox\LaunchBox_Start.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Process_Executed);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 32 "..\..\..\..\..\Graph\LaunchBox\LaunchBox_Start.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LB_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 33 "..\..\..\..\..\Graph\LaunchBox\LaunchBox_Start.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EB_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 35 "..\..\..\..\..\Graph\LaunchBox\LaunchBox_Start.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OneG_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 38 "..\..\..\..\..\Graph\LaunchBox\LaunchBox_Start.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SevG_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 42 "..\..\..\..\..\Graph\LaunchBox\LaunchBox_Start.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Config_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
