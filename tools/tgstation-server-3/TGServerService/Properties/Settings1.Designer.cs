﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TGServerService.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.1.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("tgstation")]
        public string ProjectName {
            get {
                return ((string)(this["ProjectName"]));
            }
            set {
                this["ProjectName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2337")]
        public ushort ServerPort {
            get {
                return ((ushort)(this["ServerPort"]));
            }
            set {
                this["ServerPort"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool PushChangelogToGit {
            get {
                return ((bool)(this["PushChangelogToGit"]));
            }
            set {
                this["PushChangelogToGit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("tgstation-server")]
        public string CommitterName {
            get {
                return ((string)(this["CommitterName"]));
            }
            set {
                this["CommitterName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("tgstation-server@users.noreply.github.com")]
        public string CommitterEmail {
            get {
                return ((string)(this["CommitterEmail"]));
            }
            set {
                this["CommitterEmail"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("tgstation-server")]
        public string CredentialUsername {
            get {
                return ((string)(this["CredentialUsername"]));
            }
            set {
                this["CredentialUsername"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Insufficient data for meaningful answer")]
        public string CredentialEntropy {
            get {
                return ((string)(this["CredentialEntropy"]));
            }
            set {
                this["CredentialEntropy"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("correcthorsebatterystaple")]
        public string CredentialCyphertext {
            get {
                return ((string)(this["CredentialCyphertext"]));
            }
            set {
                this["CredentialCyphertext"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:/tgstation-server-3")]
        public string ServerDirectory {
            get {
                return ((string)(this["ServerDirectory"]));
            }
            set {
                this["ServerDirectory"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int ServerSecurity {
            get {
                return ((int)(this["ServerSecurity"]));
            }
            set {
                this["ServerSecurity"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2")]
        public int ServerVisiblity {
            get {
                return ((int)(this["ServerVisiblity"]));
            }
            set {
                this["ServerVisiblity"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ChatEnabled {
            get {
                return ((bool)(this["ChatEnabled"]));
            }
            set {
                this["ChatEnabled"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool DDAutoStart {
            get {
                return ((bool)(this["DDAutoStart"]));
            }
            set {
                this["DDAutoStart"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Python27")]
        public string PythonPath {
            get {
                return ((string)(this["PythonPath"]));
            }
            set {
                this["PythonPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public int ChatProvider {
            get {
                return ((int)(this["ChatProvider"]));
            }
            set {
                this["ChatProvider"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("NEEDS INITIALIZING")]
        public string ChatProviderData {
            get {
                return ((string)(this["ChatProviderData"]));
            }
            set {
                this["ChatProviderData"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3." +
            "org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <s" +
            "tring>theghostofwhibyl1</string>\r\n</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection ChatAdmins {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["ChatAdmins"]));
            }
            set {
                this["ChatAdmins"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3." +
            "org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <s" +
            "tring>#botbus</string>\r\n</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection ChatChannels {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["ChatChannels"]));
            }
            set {
                this["ChatChannels"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("#botbus")]
        public string ChatAdminChannel {
            get {
                return ((string)(this["ChatAdminChannel"]));
            }
            set {
                this["ChatAdminChannel"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("John Madden")]
        public string ChatProviderEntropy {
            get {
                return ((string)(this["ChatProviderEntropy"]));
            }
            set {
                this["ChatProviderEntropy"] = value;
            }
        }
    }
}
