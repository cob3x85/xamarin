﻿using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Threading.Tasks;

namespace Emotions
{
  public class ImageService
  {
    public static async Task<MediaFile> TakePicture(bool useCam = true)
    {
      await CrossMedia.Current.Initialize();

      if (useCam)
      {
        if(!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
        {
          return null;
        }
      }

      var file = useCam ? await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
      {
        Directory = "Championship",
        Name = "Reto6_test.jpg"
      })
      : await CrossMedia.Current.PickPhotoAsync();

      return file;
    }
  }
}
