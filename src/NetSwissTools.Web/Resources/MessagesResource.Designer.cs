﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NetSwissTools.Web.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class MessagesResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MessagesResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NetSwissTools.Web.Resources.MessagesResource", typeof(MessagesResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Problema ao listar..
        /// </summary>
        public static string ListError {
            get {
                return ResourceManager.GetString("ListError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Problema ao carregar..
        /// </summary>
        public static string LoadError {
            get {
                return ResourceManager.GetString("LoadError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Problema ao remover..
        /// </summary>
        public static string RemoveError {
            get {
                return ResourceManager.GetString("RemoveError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Problema ao salvar..
        /// </summary>
        public static string SaveError {
            get {
                return ResourceManager.GetString("SaveError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Problema no servidor..
        /// </summary>
        public static string ServerError {
            get {
                return ResourceManager.GetString("ServerError", resourceCulture);
            }
        }
    }
}