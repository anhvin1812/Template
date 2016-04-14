using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App.Services.Dtos.JsonConverters;
using Newtonsoft.Json;

namespace App.Services.Dtos.Common
{
    public static class HttpResponseMessageExtensions
    {
        public static void EnsureSuccessResult(this HttpResponseMessage response)
        {
            //response.EnsureSuccessStatusCode();
            try
            {
                var result = JsonConvert.DeserializeObject<Result>(response.Content.ReadAsStringAsync().Result,
                new ResultConverter());

                if (result != null)
                {
                    if (result is FailResult)
                    {
                        //throw ((FailResult)result).ToException();
                    }
                }
            }
            catch (JsonReaderException e)
            {
                //TODO: Remove this catch exception when all response from web api are Json
                //swallow JsonReaderException because response result might not be Json
            }

        }
    }
}
