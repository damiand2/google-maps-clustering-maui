using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapDemo
{
    public class ClusterMarker
    {


        public ClusterMarker(string id, string title, double latitude, double longitude, string iconImageName)
        {
            Id = id;
            Title = title;
            Latitude = latitude;
            Longitude = longitude;
            IconImageName = iconImageName;
        }

        public string Id;
        public string Title;
        public double Latitude;
        public double Longitude;
        public string IconImageName;
        internal IDisposable NativeObject;
    }

    public enum ClusterAlgorithm
    {
        NonHierarchicalDistanceBased,
        GridBased,
        /// <summary>
        /// Android only
        /// </summary>
        VisibleNonHierarchicalDistanceBased
    }

    
}
