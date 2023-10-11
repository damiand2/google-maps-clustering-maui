using Android.Gms.Maps;
using Com.Google.Maps.Android.Clustering;
using Com.Google.Maps.Android.Clustering.Algo;
using Maui.GoogleMaps;
using static Com.Google.Maps.Android.Clustering.ClusterManager;

namespace MapDemo.Platforms.Android.Map
{
    public class ClusterLogic : Java.Lang.Object, IOnClusterItemClickListener, IOnClusterClickListener
    {
        private static IAlgorithm algorithm;
        private GoogleMap nativeMap;
        private Maui.GoogleMaps.Map map;
        private ClusterManager clusterManager;
        private ClusterRenderer clusterRenderer;

        public ClusterLogic(GoogleMap nativeMap, Maui.GoogleMaps.Map map)
        {
            this.nativeMap=nativeMap;
            this.map=map;
            if (nativeMap == null) { return; }
            
            if (algorithm == null)
                algorithm = GetClusterAlgorithm(map);
            var activity = Platform.CurrentActivity;
            clusterManager = new ClusterManager(activity, nativeMap) { Algorithm = algorithm };
            clusterRenderer = new ClusterRenderer(activity,
                map,
                nativeMap,
                clusterManager);
            clusterManager.Renderer = clusterRenderer;

            nativeMap.SetOnCameraIdleListener(clusterManager);
            nativeMap.SetOnMarkerClickListener(clusterManager);
            
            clusterManager.SetOnClusterItemClickListener(this);
            clusterManager.SetOnClusterClickListener(this);            
        }

        

        public void CameraIdled()
        {
            if (clusterManager == null) return;
            clusterManager.Cluster();
        }

        public void ClusterItems(List<ClusterMarker> list)
        {
            if (clusterManager == null)
            {

                return;
            }
            if (algorithm != null && algorithm.Items.Count > 0)
            {
                clusterManager.ClearItems();
            }
            
            if (list == null || list.Count == 0)
                return;
            for (int i = 0; i < list.Count; i++)
            {
                var clusterItem = list[i];
                
                var offsetItem = new DroidClusterItem(clusterItem);
                clusterManager.AddItem(offsetItem);
            }
            clusterManager.Cluster();
        }     
       
        

        private IAlgorithm GetClusterAlgorithm(Maui.GoogleMaps.Map newMap)
        {
            IAlgorithm algorithm  = new NonHierarchicalDistanceBasedAlgorithm();            

            return algorithm;
        }

        public bool OnClusterItemClick(Java.Lang.Object nativeItemObj)
        {
            var clickedItem = nativeItemObj as DroidClusterItem;
           
            return true;
        }

        public void ChangeClusterItemIcon(ClusterMarker marker, string iconName)
        {
            var renderer = clusterManager.Renderer as ClusterRenderer;
            renderer.ChangeMarkerIcon((DroidClusterItem)marker.NativeObject, iconName);
        }

        public bool OnClusterClick(ICluster cluster)
        {
            var pins = GetClusterPins(cluster);
            var clusterPosition = new Position(cluster.Position.Latitude, cluster.Position.Longitude);           
            return true;
        }

        private List<ClusterMarker> GetClusterPins(ICluster cluster)
        {
            var pins = new List<ClusterMarker>();
            foreach (var item in cluster.Items)
            {
                var clusterItem = (DroidClusterItem)item;
                pins.Add(clusterItem.Item);
            }

            return pins;
        }       
    }
}
