﻿#pragma checksum "C:\Users\alexe\Source\Repos\OtherRepoISS\SocialStuff\SocialStuff\View\GenerateTransferView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1ECD0FCEBF5897E84A6B020C8494FB5932A3EB6B572B8BC56B93C8D6C0D9AB0D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SocialStuff.View
{
    partial class GenerateTransferView : 
        global::Microsoft.UI.Xaml.Controls.Page, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2503")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Microsoft_UI_Xaml_Controls_TextBox_Text(global::Microsoft.UI.Xaml.Controls.TextBox obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
            public static void Set_Microsoft_UI_Xaml_UIElement_Visibility(global::Microsoft.UI.Xaml.UIElement obj, global::Microsoft.UI.Xaml.Visibility value)
            {
                obj.Visibility = value;
            }
            public static void Set_Microsoft_UI_Xaml_Controls_Primitives_Selector_SelectedIndex(global::Microsoft.UI.Xaml.Controls.Primitives.Selector obj, global::System.Int32 value)
            {
                obj.SelectedIndex = value;
            }
            public static void Set_Microsoft_UI_Xaml_Controls_Primitives_ButtonBase_Command(global::Microsoft.UI.Xaml.Controls.Primitives.ButtonBase obj, global::System.Windows.Input.ICommand value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Windows.Input.ICommand) global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Windows.Input.ICommand), targetNullValue);
                }
                obj.Command = value;
            }
            public static void Set_Microsoft_UI_Xaml_Controls_Control_IsEnabled(global::Microsoft.UI.Xaml.Controls.Control obj, global::System.Boolean value)
            {
                obj.IsEnabled = value;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2503")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private partial class GenerateTransferView_obj1_Bindings :
            global::Microsoft.UI.Xaml.Markup.IDataTemplateComponent,
            global::Microsoft.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Microsoft.UI.Xaml.Markup.IComponentConnector,
            IGenerateTransferView_Bindings
        {
            private global::SocialStuff.View.GenerateTransferView dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Microsoft.UI.Xaml.Controls.TextBox obj3;
            private global::Microsoft.UI.Xaml.Controls.TextBlock obj4;
            private global::Microsoft.UI.Xaml.Controls.ComboBox obj5;
            private global::Microsoft.UI.Xaml.Controls.TextBox obj6;
            private global::Microsoft.UI.Xaml.Controls.Button obj7;
            private global::Microsoft.UI.Xaml.Controls.ComboBox obj8;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj3TextDisabled = false;
            private static bool isobj4VisibilityDisabled = false;
            private static bool isobj5SelectedIndexDisabled = false;
            private static bool isobj6TextDisabled = false;
            private static bool isobj7CommandDisabled = false;
            private static bool isobj7IsEnabledDisabled = false;
            private static bool isobj8SelectedIndexDisabled = false;

            private GenerateTransferView_obj1_BindingsTracking bindingsTracking;

            public GenerateTransferView_obj1_Bindings()
            {
                this.bindingsTracking = new GenerateTransferView_obj1_BindingsTracking(this);
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 79 && columnNumber == 22)
                {
                    isobj3TextDisabled = true;
                }
                else if (lineNumber == 86 && columnNumber == 22)
                {
                    isobj4VisibilityDisabled = true;
                }
                else if (lineNumber == 98 && columnNumber == 23)
                {
                    isobj5SelectedIndexDisabled = true;
                }
                else if (lineNumber == 116 && columnNumber == 22)
                {
                    isobj6TextDisabled = true;
                }
                else if (lineNumber == 126 && columnNumber == 13)
                {
                    isobj7CommandDisabled = true;
                }
                else if (lineNumber == 127 && columnNumber == 13)
                {
                    isobj7IsEnabledDisabled = true;
                }
                else if (lineNumber == 40 && columnNumber == 23)
                {
                    isobj8SelectedIndexDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 3: // View\GenerateTransferView.xaml line 73
                        this.obj3 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                        this.bindingsTracking.RegisterTwoWayListener_3(this.obj3);
                        break;
                    case 4: // View\GenerateTransferView.xaml line 83
                        this.obj4 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                        break;
                    case 5: // View\GenerateTransferView.xaml line 94
                        this.obj5 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ComboBox>(target);
                        this.bindingsTracking.RegisterTwoWayListener_5(this.obj5);
                        break;
                    case 6: // View\GenerateTransferView.xaml line 109
                        this.obj6 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                        this.bindingsTracking.RegisterTwoWayListener_6(this.obj6);
                        break;
                    case 7: // View\GenerateTransferView.xaml line 119
                        this.obj7 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                        break;
                    case 8: // View\GenerateTransferView.xaml line 36
                        this.obj8 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ComboBox>(target);
                        this.bindingsTracking.RegisterTwoWayListener_8(this.obj8);
                        break;
                    default:
                        break;
                }
            }
                        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2503")]
                        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target) 
                        {
                            return null;
                        }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
            }

            public void Recycle()
            {
                return;
            }

            // IGenerateTransferView_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.initialized = false;
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                if (newDataRoot != null)
                {
                    this.dataRoot = global::WinRT.CastExtensions.As<global::SocialStuff.View.GenerateTransferView>(newDataRoot);
                    return true;
                }
                return false;
            }

            public void Activated(object obj, global::Microsoft.UI.Xaml.WindowActivatedEventArgs data)
            {
                this.Initialize();
            }

            public void Loading(global::Microsoft.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::SocialStuff.View.GenerateTransferView obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_ViewModel(obj.ViewModel, phase);
                    }
                }
            }
            private void Update_ViewModel(global::SocialStuff.ViewModel.GenerateTransferViewModel obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners_ViewModel(obj);
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_ViewModel_AmountText(obj.AmountText, phase);
                        this.Update_ViewModel_ShowInsufficientFundsError(obj.ShowInsufficientFundsError, phase);
                        this.Update_ViewModel_CurrencyIndex(obj.CurrencyIndex, phase);
                        this.Update_ViewModel_Description(obj.Description, phase);
                    }
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_ViewModel_SendMessageCommand(obj.SendMessageCommand, phase);
                    }
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_ViewModel_IsFormValid(obj.IsFormValid, phase);
                        this.Update_ViewModel_TransferTypeIndex(obj.TransferTypeIndex, phase);
                    }
                }
            }
            private void Update_ViewModel_AmountText(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // View\GenerateTransferView.xaml line 73
                    if (!isobj3TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBox_Text(this.obj3, obj, null);
                    }
                }
            }
            private void Update_ViewModel_ShowInsufficientFundsError(global::System.Boolean obj, int phase)
            {
                if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                {
                    this.Update_ViewModel_ShowInsufficientFundsError_Cast_ShowInsufficientFundsError_To_Visibility(obj ? global::Microsoft.UI.Xaml.Visibility.Visible : global::Microsoft.UI.Xaml.Visibility.Collapsed, phase);
                }
            }
            private void Update_ViewModel_ShowInsufficientFundsError_Cast_ShowInsufficientFundsError_To_Visibility(global::Microsoft.UI.Xaml.Visibility obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // View\GenerateTransferView.xaml line 83
                    if (!isobj4VisibilityDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_UIElement_Visibility(this.obj4, obj);
                    }
                }
            }
            private void Update_ViewModel_CurrencyIndex(global::System.Int32 obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // View\GenerateTransferView.xaml line 94
                    if (!isobj5SelectedIndexDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_Primitives_Selector_SelectedIndex(this.obj5, obj);
                    }
                }
            }
            private void Update_ViewModel_Description(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // View\GenerateTransferView.xaml line 109
                    if (!isobj6TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBox_Text(this.obj6, obj, null);
                    }
                }
            }
            private void Update_ViewModel_SendMessageCommand(global::System.Windows.Input.ICommand obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // View\GenerateTransferView.xaml line 119
                    if (!isobj7CommandDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_Primitives_ButtonBase_Command(this.obj7, obj, null);
                    }
                }
            }
            private void Update_ViewModel_IsFormValid(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // View\GenerateTransferView.xaml line 119
                    if (!isobj7IsEnabledDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_Control_IsEnabled(this.obj7, obj);
                    }
                }
            }
            private void Update_ViewModel_TransferTypeIndex(global::System.Int32 obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // View\GenerateTransferView.xaml line 36
                    if (!isobj8SelectedIndexDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_Primitives_Selector_SelectedIndex(this.obj8, obj);
                    }
                }
            }
            private void UpdateTwoWay_3_Text()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.ViewModel != null)
                        {
                            this.dataRoot.ViewModel.AmountText = this.obj3.Text;
                        }
                    }
                }
            }
            private void UpdateTwoWay_5_SelectedIndex()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.ViewModel != null)
                        {
                            this.dataRoot.ViewModel.CurrencyIndex = this.obj5.SelectedIndex;
                        }
                    }
                }
            }
            private void UpdateTwoWay_6_Text()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.ViewModel != null)
                        {
                            this.dataRoot.ViewModel.Description = this.obj6.Text;
                        }
                    }
                }
            }
            private void UpdateTwoWay_8_SelectedIndex()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.ViewModel != null)
                        {
                            this.dataRoot.ViewModel.TransferTypeIndex = this.obj8.SelectedIndex;
                        }
                    }
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2503")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private class GenerateTransferView_obj1_BindingsTracking
            {
                private global::System.WeakReference<GenerateTransferView_obj1_Bindings> weakRefToBindingObj; 

                public GenerateTransferView_obj1_BindingsTracking(GenerateTransferView_obj1_Bindings obj)
                {
                    weakRefToBindingObj = new global::System.WeakReference<GenerateTransferView_obj1_Bindings>(obj);
                }

                public GenerateTransferView_obj1_Bindings TryGetBindingObject()
                {
                    GenerateTransferView_obj1_Bindings bindingObject = null;
                    if (weakRefToBindingObj != null)
                    {
                        weakRefToBindingObj.TryGetTarget(out bindingObject);
                        if (bindingObject == null)
                        {
                            weakRefToBindingObj = null;
                            ReleaseAllListeners();
                        }
                    }
                    return bindingObject;
                }

                public void ReleaseAllListeners()
                {
                    UpdateChildListeners_ViewModel(null);
                }

                public void PropertyChanged_ViewModel(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
                {
                    GenerateTransferView_obj1_Bindings bindings = TryGetBindingObject();
                    if (bindings != null)
                    {
                        string propName = e.PropertyName;
                        global::SocialStuff.ViewModel.GenerateTransferViewModel obj = sender as global::SocialStuff.ViewModel.GenerateTransferViewModel;
                        if (global::System.String.IsNullOrEmpty(propName))
                        {
                            if (obj != null)
                            {
                                bindings.Update_ViewModel_AmountText(obj.AmountText, DATA_CHANGED);
                                bindings.Update_ViewModel_ShowInsufficientFundsError(obj.ShowInsufficientFundsError, DATA_CHANGED);
                                bindings.Update_ViewModel_CurrencyIndex(obj.CurrencyIndex, DATA_CHANGED);
                                bindings.Update_ViewModel_Description(obj.Description, DATA_CHANGED);
                                bindings.Update_ViewModel_IsFormValid(obj.IsFormValid, DATA_CHANGED);
                                bindings.Update_ViewModel_TransferTypeIndex(obj.TransferTypeIndex, DATA_CHANGED);
                            }
                        }
                        else
                        {
                            switch (propName)
                            {
                                case "AmountText":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_AmountText(obj.AmountText, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "ShowInsufficientFundsError":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_ShowInsufficientFundsError(obj.ShowInsufficientFundsError, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "CurrencyIndex":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_CurrencyIndex(obj.CurrencyIndex, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "Description":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_Description(obj.Description, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "IsFormValid":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_IsFormValid(obj.IsFormValid, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "TransferTypeIndex":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_TransferTypeIndex(obj.TransferTypeIndex, DATA_CHANGED);
                                    }
                                    break;
                                }
                                default:
                                    break;
                            }
                        }
                    }
                }
                private global::SocialStuff.ViewModel.GenerateTransferViewModel cache_ViewModel = null;
                public void UpdateChildListeners_ViewModel(global::SocialStuff.ViewModel.GenerateTransferViewModel obj)
                {
                    if (obj != cache_ViewModel)
                    {
                        if (cache_ViewModel != null)
                        {
                            ((global::System.ComponentModel.INotifyPropertyChanged)cache_ViewModel).PropertyChanged -= PropertyChanged_ViewModel;
                            cache_ViewModel = null;
                        }
                        if (obj != null)
                        {
                            cache_ViewModel = obj;
                            ((global::System.ComponentModel.INotifyPropertyChanged)obj).PropertyChanged += PropertyChanged_ViewModel;
                        }
                    }
                }
                public void RegisterTwoWayListener_3(global::Microsoft.UI.Xaml.Controls.TextBox sourceObject)
                {
                    sourceObject.LostFocus += (sender, e) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_3_Text();
                        }
                    };
                }
                public void RegisterTwoWayListener_5(global::Microsoft.UI.Xaml.Controls.ComboBox sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.Primitives.Selector.SelectedIndexProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_5_SelectedIndex();
                        }
                    });
                }
                public void RegisterTwoWayListener_6(global::Microsoft.UI.Xaml.Controls.TextBox sourceObject)
                {
                    sourceObject.LostFocus += (sender, e) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_6_Text();
                        }
                    };
                }
                public void RegisterTwoWayListener_8(global::Microsoft.UI.Xaml.Controls.ComboBox sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.Primitives.Selector.SelectedIndexProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_8_SelectedIndex();
                        }
                    });
                }
            }
        }

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2503")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // View\GenerateTransferView.xaml line 53
                {
                    this.TransferMoneyGrid = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Grid>(target);
                }
                break;
            case 3: // View\GenerateTransferView.xaml line 73
                {
                    this.AmountTextBox = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.TextBox)this.AmountTextBox).BeforeTextChanging += this.AmountTextBox_BeforeTextChanging;
                    ((global::Microsoft.UI.Xaml.Controls.TextBox)this.AmountTextBox).TextChanged += this.AmountTextBox_TextChanged;
                }
                break;
            case 4: // View\GenerateTransferView.xaml line 83
                {
                    this.InsufficientFundsText = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 5: // View\GenerateTransferView.xaml line 94
                {
                    this.CurrencyComboBox = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ComboBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.ComboBox)this.CurrencyComboBox).SelectionChanged += this.CurrencyComboBox_SelectionChanged;
                }
                break;
            case 6: // View\GenerateTransferView.xaml line 109
                {
                    this.DescriptionTextBox = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                }
                break;
            case 7: // View\GenerateTransferView.xaml line 119
                {
                    this.TransferButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                }
                break;
            case 8: // View\GenerateTransferView.xaml line 36
                {
                    this.TransferTypeComboBox = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ComboBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.ComboBox)this.TransferTypeComboBox).SelectionChanged += this.TransferTypeCombobox_SelectionChanged;
                }
                break;
            case 9: // View\GenerateTransferView.xaml line 47
                {
                    this.TitleTextBlock = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 10: // View\GenerateTransferView.xaml line 26
                {
                    global::Microsoft.UI.Xaml.Controls.Button element10 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)element10).Click += this.BackButton_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }


        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2503")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1: // View\GenerateTransferView.xaml line 2
                {                    
                    global::Microsoft.UI.Xaml.Controls.Page element1 = (global::Microsoft.UI.Xaml.Controls.Page)target;
                    GenerateTransferView_obj1_Bindings bindings = new GenerateTransferView_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                    global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element1, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

