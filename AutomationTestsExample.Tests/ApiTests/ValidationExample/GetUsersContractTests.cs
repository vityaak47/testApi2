using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutomationTestsExample.Tests.ApiTests._1Test1Assert;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace AutomationTestsExample.Tests.ApiTests.ValidationExample
{
    [TestFixture]
    public class GetUsersContractTests : ContractTestBase
    {
        
        [Test]
        [TestCase(1)] // тест для page = 1
        [TestCase(2)] // тест для page = 2
        [TestCase(3)] // тест для page = 3
        public async Task GetUsersContractTesting(int page)
        {
            CancellationToken ct = new CancellationToken();
            var baseAddress = new Uri($"https://reqres.in/api/users?page={page}");
            var client = new HttpClient { BaseAddress = baseAddress };
            
            var result = await client.GetAsync(baseAddress, ct);
            
            // преобразуем содержимое ответа апи в строку
            var resultSting = await result.Content.ReadAsStringAsync(ct);
      
            // преобразуем стринговый ответ от апи в JObject
            var responseObj = JObject.Parse(resultSting);
            
            Console.WriteLine(responseObj.Root);
            
            // Проверяем ответ апи на соответствие нашему контракту, описанному в файле getUsers.Positive.json
            CheckResponse(responseObj, "getUsers.Positive.json");
        }

    }
}