using CoreAnimation;
using CoreGraphics;
using CoreLocation;
using Foundation;
using GMapUtils.iOS;
using Google.Maps;
using Maui.GoogleMaps.iOS.Factories;
using Maui.GoogleMaps;
using UIKit;

namespace MapDemo.Platforms.iOS.Map
{
    internal class ClusterRendererHandler : DefaultClusterRenderer
    {
        private const double animationDuration = 0.5;

        private readonly MapView nativeMap;
        //private readonly uint minimumClusterSize;
        //private float maxClusterZoom = 20;
        private Dictionary<ICluster, uint> countCache = new Dictionary<ICluster, uint>();

        public ClusterRendererHandler(MapView mapView, ClusterIconGenerator iconGenerator, int minimumClusterSize)
            : base(mapView, iconGenerator)
        {
            nativeMap = mapView;
            MinimumClusterSize = (uint)minimumClusterSize;
        }

        public Marker GetMarker(IosClusterItem clusteredMarker) =>
            Markers?.FirstOrDefault(m => ReferenceEquals(m.UserData as IosClusterItem, clusteredMarker));

        

        /*public override bool ShouldRenderAsCluster(ICluster cluster, float zoom)
        {

            /*if(countCache.TryGetValue(cluster, out var count) == false)
            {
                count = cluster.Count.ToUInt32();
                countCache.Add(cluster, count);
                
            }
            return cluster.Count > (uint)minimumClusterSize && zoom <= maxClusterZoom;
        }*/

        [Export("markerWithPosition:from:userData:clusterIcon:animated:")]
        public Marker MarkerWithPosition(CLLocationCoordinate2D position, CLLocationCoordinate2D from,
            NSObject userData, UIImage clusterIcon,
            bool animated)
        {
            var initialPosition = animated ? from : position;
            var marker = userData is IosClusterItem ?
                CreateSinglePin(userData, initialPosition)
                : CreateGroup(clusterIcon, initialPosition);

            marker.Position = initialPosition;
            marker.UserData = userData;

            marker.Map = nativeMap;

            if (animated)
                AnimateMarker(position, marker);
            return marker;
        }

        private static Marker CreateGroup(UIImage clusterIcon, CLLocationCoordinate2D initialPosition)
        {
            var marker = Marker.FromPosition(initialPosition);
            marker.Icon = clusterIcon;
            marker.GroundAnchor = new CGPoint(0.5, 0.5);
            marker.ZIndex = 1;
            return marker;
        }

        private static Marker CreateSinglePin(NSObject userData, CLLocationCoordinate2D initialPosition)
        {
            var clusteredMarker = userData as IosClusterItem;
            var clusterItem = clusteredMarker.Item;
            var marker = Marker.FromPosition(initialPosition);
            marker.Icon = DefaultImageFactory.Instance.ToUIImage(BitmapDescriptorFactory.FromBundle(clusterItem.IconImageName));
            marker.Title = clusteredMarker.Title;
            marker.Snippet = clusteredMarker.Snippet;
            marker.Draggable = clusteredMarker.Draggable;
            marker.Rotation = clusteredMarker.Rotation;
            marker.GroundAnchor = clusteredMarker.GroundAnchor;
            marker.InfoWindowAnchor = clusteredMarker.InfoWindowAnchor;
            marker.Flat = clusteredMarker.Flat;
            marker.Opacity = clusteredMarker.Opacity;
            marker.ZIndex = clusteredMarker.ZIndex;
            return marker;
        }

        internal void ChangeMarkerIcon(ClusterMarker item, string newIconName)
        {
            var dci = (IosClusterItem)item.NativeObject;
            if (dci == null)
                return;

            item.IconImageName = newIconName;
            var icon = DefaultImageFactory.Instance.ToUIImage(BitmapDescriptorFactory.FromBundle(newIconName));
            dci.Icon = icon;
            var marker = GetMarker(dci);
            if (marker == null)
                return;
            marker.Icon = icon;
        }

        private static void AnimateMarker(CLLocationCoordinate2D position, Marker marker)
        {
            CATransaction.Begin();
            CATransaction.AnimationDuration = animationDuration;
            marker.Layer.Latitude = position.Latitude;
            marker.Layer.Longitude = position.Longitude;
            CATransaction.Commit();
        }
    }
}
