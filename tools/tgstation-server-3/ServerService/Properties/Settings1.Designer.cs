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
        [global::System.Configuration.DefaultSettingValueAttribute("8000")]
        public ushort WCFPort {
            get {
                return ((ushort)(this["WCFPort"]));
            }
            set {
                this["WCFPort"] = value;
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
        [global::System.Configuration.DefaultSettingValueAttribute("TGS3")]
        public string IRCNick {
            get {
                return ((string)(this["IRCNick"]));
            }
            set {
                this["IRCNick"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("hunter2")]
        public string IRCPass {
            get {
                return ((string)(this["IRCPass"]));
            }
            set {
                this["IRCPass"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>#tgstation13</string>
  <string>#devbus</string>
  <string>#coderbus</string>
  <string>#adminbus</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection IRCChannels {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["IRCChannels"]));
            }
            set {
                this["IRCChannels"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("irc.rizon.net")]
        public string IRCServer {
            get {
                return ((string)(this["IRCServer"]));
            }
            set {
                this["IRCServer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("6667")]
        public ushort IRCPort {
            get {
                return ((ushort)(this["IRCPort"]));
            }
            set {
                this["IRCPort"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("adminbus")]
        public string IRCAdminChannel {
            get {
                return ((string)(this["IRCAdminChannel"]));
            }
            set {
                this["IRCAdminChannel"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool IRCEnabled {
            get {
                return ((bool)(this["IRCEnabled"]));
            }
            set {
                this["IRCEnabled"] = value;
            }
        }
    }
}
