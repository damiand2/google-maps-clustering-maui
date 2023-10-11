using CoreGraphics;
using Foundation;
using GMapUtils.iOS;
using Microsoft.Maui.Platform;
using UIKit;

namespace MapDemo.Platforms.iOS.Map
{
    internal class ClusterIconGeneratorHandler : DefaultClusterIconGenerator
    {
        private readonly NSCache iconCache;
        private int[] buckets = new int[] { 10, 50, 100, 500, 1000, 5000, 10000 };

        public ClusterIconGeneratorHandler()
        {
            iconCache = new NSCache();           
        }

        public override UIImage IconForSize(nuint size)
        {
            string text = null;
            nuint bucketIndex = 0;
            bucketIndex = BucketIndexForSize((nint)size);
            text = size < (nuint)buckets[0] ? size.ToString() : $"{buckets[bucketIndex]}+";


          
            return GetIconForText(text, bucketIndex);
        }

        private nuint BucketIndexForSize(nint size)
        {
            uint index = 0;
           

            while (index + 1 < buckets.Length && buckets[index + 1] <= size)
                ++index;

            return index;
        }

        private UIImage GetIconForText(string text, UIImage baseImage)
        {
            var nsText = new NSString(text);
            var icon = iconCache.ObjectForKey(nsText);
            if (icon != null)
                return (UIImage)icon;

            var font = UIFont.BoldSystemFontOfSize(12);
            var size = baseImage.Size;
            UIGraphics.BeginImageContextWithOptions(size, false, 0.0f);
            baseImage.Draw(new CGRect(0, 0, size.Width, size.Height));
            var rect = new CGRect(0, 0, baseImage.Size.Width, baseImage.Size.Height);

            var paragraphStyle = NSParagraphStyle.Default;
            var attributes = new UIStringAttributes(NSDictionary.FromObjectsAndKeys(
                objects: new NSObject[] { font, paragraphStyle, Colors.White.ToPlatform() },
                keys: new NSObject[] { UIStringAttributeKey.Font, UIStringAttributeKey.ParagraphStyle, UIStringAttributeKey.ForegroundColor }
            ));

            var textSize = nsText.GetSizeUsingAttributes(attributes);
            var textRect = rect.Inset((rect.Size.Width - textSize.Width) / 2,
                (rect.Size.Height - textSize.Height) / 2);
            //nsText.DrawString(RectangleFExtensions.Integral(textRect), attributes);
            nsText.DrawString(textRect.Integral(), attributes);

            var newImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            iconCache.SetObjectforKey(newImage, nsText);
            return newImage;
        }

        private UIImage GetIconForText(string text, nuint bucketIndex)
        {
            var nsText = new NSString(text);
            var icon = iconCache.ObjectForKey(nsText);
            if (icon != null)
                return (UIImage)icon;

            var font = UIFont.BoldSystemFontOfSize(14);
            var paragraphStyle = NSParagraphStyle.Default;
            var dict = NSDictionary.FromObjectsAndKeys(
                objects: new NSObject[] { font, paragraphStyle, Colors.White.ToPlatform() },
                keys: new NSObject[] { UIStringAttributeKey.Font, UIStringAttributeKey.ParagraphStyle, UIStringAttributeKey.ForegroundColor }
            );
            var attributes = new UIStringAttributes(dict);


            var textSize = nsText.GetSizeUsingAttributes(attributes);
            var rectDimension = Math.Max(20, Math.Max(textSize.Width, textSize.Height)) + 3 * bucketIndex + 6;
            var rect = new CGRect(0.0f, 0.0f, rectDimension, rectDimension);

            UIGraphics.BeginImageContext(rect.Size);
            UIGraphics.BeginImageContextWithOptions(rect.Size, false, 0);

            var ctx = UIGraphics.GetCurrentContext();
            ctx.SaveState();

          
            ctx.SetFillColor(Colors.Blue.ToCGColor());
            ctx.FillEllipseInRect(rect);
            ctx.RestoreState();

            UIColor.White.SetColor();
            var textRect = rect.Inset((rect.Size.Width - textSize.Width) / 2,
                (rect.Size.Height - textSize.Height) / 2);
            nsText.DrawString(textRect.Integral(), attributes);

            var newImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            iconCache.SetObjectforKey(newImage, nsText);

            return newImage;
        }
    }
}
