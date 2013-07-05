using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;
using App_Code.Utility;


public partial class HTTPtest : System.Web.UI.Page
{
    public static int[] timeSt;
    public static double[] val;
    public static string[] timeSeries;
    static string uuid;
    protected void Page_Load(object sender, EventArgs e)
    {
        string sURL;
        sURL = "http://192.168.1.40:9101/api/query";

        string stringData = "select data in (now -6h, now) limit 5 where Path = '/RasPi1/Meter33/Energy'";
        HttpWebRequest req = WebRequest.Create(sURL) as HttpWebRequest;
        IWebProxy iwprxy = WebRequest.GetSystemWebProxy();
        req.Proxy = iwprxy;

        req.Method = "POST";
        req.ContentType = "";
       
        ASCIIEncoding encoding = new ASCIIEncoding();
        byte[] data = encoding.GetBytes(stringData);
        

        req.ContentLength = data.Length;

        Stream os = req.GetRequestStream();
        os.Write(data, 0, data.Length);
        os.Close();      

        HttpWebResponse response = req.GetResponse() as HttpWebResponse;

        Stream objStream;
        objStream = req.GetResponse().GetResponseStream();

        StreamReader objReader = new StreamReader(objStream);

        var jss = new JavaScriptSerializer();

        string sline = objReader.ReadLine();
         
        var f1 = jss.Deserialize<dynamic>(sline);

        var f2 = f1[0];
        uuid = f2.ToString();
        var f3 = f2["Readings"];

            timeSt = new int[f3.Length];
            val = new double[f3.Length];
            timeSeries = new string[f3.Length];

            for (int i = 0; i < f3.Length; i++)
            {
                var f4 = f3[i];
                timeSt[i] = Convert.ToInt32( f4[0]/1000);
                val[i] = Convert.ToDouble( f4[1]);
            }

            timeSeries = Utilitie_S.TimeFormatterBar(timeSt);

        response.Close();
          
    }
}   