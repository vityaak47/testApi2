using Newtonsoft.Json;

namespace AutomationTestsExample.Tests.ApiTests._1Test1Assert
{
    public class ResponsePost
    {
        [JsonProperty("name")] 
        public string Name { get; set; } 
        
        [JsonProperty("job")] 
        public string Job { get; set; } 
        
        [JsonProperty("id")]
        public string Id { get; set; } 
        
        [JsonProperty("createdAt")] 
        public string CreatedAt { get; set; }
    }
    
    public class RequestPost
    {
        public RequestPost(string name, string job)
        {
            Name = name;
            Job = job;
        }

        [JsonProperty("name")] 
        public string Name { get; set; } 
        
        [JsonProperty("job")] 
        public string Job { get; set; }
    }
}