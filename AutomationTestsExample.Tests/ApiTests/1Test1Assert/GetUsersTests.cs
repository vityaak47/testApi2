using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace AutomationTestsExample.Tests.ApiTests._1Test1Assert
{
    [TestFixture] // Атрибут,указывающий,что это тестовый класс
    public class GetUsersTests
    {
        private ResponseGet _responseObj;
        private HttpResponseMessage _httpResponse;

        private const string Host = "https://reqres.in";
        private const string Api = "/api/users/2";
        
        [OneTimeSetUp] // Говорит о том,что данный участок кода (а именно отправка апи запроса и получение ответа) выполняется 1 раз перед прогонкой всех тестов в класе
        public async Task SendGetRequest()
        {
            // Распространяет уведомление о том, что операции следует отменить.
            CancellationToken ct = new CancellationToken();

            // универсальный код ресурса (URI) - куда нужно отправлять запрос
            var baseAddress = new Uri(Host + Api);

            // инициализируем системный класс для отправки http запросов
            // using указывает на то, что после выполнения данного участка кода в {} нужно освободить ресурсы, которые захватил объект
            using (var client = new HttpClient { BaseAddress = baseAddress })
            {
                // Отправка запроса GET по указанному URI в качестве асинхронной операции.
                _httpResponse = await client.GetAsync(baseAddress, ct);

                // Конвертируем содержимое ответа (result) в строку
                var responseString = await _httpResponse.Content.ReadAsStringAsync(ct);

                // Преобразуем (десериализуем) строку в json (Получаем экзепляр класса ResponseGet, в который помещаются все данные из полученного json)
                _responseObj = JsonConvert.DeserializeObject<ResponseGet>(responseString);
    
                /*
                _responseObj = null; // Можно раскомментировать,чтобы понять поведение теста в случае, если _responseObj == null
                */
                
                // Проверяем, что в _responseObj что-то записалось, чтобы дальнейшим тестам было с чем работать
                if (_responseObj == null)
                {
                    Assert.Fail($"Ответ от api {Api} == null, выполнение следующих проверок не имеет смысла! ");
                }
            }
        }

        [Test]
        public void CheckResponseCodeGetUsersTesting()
        {
            //Проверяем, что статус ответа API == ОК 
            Assert.AreEqual(HttpStatusCode.OK, _httpResponse.StatusCode, "полученный code некорректен");
        }
        
        [Test]
        public void CheckResponseFirstNameGetUsersTesting()
        {
            // Проверка,что поле first_name в data == "Janet"
             if (_responseObj.Data != null)
                Assert.AreEqual("Janet", _responseObj.Data.FirstName, "полученное значение поля FirstName некорректено");
        }
        
        [Test]
        public void CheckResponseEmailGetUsersTesting()
        {
            // Проверка,что поле first_name в data == "Janet"
            if (_responseObj.Data != null)
                Assert.AreEqual("janet.weaver@reqres.in", _responseObj.Data.Email, "полученное значение поля Email некорректено");
        }
    }
}