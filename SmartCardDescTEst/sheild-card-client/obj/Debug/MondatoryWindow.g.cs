﻿#pragma checksum "..\..\MondatoryWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "787BF860E02591F077D0DB29D74D45E7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GID_Client;
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
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace GID_Client {
    
    
    /// <summary>
    /// MondatoryWindow
    /// </summary>
    public partial class MondatoryWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\MondatoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.MaskedTextBox txbDocNum;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\MondatoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.MaskedTextBox DpBirthDate;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\MondatoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.MaskedTextBox DpExpireDate;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\MondatoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnStart;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\MondatoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GID_Client;component/mondatorywindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MondatoryWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txbDocNum = ((Xceed.Wpf.Toolkit.MaskedTextBox)(target));
            
            #line 37 "..\..\MondatoryWindow.xaml"
            this.txbDocNum.KeyDown += new System.Windows.Input.KeyEventHandler(this.txbDocNum_KeyDown);
            
            #line default
            #line hidden
            
            #line 38 "..\..\MondatoryWindow.xaml"
            this.txbDocNum.KeyUp += new System.Windows.Input.KeyEventHandler(this.txbDocNum_KeyUp);
            
            #line default
            #line hidden
            
            #line 39 "..\..\MondatoryWindow.xaml"
            this.txbDocNum.LostFocus += new System.Windows.RoutedEventHandler(this.txbDocNum_LostFocus);
            
            #line default
            #line hidden
            return;
            case 2:
            this.DpBirthDate = ((Xceed.Wpf.Toolkit.MaskedTextBox)(target));
            
            #line 54 "..\..\MondatoryWindow.xaml"
            this.DpBirthDate.KeyDown += new System.Windows.Input.KeyEventHandler(this.DpBirthDate_KeyDown_1);
            
            #line default
            #line hidden
            
            #line 55 "..\..\MondatoryWindow.xaml"
            this.DpBirthDate.KeyUp += new System.Windows.Input.KeyEventHandler(this.DpBirthDate_KeyUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.DpExpireDate = ((Xceed.Wpf.Toolkit.MaskedTextBox)(target));
            
            #line 74 "..\..\MondatoryWindow.xaml"
            this.DpExpireDate.KeyDown += new System.Windows.Input.KeyEventHandler(this.DpExpireDate_KeyDown);
            
            #line default
            #line hidden
            
            #line 75 "..\..\MondatoryWindow.xaml"
            this.DpExpireDate.KeyUp += new System.Windows.Input.KeyEventHandler(this.DpExpireDate_KeyUp);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnStart = ((System.Windows.Controls.Button)(target));
            
            #line 91 "..\..\MondatoryWindow.xaml"
            this.btnStart.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 98 "..\..\MondatoryWindow.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

