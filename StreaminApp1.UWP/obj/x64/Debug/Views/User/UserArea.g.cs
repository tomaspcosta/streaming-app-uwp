﻿#pragma checksum "C:\Users\tomas\Desktop\DA\Projeto\3ºParte\ad-project-group-11-main\StreamingApp\StreaminApp1.UWP\Views\User\UserArea.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3DB20E3BDC6747B6DED22E6C595CCB9C8C959D60DA019A54ACDA3BF163A1C7FD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StreamingApp.UWP.Views.Users
{
    partial class UserArea : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_TextBox_Text(global::Windows.UI.Xaml.Controls.TextBox obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
            public static void Set_Windows_UI_Xaml_Controls_PasswordBox_Password(global::Windows.UI.Xaml.Controls.PasswordBox obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Password = value ?? global::System.String.Empty;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class UserArea_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IDataTemplateComponent,
            global::Windows.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IUserArea_Bindings
        {
            private global::StreamingApp.UWP.Views.Users.UserArea dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.TextBox obj3;
            private global::Windows.UI.Xaml.Controls.TextBox obj4;
            private global::Windows.UI.Xaml.Controls.TextBox obj5;
            private global::Windows.UI.Xaml.Controls.PasswordBox obj6;
            private global::Windows.UI.Xaml.Controls.PasswordBox obj7;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj3TextDisabled = false;
            private static bool isobj4TextDisabled = false;
            private static bool isobj5TextDisabled = false;
            private static bool isobj6PasswordDisabled = false;
            private static bool isobj7PasswordDisabled = false;

            private UserArea_obj1_BindingsTracking bindingsTracking;

            public UserArea_obj1_Bindings()
            {
                this.bindingsTracking = new UserArea_obj1_BindingsTracking(this);
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 18 && columnNumber == 76)
                {
                    isobj3TextDisabled = true;
                }
                else if (lineNumber == 19 && columnNumber == 79)
                {
                    isobj4TextDisabled = true;
                }
                else if (lineNumber == 20 && columnNumber == 83)
                {
                    isobj5TextDisabled = true;
                }
                else if (lineNumber == 21 && columnNumber == 83)
                {
                    isobj6PasswordDisabled = true;
                }
                else if (lineNumber == 22 && columnNumber == 91)
                {
                    isobj7PasswordDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 3: // Views\User\UserArea.xaml line 18
                        this.obj3 = (global::Windows.UI.Xaml.Controls.TextBox)target;
                        this.bindingsTracking.RegisterTwoWayListener_3(this.obj3);
                        break;
                    case 4: // Views\User\UserArea.xaml line 19
                        this.obj4 = (global::Windows.UI.Xaml.Controls.TextBox)target;
                        this.bindingsTracking.RegisterTwoWayListener_4(this.obj4);
                        break;
                    case 5: // Views\User\UserArea.xaml line 20
                        this.obj5 = (global::Windows.UI.Xaml.Controls.TextBox)target;
                        this.bindingsTracking.RegisterTwoWayListener_5(this.obj5);
                        break;
                    case 6: // Views\User\UserArea.xaml line 21
                        this.obj6 = (global::Windows.UI.Xaml.Controls.PasswordBox)target;
                        this.bindingsTracking.RegisterTwoWayListener_6(this.obj6);
                        break;
                    case 7: // Views\User\UserArea.xaml line 22
                        this.obj7 = (global::Windows.UI.Xaml.Controls.PasswordBox)target;
                        this.bindingsTracking.RegisterTwoWayListener_7(this.obj7);
                        break;
                    default:
                        break;
                }
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

            // IUserArea_Bindings

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
                    this.dataRoot = (global::StreamingApp.UWP.Views.Users.UserArea)newDataRoot;
                    return true;
                }
                return false;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::StreamingApp.UWP.Views.Users.UserArea obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_UserViewModel(obj.UserViewModel, phase);
                    }
                }
            }
            private void Update_UserViewModel(global::StreamingApp.UWP.ViewModels.UserViewModel obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners_UserViewModel(obj);
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_UserViewModel_Email(obj.Email, phase);
                        this.Update_UserViewModel_Username(obj.Username, phase);
                        this.Update_UserViewModel_PhoneNumber(obj.PhoneNumber, phase);
                        this.Update_UserViewModel_Password(obj.Password, phase);
                        this.Update_UserViewModel_ConfirmPassword(obj.ConfirmPassword, phase);
                    }
                }
            }
            private void Update_UserViewModel_Email(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // Views\User\UserArea.xaml line 18
                    if (!isobj3TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBox_Text(this.obj3, obj, null);
                    }
                }
            }
            private void Update_UserViewModel_Username(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // Views\User\UserArea.xaml line 19
                    if (!isobj4TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBox_Text(this.obj4, obj, null);
                    }
                }
            }
            private void Update_UserViewModel_PhoneNumber(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // Views\User\UserArea.xaml line 20
                    if (!isobj5TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBox_Text(this.obj5, obj, null);
                    }
                }
            }
            private void Update_UserViewModel_Password(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // Views\User\UserArea.xaml line 21
                    if (!isobj6PasswordDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_PasswordBox_Password(this.obj6, obj, null);
                    }
                }
            }
            private void Update_UserViewModel_ConfirmPassword(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // Views\User\UserArea.xaml line 22
                    if (!isobj7PasswordDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_PasswordBox_Password(this.obj7, obj, null);
                    }
                }
            }
            private void UpdateTwoWay_3_Text()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.UserViewModel != null)
                        {
                            this.dataRoot.UserViewModel.Email = this.obj3.Text;
                        }
                    }
                }
            }
            private void UpdateTwoWay_4_Text()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.UserViewModel != null)
                        {
                            this.dataRoot.UserViewModel.Username = this.obj4.Text;
                        }
                    }
                }
            }
            private void UpdateTwoWay_5_Text()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.UserViewModel != null)
                        {
                            this.dataRoot.UserViewModel.PhoneNumber = this.obj5.Text;
                        }
                    }
                }
            }
            private void UpdateTwoWay_6_Password()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.UserViewModel != null)
                        {
                            this.dataRoot.UserViewModel.Password = this.obj6.Password;
                        }
                    }
                }
            }
            private void UpdateTwoWay_7_Password()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.UserViewModel != null)
                        {
                            this.dataRoot.UserViewModel.ConfirmPassword = this.obj7.Password;
                        }
                    }
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private class UserArea_obj1_BindingsTracking
            {
                private global::System.WeakReference<UserArea_obj1_Bindings> weakRefToBindingObj; 

                public UserArea_obj1_BindingsTracking(UserArea_obj1_Bindings obj)
                {
                    weakRefToBindingObj = new global::System.WeakReference<UserArea_obj1_Bindings>(obj);
                }

                public UserArea_obj1_Bindings TryGetBindingObject()
                {
                    UserArea_obj1_Bindings bindingObject = null;
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
                    UpdateChildListeners_UserViewModel(null);
                }

                public void PropertyChanged_UserViewModel(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
                {
                    UserArea_obj1_Bindings bindings = TryGetBindingObject();
                    if (bindings != null)
                    {
                        string propName = e.PropertyName;
                        global::StreamingApp.UWP.ViewModels.UserViewModel obj = sender as global::StreamingApp.UWP.ViewModels.UserViewModel;
                        if (global::System.String.IsNullOrEmpty(propName))
                        {
                            if (obj != null)
                            {
                                bindings.Update_UserViewModel_Email(obj.Email, DATA_CHANGED);
                                bindings.Update_UserViewModel_Username(obj.Username, DATA_CHANGED);
                                bindings.Update_UserViewModel_PhoneNumber(obj.PhoneNumber, DATA_CHANGED);
                                bindings.Update_UserViewModel_Password(obj.Password, DATA_CHANGED);
                                bindings.Update_UserViewModel_ConfirmPassword(obj.ConfirmPassword, DATA_CHANGED);
                            }
                        }
                        else
                        {
                            switch (propName)
                            {
                                case "Email":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_UserViewModel_Email(obj.Email, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "Username":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_UserViewModel_Username(obj.Username, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "PhoneNumber":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_UserViewModel_PhoneNumber(obj.PhoneNumber, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "Password":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_UserViewModel_Password(obj.Password, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "ConfirmPassword":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_UserViewModel_ConfirmPassword(obj.ConfirmPassword, DATA_CHANGED);
                                    }
                                    break;
                                }
                                default:
                                    break;
                            }
                        }
                    }
                }
                private global::StreamingApp.UWP.ViewModels.UserViewModel cache_UserViewModel = null;
                public void UpdateChildListeners_UserViewModel(global::StreamingApp.UWP.ViewModels.UserViewModel obj)
                {
                    if (obj != cache_UserViewModel)
                    {
                        if (cache_UserViewModel != null)
                        {
                            ((global::System.ComponentModel.INotifyPropertyChanged)cache_UserViewModel).PropertyChanged -= PropertyChanged_UserViewModel;
                            cache_UserViewModel = null;
                        }
                        if (obj != null)
                        {
                            cache_UserViewModel = obj;
                            ((global::System.ComponentModel.INotifyPropertyChanged)obj).PropertyChanged += PropertyChanged_UserViewModel;
                        }
                    }
                }
                public void RegisterTwoWayListener_3(global::Windows.UI.Xaml.Controls.TextBox sourceObject)
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
                public void RegisterTwoWayListener_4(global::Windows.UI.Xaml.Controls.TextBox sourceObject)
                {
                    sourceObject.LostFocus += (sender, e) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_4_Text();
                        }
                    };
                }
                public void RegisterTwoWayListener_5(global::Windows.UI.Xaml.Controls.TextBox sourceObject)
                {
                    sourceObject.LostFocus += (sender, e) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_5_Text();
                        }
                    };
                }
                public void RegisterTwoWayListener_6(global::Windows.UI.Xaml.Controls.PasswordBox sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Windows.UI.Xaml.Controls.PasswordBox.PasswordProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_6_Password();
                        }
                    });
                }
                public void RegisterTwoWayListener_7(global::Windows.UI.Xaml.Controls.PasswordBox sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Windows.UI.Xaml.Controls.PasswordBox.PasswordProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_7_Password();
                        }
                    });
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Views\User\UserArea.xaml line 44
                {
                    global::Windows.UI.Xaml.Controls.Button element2 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element2).Click += this.LogoutButton_Click;
                }
                break;
            case 8: // Views\User\UserArea.xaml line 24
                {
                    this.BtnSave = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnSave).Click += this.BtnSave_Click;
                }
                break;
            case 9: // Views\User\UserArea.xaml line 33
                {
                    this.ErrorMessageTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
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
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1: // Views\User\UserArea.xaml line 1
                {                    
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    UserArea_obj1_Bindings bindings = new UserArea_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                    global::Windows.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element1, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

