# Welcome to GoogleMapUtils for iOS and Android
Latest Google Maps Utils SDKs bindings for iOS (4.2.2) and Android (3.5.3)

Works with:
- Xamarin.GooglePlayServices.Maps, Version="118.0.2" - Android
- Xamarin.Google.iOS.Maps, Version="6.0.1.1" - iOS

See more: 
- https://github.com/googlemaps/android-maps-utils
- https://github.com/googlemaps/google-maps-ios-utils

# Usage
These binding libraries provide very little in terms of MAUI-like abstractions over raw native libraries.
This is a design choice to pursue always latest native libraries without hassle of writing new abstractions
for each new API that appears there. Nice side effect of this is that you can consult original API docs
and quickly find out how to use newest features available there.
- Demo app with sample using clustering feature available at Sample folder in repo
- When adding reference for iOS library, please add manually bilding step in your csproj:
```xml
<Target Name="LinkWithSwift" DependsOnTargets="_ParseBundlerArguments;_DetectSdkLocations" BeforeTargets="_LinkNativeExecutable">
    <PropertyGroup>
    	<_SwiftPlatform Condition="$(RuntimeIdentifier.StartsWith('iossimulator-'))">iphonesimulator</_SwiftPlatform>
    	<_SwiftPlatform Condition="$(RuntimeIdentifier.StartsWith('ios-'))">iphoneos</_SwiftPlatform>
    </PropertyGroup>
    <ItemGroup>
    	<_CustomLinkFlags Include="-L" />
    	<_CustomLinkFlags Include="/usr/lib/swift" />
    	<_CustomLinkFlags Include="-L" />
    	<_CustomLinkFlags Include="$(_SdkDevPath)/Toolchains/XcodeDefault.xctoolchain/usr/lib/swift/$(_SwiftPlatform)" />
    	<_CustomLinkFlags Include="-Wl,-rpath" />
    	<_CustomLinkFlags Include="-Wl,/usr/lib/swift" />
    </ItemGroup>
</Target>
```
- 
# Troubleshooting 
- error while adding iOS nuget to project on Windows machine: `Could not find a part of the path '%userprofile%\.nuget\packages\gmaputils.ios\lib\net7.0-ios16.1\GoogleMapUtils.iOS.resources\GoogleMapsUtils.xcframework\ios-arm64_x86_64-simulator\GoogleMapsUtils.framework\GoogleMaps.bundle\GMSCoreResources.bundle\ic_closed_place_waypoint_alert_night_32pt@2x.png'.`
This is a windows based problem with paths longer than 260 or so characters. Move nuget cache folder to something very short like c:\nuget (https://learn.microsoft.com/en-us/nuget/consume-packages/managing-the-global-packages-and-cache-folders) 
- Unending build cycle with messages like: ` warning MSB3026: Could not copy "<nuget>\lib\net7.0-ios16.1\GoogleMapUtils.iOS.resources\GoogleMapsUtils.xcframework\ios-arm64\GoogleMapsUtils.framework\GoogleMaps.bundle\GMSCoreResources.bundle\ic_closed_place_waypoint_alert_32pt@3x.png" to "bin\Debug\net7.0-ios\iossimulator-x64\GoogleMapUtils.iOS.resources\GoogleMapsUtils.xcframework\ios-arm64\GoogleMapsUtils.framework\GoogleMaps.bundle\GMSCoreResources.bundle\ic_closed_place_waypoint_alert_32pt@3x.png". Beginning retry 4 in 1000ms. Could not find a part of the path 'bin\Debug\net7.0-ios\iossimulator-x64\GoogleMapUtils.iOS.resources\GoogleMapsUtils.xcframework\ios-arm64\GoogleMapsUtils.framework\GoogleMaps.bundle\GMSCoreResources.bundle\ic_closed_place_waypoint_alert_32pt@3x.png'.`
This is the same problem as above on windows, but this time it is related to project path. Try to place project directly beneath root folder and use short folder names.
Or build on Mac where problem does not occur.
- Error during ios build - clang exited with 1 - most probably missing swift link build step, please consult 'Usage' chapter above

