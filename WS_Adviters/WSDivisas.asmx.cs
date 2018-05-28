using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

[WebService(Namespace = "InfoDivisas")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]

public class WSDivisas : System.Web.Services.WebService
{
    public WSDivisas()
    {
        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

   [WebMethod]
    public string RecuperarCotizacion(string moneda)
    {
        try
        {
            var url = String.Format("https://www.cronista.com/MercadosOnline/moneda.html?id={0}", moneda);
            System.Net.WebClient webClient = new System.Net.WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;
            
            var result = webClient.DownloadString(url);
            string valCompra = getBetween(result, "compra2 = fixDecimal(", ");");
            string valVenta = getBetween(result, "venta2 = fixDecimal(", ");");
            return valCompra + '|' + valVenta;

        }
        catch (Exception)
        {
            return moneda;

        }

    }
    public static string getBetween(string strSource, string strStart, string strEnd)
    {
        int Start, End;
        if (strSource.Contains(strStart) && strSource.Contains(strEnd))
        {
            Start = strSource.IndexOf(strStart, 0) + strStart.Length;
            End = strSource.IndexOf(strEnd, Start);
            return strSource.Substring(Start, End - Start);
        }
        else
        {
            return "";
        }
    }

}