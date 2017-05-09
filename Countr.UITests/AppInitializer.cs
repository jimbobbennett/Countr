using Xamarin.UITest;

namespace Countr.UITests
{
   public class AppInitializer
   {
      public static IApp StartApp(Platform platform)
      {
         if (platform == Platform.Android)
         {
            return ConfigureApp
               .Android
               .EnableLocalScreenshots()
               .ApkFile("../../../Countr.Droid/bin/Release/io.jimbobbennett.Countr-Signed.apk")
               .StartApp();
         }

         return ConfigureApp
            .iOS
            .EnableLocalScreenshots()
            .StartApp();
      }
   }
}
