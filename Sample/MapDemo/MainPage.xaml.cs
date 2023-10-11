
using Maui.GoogleMaps;
using MapDemo.Map;
#if ANDROID
using MapDemo.Platforms.Android.Map;
using Android.Gms.Maps;
#elif IOS
using Google.Maps;
using MapDemo.Platforms.iOS.Map;
#endif

namespace MapDemo
{
    public partial class MainPage : ContentPage
    {
#if ANDROID
        private ClusterLogic clusterLogic;
#elif IOS
        private ClusterLogic clusterLogic;
#endif
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var position = new Position(49.804, 15.475);
            var latestMapRegion = new MapSpan(position, 2.504, 6.769);

            map.MoveToRegion(latestMapRegion);
            map.CameraIdled +=Map_CameraIdled;
        }

        private void Map_CameraIdled(object sender, CameraIdledEventArgs e)
        {

            map.CameraIdled -=Map_CameraIdled;

#if ANDROID

            clusterLogic = new ClusterLogic(((ClusterMapHandler)map.Handler).GetNativeControl(), map);
#elif IOS
            clusterLogic = new ClusterLogic((MapView)map.Handler.PlatformView, map);
#endif
            List<ClusterMarker> markers = new List<ClusterMarker>();
            for (int i = 0; i < 2000; i++)
            {
                markers.Add(new ClusterMarker(
                    i.ToString(),
                    i.ToString(),
                    i % 2 ==0 ? 49.804 + Random.Shared.NextDouble() : 49.804 - Random.Shared.NextDouble(),
                    i % 2 ==0 ? 15.475 + (Random.Shared.NextDouble() * 2) : 15.475 - (Random.Shared.NextDouble() * 2),
                    "dotnet_bot"
                ));
            }
#if ANDROID
            clusterLogic.ClusterItems(markers);
#elif IOS
            clusterLogic.ClusterItems(markers);
#endif

        }
    }
}