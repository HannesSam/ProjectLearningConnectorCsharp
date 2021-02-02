using ProjectLearningConnectorCsharp.Models.AssignmentType;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjectLearningConnectorCsharp.Models
{
    class AssignmentManager
    {
        public string AssignmentId { get; set; }

        public AssignmentManager(string assignmentId)
        {
            AssignmentId = assignmentId;
        }

        public dynamic RunAssignment(string jsonString)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new ObjectToInferredTypesConverter());

            dynamic assignmentObject = JsonSerializer.Deserialize<dynamic[][]>(jsonString, options);
            int numberOfParameters = assignmentObject[0].Length;
            int numberOfTests = assignmentObject.Length;
            dynamic[] returnValues = new dynamic[numberOfTests];


            for (int i = 0; i < numberOfTests; i++)
            {
            dynamic[] parametersArray = new dynamic[numberOfParameters];
            for (int y = 0; y < numberOfParameters; y++)
                {
                parametersArray[y] = assignmentObject[i][y];
                }
                returnValues[i] = Type.GetType("ProjectLearningConnectorCsharp.Program").GetMethod("Test").Invoke(null, parametersArray);               
            }


            //Console.WriteLine(Type.GetType("ProjectLearningConnectorCsharp.Program").GetMethod("Test").Invoke(null, parametersArray));
            //switch (switchCode)
            //{
            //    case 1:
            //        int test1 = assignmentObject.Array[0][0];
            //        int test2 = assignmentObject.Array[0][1];
            //        object[] parametersArray = new object[] { test1, test2 };
            //        Console.WriteLine(Type.GetType("ProjectLearningConnectorCsharp.Program").GetMethod("Test").Invoke(null, parametersArray));
            //        break;
            //    default:
            //        break;
            //}
            
            //IF simple assignment the return is always an ResultArray
            return returnValues;
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
