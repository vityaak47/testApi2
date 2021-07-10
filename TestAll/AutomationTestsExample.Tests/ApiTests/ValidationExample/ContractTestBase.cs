using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;

namespace AutomationTestsExample.Tests.ApiTests.ValidationExample
{
    public class ContractTestBase
    {
        public static void CheckResponse(
            [NotNull] JObject jObject,
            string fileName)
        {
            // получаем наш файл getUsers.Positive.json с контрактом и преобразуем его в формат JSchema
            var schema = GetJSchemaByFile(@"\ApiTests\ValidationExample\contracts\", fileName);

            if (jObject == null) throw new ArgumentNullException(nameof(jObject));
            if (schema == null) throw new ArgumentNullException(nameof(schema));

            // метод IsValid проверяет соответствует ли ответ апи (jObject) нашему контрактному файлу getUsers.Positive.json (schema)
            var valid = jObject.IsValid(schema, out IList<string> messages);
            
            Assert.IsTrue(valid,
                $"Полученный json невалиден. Невалидные поля {string.Join(", ", messages.ToArray())}");
        }
        
        public static JSchema GetJSchemaByFile(string relativePath, string fileName)
        {
            //Находим путь к файлу
            var direct = Directory.GetCurrentDirectory();
            
            // Убираем из этого пути лишнее (все,что после bin) и заменяем его на relativePath
            var path = direct.Substring(0,
                direct.IndexOf(@"\bin\", StringComparison.Ordinal)) + relativePath;
            /*
             Преобразуем наш файл getUsers.Positive.json в формат JSchema (для этого мы сначала считаем файл в строку)
             Документацию по JSON Schema Validation
             можно посмотреть тут - https://json-schema.org/draft/2019-09/json-schema-validation.html
              */
            return JSchema.Parse(File.ReadAllText($@"{path}{fileName}"));
        }
    }
}