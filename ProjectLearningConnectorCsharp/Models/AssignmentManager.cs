using ProjectLearningConnectorCsharp.Models.AssignmentType;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjectLearningConnectorCsharp.Models
{
    class AssignmentManager
    {
        public string AssignmentId { get; set; }
        public static JsonSerializerOptions Options { get; set; }
        public AssignmentManager(string assignmentId)
        {
            Options = new JsonSerializerOptions();
            Options.Converters.Add(new ObjectToInferredTypesConverter());
            AssignmentId = assignmentId;
        }

        public dynamic RunAssignment(string jsonString)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new ObjectToInferredTypesConverter());


            List<List<dynamic>> incomingResultValues = DeserializeValueTypeArray(JsonDocument.Parse(jsonString));

            int numberOfTests = incomingResultValues.Count;
            int numberOfParameters = incomingResultValues[0].Count;

            dynamic[] returnValues = new dynamic[numberOfTests];

            // Number of tests
            for (int i = 0; i < numberOfTests; i++)
            {
                dynamic[] parametersArray = new dynamic[numberOfParameters];
                // Number of parameters

                for (int y = 0; y < numberOfParameters; y++)
                {
                    var value = incomingResultValues[i][y];

                    if (value is JsonElement)
                    {
                        JsonElement[] result = JsonSerializer.Deserialize<JsonElement[]>(value.ToString(), Options);

                        dynamic[] arrayValues = new dynamic[result.Length];

                        for (int p = 0; p < result.Length; p++)
                        {
                            arrayValues[p] = DeserializeValueType(result[p]);
                        }

                        if (arrayValues[i] is Int32)
                        {
                            parametersArray[y] = Array.ConvertAll(arrayValues, (o) => (int)o);
                        }
                        else
                        {
                            parametersArray[y] = arrayValues;
                        }

                    }
                    else
                    {
                        parametersArray[y] = value;
                    }
                }
                var returnValue = Type.GetType("ProjectLearningConnectorCsharp.Program").GetMethod("Test").Invoke(null, parametersArray);

                if (returnValue is not Array)
                {
                    Console.WriteLine();
                    dynamic[] returnValueInArray = new dynamic[1];
                    returnValueInArray[0] = returnValue;
                    returnValues[i] = returnValueInArray;
                }
                else
                {

                    returnValues[i] = returnValue;
                }
            }

            return returnValues;
        }

        private static List<List<dynamic>> DeserializeValueTypeArray(JsonDocument valueType)
        {

            JsonDocument[] result = JsonSerializer.Deserialize<JsonDocument[]>(valueType.RootElement.ToString());

            List<List<dynamic>> returnValues = new();

            foreach (JsonDocument element in result)
            {
                JsonDocument[] innerResult = JsonSerializer.Deserialize<JsonDocument[]>(element.RootElement.ToString());

                List<dynamic> dynamics = new();
                foreach (JsonDocument item in innerResult)
                {
                    dynamics.Add(DeserializeValueType(item.RootElement));

                }
                returnValues.Add(dynamics);

            }

            return returnValues;
        }

        private static dynamic DeserializeValueType(JsonElement valueType)
        {
            if (valueType.ValueKind == JsonValueKind.String)
            {
                return valueType.GetString();
            }

          

            var result = JsonSerializer.Deserialize<dynamic>(valueType.ToString(), Options);

            return result;
        }
    }

  

    public class ObjectToInferredTypesConverter : JsonConverter<object>
    {
        public override object Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) => reader.TokenType switch
            {
                JsonTokenType.True => true,
                JsonTokenType.False => false,
                JsonTokenType.Number when reader.TryGetInt32(out int l) => l,
                JsonTokenType.Number => reader.GetDouble(),
                JsonTokenType.String when reader.TryGetDateTime(out DateTime datetime) => datetime,
                JsonTokenType.String => reader.GetString(),
                _ => JsonDocument.ParseValue(ref reader).RootElement.Clone()
            };

        public override void Write(
            Utf8JsonWriter writer,
            object objectToWrite,
            JsonSerializerOptions options) =>
            JsonSerializer.Serialize(writer, objectToWrite, objectToWrite.GetType(), options);
    }
}
