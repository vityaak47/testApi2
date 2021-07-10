using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace AutomationTestsExample.Tests.ApiTests.ErrorListExample
{
    [TestFixture]
    public class GetUnknownTests : APiTestBase
    {
        private const string Url = "https://reqres.in/api/unknown";
        
        [Test]
        public async Task CheckResponsUnknownGetRequestTesting()
        {
            // подготовка к отправке запроса (создание клиента, который отправит запрос)
            CancellationToken ct = new CancellationToken();
            var baseAddress = new Uri(Url);
            var client = new HttpClient { BaseAddress = baseAddress };
            
            // отправка GET запроса, получение ответа в result
            var result = await client.GetAsync(baseAddress, ct);
            
            // получение строкой содержимого ответа апи, которое лежит в Content
            var resultSting = await result.Content.ReadAsStringAsync(ct);
      
            // получаем результат в JObject формате, чтобы можно было добраться до интересующего нас поля
            var responseObj = JObject.Parse(resultSting);
            
            // проверка, что поле page == 1
            CustomAssertAreEqual(1, (int) responseObj.Root["page"], "Поле page");
            // проверка, что поле per_page == 6
            CustomAssertAreEqual(6, (int) responseObj.Root["per_page"], "Поле per_page");
            CustomAssertAreEqual(12, (int) responseObj.Root["total"], "Поле total");
            CustomAssertAreEqual(2, (int) responseObj.Root["total_pages"], "Поле total_pages");
            
            // проверка, что поле data содержит какие-то данные
            CustomAssertIsNotEmpty(responseObj.Root["data"].ToString(), "Поле data пустое");
        }
        /* Обратите внимание, что в самом тесте явно нигде не вызывает Assert! Assert вызывается 1 раз,
         после прохождения каждого теста (в методе Dispose в классе APiTestBase)  */
    }
}