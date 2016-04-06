using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Services.Dtos.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App.Services.Dtos.JsonConverters
{
    public class ResultConverter : JsonCreationConverter<Result>
    {
        protected override Result Create(Type objectType, JObject jObject)
        {
            if (FieldExists("Errors", jObject))
            {
                return new FailResult();
            }
            else if (FieldExists("Data", jObject))
            {
                return new SuccessResult();
            }
            else
            {
                return null;
            }
        }

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                         object existingValue,
                                         JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
            Result target = Create(objectType, jObject);

            // Populate the object properties
            if (target != null)
                serializer.Populate(jObject.CreateReader(), target);

            return target;
        }

        private bool FieldExists(string fieldName, JObject jObject)
        {
            return jObject[fieldName] != null;
        }
    }
}
