using Microsoft.ProjectOxford.Emotion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emotions
{
  public class ServiceEmotions
  {
    static string key = "6eb1881847f74e65b85a30b3c366d3ba";
    public static async Task<Dictionary<string, float>> GetEmotions(Stream stream)
    {
      EmotionServiceClient client = new EmotionServiceClient(key);
      var emotions = await client.RecognizeAsync(stream);
      if (emotions?.Count() == 0)
      {
        return null;
      }

      return emotions[0].Scores.ToRankedList().ToDictionary(x => x.Key, x => x.Value);
    }
  }
}
