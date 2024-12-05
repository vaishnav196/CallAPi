using RestSharp;
using System.Configuration;
using System.Data;
using System.Text;

namespace CallAPi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string mobileNumber = "918108136181";  // Replace with actual mobile number
            string filepath = "http://182.156.225.100:8082/pdf//2024/11/20/H8691_24098040_24196847_761920_2_21.PDF";  // Replace with actual file path
            string userId = "SSSL";  // Replace with actual user ID

            // Call the async API method
            //  await  BmjhApiCallAsync(mobileNumber, filepath, userId);
            var result = await SendWhatsAppMessageAsync(mobileNumber, filepath);
        }
        public static async Task BmjhApiCallAsync(string mobileNumber, string pdfUrl, string userId)
        {
            try
            {
                var ApiUrl = "https://integration-api.snap.pe/rest/v1/merchants/BhagwanMahaveerJainHospital/applications/BhagwanMahaveerJainHospital/send-message";
        var AuthorizationToken = " write yoru fiex tken here";


        // Create the JSON payload dynamically
        string jsonPayload = @$"
            {{
                ""template"": ""lab_report"",
                ""template_type"": ""document"",
                ""mobile_number"": ""{mobileNumber}"",
                ""url"": ""{pdfUrl}""
               
            }}";

                // Set up HttpClient and request
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, ApiUrl)
                    {
                        Headers = { { "Authorization", AuthorizationToken } },
                        Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
                    };

                    // Send the request
                    var response = await client.SendAsync(request);

                    // Handle the response
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Message sent successfully:");
                        Console.WriteLine(responseContent);
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Failed to send message: {response.StatusCode} - {response.ReasonPhrase}");
                        Console.WriteLine("Error Details: " + errorContent);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
        }
        public static async Task<bool> SendWhatsAppMessageAsync(string mobileNumber, string pdfUrl)
        {
            var options = new RestClientOptions("https://integration-api.snap.pe")
            {
                MaxTimeout = -1 // Infinite timeout, adjust as needed
            };
            var client = new RestClient(options);

            // Prepare the request
            var request = new RestRequest("/rest/v1/merchants/BhagwanMahaveerJainHospital/applications/BhagwanMahaveerJainHospital/send-message", Method.Post);

            // Add headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Basic MzI3MDkzMjpjYmQ0Nzk0Mi1mYmFiLTQ4NTAtOTMwNC0yNTljNTQ2MGY1NjU=");

            // Prepare JSON body
            var body = new
            {
                template = "lab_report",
                template_type = "document",
                mobile_number = mobileNumber,
                url = pdfUrl
            };

            // Attach the body to the request
            request.AddJsonBody(body);

            try
            {
                // Execute the request
                RestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    Console.WriteLine("API Response: " + response.Content); // Log success response
                    return true; // Indicate success
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    Console.WriteLine("API Error Response: " + response.Content);
                    return false; // Indicate failure
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false; // Indicate failure
            }
        }
    }
}
