using Com.Google.Maps.Android.Clustering;
using Android.Gms.Maps.Model;
using Java.Lang;

namespace MapDemo.Platforms.Android.Map
{
    public class DroidClusterItem : global::Java.Lang.Object, IClusterItem
    {
        public DroidClusterItem(ClusterMarker item)
        {
            Position = new LatLng(item.Latitude, item.Longitude);
            this.Item = item;
            item.NativeObject = this;            
        }

        public LatLng Position { get; }

        public string Snippet => string.Empty;

        public string Title => Item.Title;

        public Float ZIndex => (Java.Lang.Float)1.0f;

        public readonly ClusterMarker Item;
    }
}
