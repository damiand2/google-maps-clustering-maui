using Maui.GoogleMaps;
using GMapUtils.iOS;
using Google.Maps;


namespace MapDemo.Platforms.iOS.Map
{
    public class ClusterLogic
    {
        private MapView nativeMap;
        private Maui.GoogleMaps.Map map;
        private ClusterRendererHandler clusterRenderer;
        private ClusterManager clusterManager;
        private static IClusterAlgorithm algorithm;
        private static bool itemAddedToAlgorithm = false;

        public ClusterLogic(MapView nativeMap, Maui.GoogleMaps.Map map)
        {
            this.nativeMap=nativeMap;
            this.map=map;
            if (nativeMap == null) { return; }
           
            if (algorithm == null)
                algorithm = GetClusterAlgorithm(map);

            var iconGenerator = new ClusterIconGeneratorHandler();
            clusterRenderer = new ClusterRendererHandler(nativeMap, iconGenerator, 5);
            clusterManager = new ClusterManager(nativeMap, algorithm, clusterRenderer);
            nativeMap.TappedMarker = HandleGmsTappedMarker;            
        }

        public void CameraIdled()
        {
            clusterManager.Cluster();
        }

        public void ChangeClusterItemIcon(ClusterMarker marker, string iconName)
        {
            clusterRenderer.ChangeMarkerIcon(marker, iconName);
        }

        private bool HandleGmsTappedMarker(MapView mapView, Marker marker)
        {
            if (marker?.UserData is ICluster cluster)
            {
                var pins = GetClusterPins(cluster);
                var clusterPosition = new Position(cluster.Position.Latitude, cluster.Position.Longitude);

                
                return true;

            }
            var targetPin = marker.UserData as IosClusterItem;

            if (targetPin != null)
            {                
                
                return true;                
            }
            return false;
        }

        private List<ClusterMarker> GetClusterPins(ICluster cluster)
        {
            var pins = new List<ClusterMarker>();
            foreach (var item in cluster.Items)
            {
                var clusterItem = (IosClusterItem)item;
                pins.Add(clusterItem.Item);
            }

            return pins;
        }



        public void ClusterItems(List<ClusterMarker> list)
        {
            if (algorithm != null && itemAddedToAlgorithm == true)
            {
                clusterManager.ClearItems();
            }
            if (list == null || list.Count == 0)
            {
                itemAddedToAlgorithm = false;
                return;
            }
                
            var newList = new List<IosClusterItem>();
            foreach (var item in list)
            {
                var ci = new IosClusterItem(item);
                newList.Add(ci);
            }
            itemAddedToAlgorithm = true;
            clusterManager.AddItems(newList.ToArray());
            clusterManager.Cluster();
        }
        

        private IClusterAlgorithm GetClusterAlgorithm(Maui.GoogleMaps.Map newMap)
        {
            var algorithm  = new NonHierarchicalDistanceBasedAlgorithm();            

            return algorithm;
        }
    }
}
