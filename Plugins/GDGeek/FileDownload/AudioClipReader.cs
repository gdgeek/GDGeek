using GDGeek;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using Object = System.Object;

namespace GDGeek.FileDownload
{
    public class AudioClipReader : ArchiveReader<AudioClip, AudioType>
    {

        public override System.Threading.Tasks.Task objectToStream(Stream stream, AudioClip audio, AudioType type)
        {

            byte[] l = System.BitConverter.GetBytes(audio.name.Length);
            
            byte[] name = Convert.FromBase64String(audio.name);
            byte[] samples = System.BitConverter.GetBytes(audio.samples);
            byte[] channels = System.BitConverter.GetBytes(audio.channels);

            byte[] frequency = System.BitConverter.GetBytes(audio.frequency);
            
            byte[] data = audio.GetData();
            var t = new System.Threading.Tasks.Task(() =>
            {
                
                try
                {
                    stream.Write(l, 0, l.Length);
                    stream.Write(name, 0, name.Length);
                    stream.Write(samples, 0, samples.Length);
                    stream.Write(channels, 0, channels.Length);
                    stream.Write(frequency, 0, frequency.Length);
                    stream.Write(data, 0, data.Length);
                    stream.Flush();
                    stream.Close();
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                }

            });
            t.Start();
            return t;

        }

     

        public override System.Threading.Tasks.Task<AudioClip> streamToObject(Stream stream, AudioType type)
        {
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);

            int l = System.BitConverter.ToInt32(data, 0);
            string name = Convert.ToBase64String(data, 4, l);
            int samples = System.BitConverter.ToInt32(data, 4 + l);
            int channels = System.BitConverter.ToInt32(data, 8 + l);
            int frequency = System.BitConverter.ToInt32(data, 12 + l);

            AudioClip audio = AudioClip.Create(name, samples, channels, frequency, false);

            audio.SetData(data.Skip(16 + l).ToArray());
              
            stream.Flush();
            stream.Close();
            
            var t = new System.Threading.Tasks.Task<AudioClip>(() =>
            {
                return audio;
            });
            t.Start();
            return t;
        }
    }
   
}

