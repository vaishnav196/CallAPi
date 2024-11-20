using RestSharp;
using System.Configuration;
using System.Data;

namespace CallAPi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mobileNumber = "918108136181";  // Replace with actual mobile number
            string filepath = "http://182.156.225.100:8082/pdf//2024/11/20/H8691_24098040_24196847_761920_2_21.PDF";  // Replace with actual file path
            string userId = "SSSL";  // Replace with actual user ID

            // Call the async API method
            BmjhApiCallAsync(mobileNumber, filepath, userId);
        }
        private static async Task BmjhApiCallAsync(string ptnMobNo, string filepath, string userid)
        {
            //string logpath = ConfigurationManager.AppSettings["loggerpath"].ToString();
            //string logger = ConfigurationManager.AppSettings["logger"].ToString();

            //void log(string text)
            //{
            //    if (logger == "Y")
            //    {
            //        System.IO.File.AppendAllText(logpath, text + Environment.NewLine + Environment.NewLine);
            //    }
            //}
            //log("inside the BmjhApiCallAsync");

            try
            {
                //log("inside the BmjhApiCallAsync try block");
                //log("Filepath:" + filepath);

                //var objComm1 = new iBCommonModulesClient();
                //var ds = new DataSet();
                //string path = "";
                //ds = objComm1.RuleData1("1559");

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    path = ds.Tables[0].Rows[0]["data1"].ToString();
                //}
                //filepath = path + filepath.Replace("\\", "/");
                //log("Filepath1:" + filepath);

                // Fixed parameters from the CURL request
                string snapPeApiUrl = "https://integration-api.snap.pe/rest/v1/merchants/BhagwanMahaveerJainHospital/applications/BhagwanMahaveerJainHospital/send-message";
                string authorizationToken = "MzI3MDkzMjpjYmQ0Nzk0Mi1mYmFiLTQ4NTAtOTMwNC0yNTljNTQ2MGY1NjU=";

                //log("snapPeApiUrl: " + snapPeApiUrl);
                //log("authorizationToken: " + authorizationToken);

                var jsonContent = new
                {
                    mobile_number = ptnMobNo,
                    message_type = "document",
                    message_text = "Please find attached your Lab Report for the Sample Collected. Team BMJH Lab",
                    url = filepath
                };

               // log("JSON Content: " + Newtonsoft.Json.JsonConvert.SerializeObject(jsonContent));

             

                var client = new RestClient(snapPeApiUrl);
                var request = new RestRequest("", Method.Post);
                request.AddHeader("Authorization", $"Basic {authorizationToken}");
                request.AddJsonBody(jsonContent);

               // log("Sending API request with RestSharp...");

                // Execute the API call
                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                   // log("Message sent successfully");
                }
                else
                {
                    //log($"Failed to send message: {response.StatusCode} - {response.ErrorMessage}\nResponse content: {response.Content}");
                }

                //// Creating an object to update in your table
                //var ClsWhatsAppObj = new WhatsAppDtlWc
                //{
                //    WhatsAppType = 5, // Assuming 5 for SnapPe type
                //    StatusCode = (int)response.StatusCode,
                //    Status = response.StatusDescription,
                //    ApiResponse = response.Content,
                //    MobileNo = Convert.ToInt64(ptnMobNo),
                //    SentMessage = "Your Lab Report is Ready.",
                //    SentUser = userid,
                //    JsonData = Newtonsoft.Json.JsonConvert.SerializeObject(jsonContent),
                //    DataId = "",
                //    FileName = Path.GetFileName(filepath)
                //};

                //// Logging the details of the sent request
                //log("SendSnapPeApiCallAsync WhatsAppType = " + ClsWhatsAppObj.WhatsAppType);
                //log("SendSnapPeApiCallAsync StatusCode = " + ClsWhatsAppObj.StatusCode);
                //log("SendSnapPeApiCallAsync Status = " + ClsWhatsAppObj.Status);
                //log("SendSnapPeApiCallAsync ApiResponse = " + ClsWhatsAppObj.ApiResponse);
                //log("SendSnapPeApiCallAsync MobileNo = " + ClsWhatsAppObj.MobileNo);
                //log("SendSnapPeApiCallAsync FileName = " + ClsWhatsAppObj.FileName);
                //log("SendSnapPeApiCallAsync SentUser = " + ClsWhatsAppObj.SentUser);
                //log("SendSnapPeApiCallAsync JsonData = " + ClsWhatsAppObj.JsonData);

                //// Updating details in your database
                //var objDtl = new iBDispatchLabRptClient();
                //log("Before updating WhatsApp details in database");
                //var UpdateResult = objDtl.UpdWhatsAppDtl(ClsWhatsAppObj);
                //log("After updating WhatsApp details in database");
            }
            catch (Exception ex)
            {
               // log("In BMJHASYNC() exception: " + ex.Message);
            }
        }

    }
}
