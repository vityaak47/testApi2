using Newtonsoft.Json;

namespace AutomationTestsExample.Tests.ApiTests._1Test1Assert
{
    // Класс для дальнейшего преобразования ответа от апи в экземпляр этогокласса 
    public class ResponseGet
    {
        [JsonProperty("data")]  // как поле называется в ответе апи
        public Data Data { get; set; }  // переменная класса, в которую будет записано значение из аналогичного поля из ответа апи
        
        [JsonProperty("support")] 
        public Support Support { get; set; }
    }
    
    public class Support
    {
        [JsonProperty("url")] 
        public string Url { get; set; }

        [JsonProperty("text")] 
        public string Text { get; set; }
    }

    public class Data
    {
        [JsonProperty("id")] 
        public int Id { get; set; }
        
        [JsonProperty("email")] 
        public string Email { get; set; }
        
        [JsonProperty("first_name")] 
        public string FirstName { get; set; }
        
        [JsonProperty("last_name")] 
        public string LastName { get; set; }
        
        [JsonProperty("avatar")] 
        public string Avatar { get; set; }
    }
}