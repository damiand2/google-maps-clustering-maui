using Android.Content;
using Android.Gms.Maps.Model;
using Android.Gms.Maps;
using Com.Google.Maps.Android.Clustering.View;
using Com.Google.Maps.Android.Clustering;
using Maui.GoogleMaps.Android.Factories;
using NativeBitmapDescriptor = Android.Gms.Maps.Model.BitmapDescriptor;


namespace MapDemo.Platforms.Android.Map
{
    public class ClusterRenderer : DefaultClusterRenderer
    {
        private readonly Maui.GoogleMaps.Map map;
        private readonly Dictionary<string, NativeBitmapDescriptor> enabledBucketsCache;        
        private int[] buckets = new int[] { 10, 50, 100, 500, 1000, 5000, 10000 };
        private Dictionary<string, NativeBitmapDescriptor> iconCache = new Dictionary<string, NativeBitmapDescriptor>();

        public ClusterRenderer(Context context,
            Maui.GoogleMaps.Map map,
            GoogleMap nativeMap,
            ClusterManager manager)
            : base(context, nativeMap, manager)
        {
            this.map = map;         
            enabledBucketsCache = new Dictionary<string, NativeBitmapDescriptor>();
        }        

        

        private NativeBitmapDescriptor GetIcon(Maui.GoogleMaps.BitmapDescriptor descriptor)
        {            
            iconCache.TryGetValue(descriptor.Id, out var bitmapDescriptor);
            if (bitmapDescriptor == null)
            {
                var bitmapDescriptorFactory = DefaultBitmapDescriptorFactory.Instance;
                bitmapDescriptor = bitmapDescriptorFactory.ToNative(descriptor);
                iconCache.Add(descriptor.Id, bitmapDescriptor);
            }
            return bitmapDescriptor;
        }

        private NativeBitmapDescriptor GetIcon(ICluster cluster, Maui.GoogleMaps.BitmapDescriptor descriptor)
        {            
            var icon = GetFromIconCache(cluster);
            if (icon == null)
            {
                var bitmapDescriptorFactory = DefaultBitmapDescriptorFactory.Instance;
                icon = bitmapDescriptorFactory.ToNative(descriptor);
                AddToIconCache(cluster, icon);
            }
            return icon;
        }

        private NativeBitmapDescriptor GetFromIconCache(ICluster cluster)
        {
            NativeBitmapDescriptor bitmapDescriptor;
            var clusterText = GetClusterText(cluster);
            enabledBucketsCache.TryGetValue(clusterText, out bitmapDescriptor);
            
            return bitmapDescriptor;
        }

        private void AddToIconCache(ICluster cluster, NativeBitmapDescriptor icon)
        {
            var clusterText = GetClusterText(cluster); 
            enabledBucketsCache.Add(clusterText, icon);          
        }        

        internal void ChangeMarkerIcon(DroidClusterItem item, string newIconName)
        {
            var marker = GetMarker(item);

            if (item == null)
                return;
            marker.SetIcon(GetIcon(Maui.GoogleMaps.BitmapDescriptorFactory.FromBundle(newIconName)));
        }

        protected override void OnBeforeClusterItemRendered(Java.Lang.Object marker, MarkerOptions options)
        {
            var clusteredMarker = marker as DroidClusterItem;
            if (clusteredMarker != null)
            {
                options.SetTitle(clusteredMarker.Title)
                .SetSnippet(clusteredMarker.Snippet);
                if (clusteredMarker.Item.IconImageName != null)
                {
                    options.SetIcon(GetIcon(Maui.GoogleMaps.BitmapDescriptorFactory.FromBundle(clusteredMarker.Item.IconImageName)));

                }
            }

            base.OnBeforeClusterItemRendered(marker, options);
        }

        protected override int GetBucket(ICluster cluster)
        {
            var size = cluster.Size;
            if (size <= buckets[0])
                return size;
            return buckets[BucketIndexForSize(cluster.Size)];
        }

        //public override int GetColor(int size)
        //{
        //    return bucketColors[BucketIndexForSize(size)].ToInt();
        //}

        private string GetClusterText(ICluster cluster)
        {
            return GetClusterText(cluster.Size);
        }

        protected override string GetClusterText(int size)
        {
            string result;           
            var bucketIndex = BucketIndexForSize(size);

            result = size < buckets[0] ? size.ToString() : $"{buckets[bucketIndex]}+";


            return result;
        }

        private int BucketIndexForSize(int size)
        {
            uint index = 0;           

            while (index + 1 < buckets.Length && buckets[index + 1] <= size)
                ++index;

            return (int)index;
        }
    }
}
