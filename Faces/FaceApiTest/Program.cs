﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FaceApiTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var imagePath = @"image2.jpg";
            var urlAddress = "http://localhost:6001/api/faces";
            ImageUtility imgUtil = new ImageUtility();
            var bytes = imgUtil.ConvertToBytes(imagePath);
            List<byte[]> faceList = null;
            var byteContent = new ByteArrayContent(bytes);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            using (var httpClient=new HttpClient())
            {
                using (var response=await httpClient.PostAsync(urlAddress,byteContent))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    faceList = JsonConvert.DeserializeObject<List<byte[]>>(apiResponse);
                }
            }
            if (faceList.Count>0)
            {
                for (int i = 0; i < faceList.Count; i++)
                {
                    imgUtil.FromBytesToImage(faceList[i], "faceSavedByFaceTestAPI" + i);
                }
            }
        }
    }
}
