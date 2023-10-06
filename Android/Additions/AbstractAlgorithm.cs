using System;
using Android.Runtime;
using Java.Interop;

namespace Com.Google.Maps.Android.Clustering.Algo 
{
    public abstract partial class AbstractAlgorithm
    {
        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.maps.android.clustering.algo']/interface[@name='Algorithm']/method[@name='addItem' and count(parameter)=1 and parameter[1][@type='T']]"
        [Register("addItem", "(Lcom/google/maps/android/clustering/ClusterItem;)Z",
            "GetAddItem_Lcom_google_maps_android_clustering_ClusterItem_Handler")]
        public abstract bool AddItem(global::Java.Lang.Object p0);

        [Register("removeItem", "(Lcom/google/maps/android/clustering/ClusterItem;)Z",
            "GetRemoveItem_Lcom_google_maps_android_clustering_ClusterItem_Handler")]
        public abstract bool RemoveItem(global::Java.Lang.Object item);

        [Register("updateItem", "(Lcom/google/maps/android/clustering/ClusterItem;)Z",
            "GetUpdateItem_Lcom_google_maps_android_clustering_ClusterItem_Handler")]
        public abstract bool UpdateItem(global::Java.Lang.Object item); 
    }
    
    internal partial class AbstractAlgorithmInvoker
    {
        [Register ("removeItem", "(Lcom/google/maps/android/clustering/ClusterItem;)Z", "GetRemoveItem_Lcom_google_maps_android_clustering_ClusterItem_Handler")]
        public override unsafe bool RemoveItem (global::Java.Lang.Object item)
        {
            const string __id = "removeItem.(Lcom/google/maps/android/clustering/ClusterItem;)Z";
            IntPtr native_item = JNIEnv.ToLocalJniHandle (item);
            try {
                JniArgumentValue* __args = stackalloc JniArgumentValue [1];
                __args [0] = new JniArgumentValue (native_item);
                var __rm = _members.InstanceMethods.InvokeVirtualBooleanMethod (__id, this, __args);
                return __rm;
            } finally {
                JNIEnv.DeleteLocalRef (native_item);
                global::System.GC.KeepAlive (item);
            }
        }
        
        [Register ("addItem", "(Lcom/google/maps/android/clustering/ClusterItem;)Z", "GetAddItem_Lcom_google_maps_android_clustering_ClusterItem_Handler")]
        public override unsafe bool AddItem (global::Java.Lang.Object item)
        {
            const string __id = "addItem.(Lcom/google/maps/android/clustering/ClusterItem;)Z";
            IntPtr native_item = JNIEnv.ToLocalJniHandle (item);
            try {
                JniArgumentValue* __args = stackalloc JniArgumentValue [1];
                __args [0] = new JniArgumentValue (native_item);
                var __rm = _members.InstanceMethods.InvokeVirtualBooleanMethod (__id, this, __args);
                return __rm;
            } finally {
                JNIEnv.DeleteLocalRef (native_item);
                global::System.GC.KeepAlive (item);
            }
        }
        
        [Register ("updateItem", "(Lcom/google/maps/android/clustering/ClusterItem;)Z", "GetUpdateItem_Lcom_google_maps_android_clustering_ClusterItem_Handler")]
        public override unsafe bool UpdateItem (global::Java.Lang.Object item)
        {
            const string __id = "updateItem.(Lcom/google/maps/android/clustering/ClusterItem;)Z";
            IntPtr native_item = JNIEnv.ToLocalJniHandle (item);
            try {
                JniArgumentValue* __args = stackalloc JniArgumentValue [1];
                __args [0] = new JniArgumentValue (native_item);
                var __rm = _members.InstanceMethods.InvokeVirtualBooleanMethod (__id, this, __args);
                return __rm;
            } finally {
                JNIEnv.DeleteLocalRef (native_item);
                global::System.GC.KeepAlive (item);
            }
        }
    }
}