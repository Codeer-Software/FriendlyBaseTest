using System;
using System.Collections.Generic;
using System.Text;

namespace FriendlyBaseTest
{
    static class TargetPath
    {
        internal static string Path32 { get { return @"..\..\..\FriendlyBaseTargetx86N20\bin\Debug\FriendlyBaseTargetx86N20.exe"; } }
        internal static string Path64 { get { return @"..\..\..\FriendlyBaseTargetx64N20\bin\Debug\FriendlyBaseTargetx64N20.exe"; } }
        internal static string PathMfc { get { return @"..\..\..\Debug\MfcTestTarget.exe"; } }
        internal static string PathWpf32 { get { return @"..\..\..\WpfTestTargetx86N40\bin\Debug\WpfTestTargetx86N40.exe"; } }
        internal static string PathWpf64{ get { return @"..\..\..\WpfTestTargetx64N40\bin\Debug\WpfTestTargetx64N40.exe"; } }
        internal static string PathExpandNative{ get { return @"..\..\..\Debug\ExpandTestTargetNative.dll"; } }
    }
}
