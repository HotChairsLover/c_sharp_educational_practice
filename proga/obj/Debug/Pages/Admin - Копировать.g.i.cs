﻿#pragma checksum "..\..\..\Pages\Admin - Копировать.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "44022B204579CFC216A10F4882F91A040073EF7DAC2D0634D96DF19C572398E1"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Pizza1.Pages;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace Pizza1.Pages {
    
    
    /// <summary>
    /// Аdmin
    /// </summary>
    public partial class Аdmin : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 68 "..\..\..\Pages\Admin - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Name;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\Pages\Admin - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView list_users;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\Pages\Admin - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox dish_serch_text;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\Pages\Admin - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel parrent_dish;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\Pages\Admin - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ingr_search;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\Pages\Admin - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView list_ingredients;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\..\Pages\Admin - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox find_ord;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\Pages\Admin - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView list_orders;
        
        #line default
        #line hidden
        
        
        #line 158 "..\..\..\Pages\Admin - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas canvas;
        
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
            System.Uri resourceLocater = new System.Uri("/proga;component/pages/admin%20-%20%d0%9a%d0%be%d0%bf%d0%b8%d1%80%d0%be%d0%b2%d0%" +
                    "b0%d1%82%d1%8c.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\Admin - Копировать.xaml"
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
            
            #line 57 "..\..\..\Pages\Admin - Копировать.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Exit);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Name = ((System.Windows.Controls.TextBox)(target));
            
            #line 68 "..\..\..\Pages\Admin - Копировать.xaml"
            this.Name.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Name_search);
            
            #line default
            #line hidden
            return;
            case 3:
            this.list_users = ((System.Windows.Controls.ListView)(target));
            
            #line 69 "..\..\..\Pages\Admin - Копировать.xaml"
            this.list_users.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.list_users_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 87 "..\..\..\Pages\Admin - Копировать.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddUser);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 88 "..\..\..\Pages\Admin - Копировать.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteUser);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 89 "..\..\..\Pages\Admin - Копировать.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ExportUser);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 90 "..\..\..\Pages\Admin - Копировать.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ImportUser);
            
            #line default
            #line hidden
            return;
            case 8:
            this.dish_serch_text = ((System.Windows.Controls.TextBox)(target));
            
            #line 95 "..\..\..\Pages\Admin - Копировать.xaml"
            this.dish_serch_text.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Dish_search);
            
            #line default
            #line hidden
            return;
            case 9:
            this.parrent_dish = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 10:
            
            #line 99 "..\..\..\Pages\Admin - Копировать.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddDish);
            
            #line default
            #line hidden
            return;
            case 11:
            this.ingr_search = ((System.Windows.Controls.TextBox)(target));
            
            #line 104 "..\..\..\Pages\Admin - Копировать.xaml"
            this.ingr_search.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Ingridients_search);
            
            #line default
            #line hidden
            return;
            case 12:
            this.list_ingredients = ((System.Windows.Controls.ListView)(target));
            
            #line 105 "..\..\..\Pages\Admin - Копировать.xaml"
            this.list_ingredients.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.list_ingrid_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 121 "..\..\..\Pages\Admin - Копировать.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddIngridient);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 122 "..\..\..\Pages\Admin - Копировать.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteIngridient);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 123 "..\..\..\Pages\Admin - Копировать.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ExportIngridient);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 124 "..\..\..\Pages\Admin - Копировать.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ImportIngridient);
            
            #line default
            #line hidden
            return;
            case 17:
            this.find_ord = ((System.Windows.Controls.TextBox)(target));
            
            #line 129 "..\..\..\Pages\Admin - Копировать.xaml"
            this.find_ord.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Orders_search);
            
            #line default
            #line hidden
            return;
            case 18:
            this.list_orders = ((System.Windows.Controls.ListView)(target));
            return;
            case 20:
            this.canvas = ((System.Windows.Controls.Canvas)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 19:
            
            #line 147 "..\..\..\Pages\Admin - Копировать.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Press_orders);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

