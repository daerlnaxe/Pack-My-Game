﻿#pragma checksum "..\..\..\..\..\Graph\LaunchBox\W_Config.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A72251E8AE8205F6426BCFFEE24797E4F6BC6CA3"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using DxTBoxCore.Common;
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
    /// W_Config
    /// </summary>
    public partial class W_Config : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 44 "..\..\..\..\..\Graph\LaunchBox\W_Config.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbLBPath;
        
        #line default
        #line hidden
        
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
            System.Uri resourceLocater = new System.Uri("/UnPack My Game;V1.0.0.0;component/graph/launchbox/w_config.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Graph\LaunchBox\W_Config.xaml"
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
            
            #line 13 "..\..\..\..\..\Graph\LaunchBox\W_Config.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.Can_Submit);
            
            #line default
            #line hidden
            
            #line 13 "..\..\..\..\..\Graph\LaunchBox\W_Config.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Exec_Submit);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 25 "..\..\..\..\..\Graph\LaunchBox\W_Config.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Close);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbLBPath = ((System.Windows.Controls.TextBox)(target));
            
            #line 46 "..\..\..\..\..\Graph\LaunchBox\W_Config.xaml"
            this.tbLBPath.PreviewKeyUp += new System.Windows.Input.KeyEventHandler(this.tbLBPath_PreviewKeyUp);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 49 "..\..\..\..\..\Graph\LaunchBox\W_Config.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Find_Launchbox);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 57 "..\..\..\..\..\Graph\LaunchBox\W_Config.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ResetT_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 94 "..\..\..\..\..\Graph\LaunchBox\W_Config.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Raz_Games);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 98 "..\..\..\..\..\Graph\LaunchBox\W_Config.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Raz_CheatsCodes);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 102 "..\..\..\..\..\Graph\LaunchBox\W_Config.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Raz_Manuals);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 106 "..\..\..\..\..\Graph\LaunchBox\W_Config.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Raz_Images);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 110 "..\..\..\..\..\Graph\LaunchBox\W_Config.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Raz_Musics);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 114 "..\..\..\..\..\Graph\LaunchBox\W_Config.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Raz_Videos);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

