using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AutomationTestsExample.Tests.ApiTests.ErrorListExample
{
    public class APiTestBase
    /* Базовый класс для тестирования апи. Каждому тестовому классу, который будет от него наследован, будут доступны
     все его методы и переменные */
    {
        private static List<string> ErrorList;
        
        [SetUp] // блок, который будет выполнен перед каждым тестом каждого класса, наследуемого от APiTestBase
        public async Task Setup()
        {
            ErrorList = new List<string>(); // инициализируем пустой список, в который в дальнейшем будут записываться пойманные ошибки
        }

        [TearDown] // После каждого теста 
        public void Dispose()
        {
            // выполняется проверка, что список ошибок пустой, если не пустой, то вывести содержимое этого списка
            Assert.IsEmpty(ErrorList, $"Значения не соответствуют ожидаемым : { string.Join("\n", ErrorList) }");
        }
        
        // Аналогия AssertAreEqual, проверка через IF без выхода из теста, в случае ошибки
        protected List<string> CustomAssertAreEqual(int expected, int? actual, string error)
        {
            if (expected != actual)
            {
                // если ожидаемое значение != полученному, записываем ошибку в  ErrorList
                ErrorList.Add(error + $". Ожидали { expected }. Получено: { actual }");
            }
            
            return ErrorList;
        }

        protected List<string> CustomAssertIsNotEmpty(string aString, string error)
        {
            if (aString == string.Empty || aString.Length == 0)
            {
                ErrorList.Add(error);
            }
            return ErrorList;
        }
    }
}