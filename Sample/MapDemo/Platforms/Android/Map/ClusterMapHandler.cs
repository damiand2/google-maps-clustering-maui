using Android.Gms.Maps;
using Maui.GoogleMaps.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapDemo.Map
{
    public partial class ClusterMapHandler : MapHandler
    {
        protected override void ConnectHandler(MapView platformView)
        {
            base.ConnectHandler(platformView);
        }

        public GoogleMap GetNativeControl() => NativeMap;
        

        

        
    }
}
