﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BestPurchase.ServiceLayer {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class DestinationNames {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DestinationNames() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BestPurchase.ServiceLayer.DestinationNames", typeof(DestinationNames).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /api/ML/AddOrder.
        /// </summary>
        internal static string AddOrder {
            get {
                return ResourceManager.GetString("AddOrder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /api/ML/AddProductToCart.
        /// </summary>
        internal static string AddProductToCart {
            get {
                return ResourceManager.GetString("AddProductToCart", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /api/ML/DeleteProductFromCart.
        /// </summary>
        internal static string DeleteProductFromCart {
            get {
                return ResourceManager.GetString("DeleteProductFromCart", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /api/ML/GetProductById?Id=.
        /// </summary>
        internal static string GetProductById {
            get {
                return ResourceManager.GetString("GetProductById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /api/ML/GetProducts.
        /// </summary>
        internal static string GetProducts {
            get {
                return ResourceManager.GetString("GetProducts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /api/ML/GetShoppingCartContent?cartId=.
        /// </summary>
        internal static string GetShoppingCartContent {
            get {
                return ResourceManager.GetString("GetShoppingCartContent", resourceCulture);
            }
        }
    }
}
