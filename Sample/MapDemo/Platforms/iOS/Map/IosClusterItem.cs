using GMapUtils.iOS;
using Google.Maps;
using CoreLocation;

namespace MapDemo.Platforms.iOS.Map
{
    public class IosClusterItem : Marker, IClusterItem
    {
        public ClusterMarker Item;

        public IosClusterItem(ClusterMarker item)
        {
            Item = item;
            Position = new CLLocationCoordinate2D(item.Latitude, item.Longitude);
            Item.NativeObject = this;
        }
    }

    


}
