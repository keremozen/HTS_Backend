// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System.Text;
using System.Xml;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration.Json;


Console.WriteLine("Hello, World!");

try
{
    string dovizUrl = "http://www.tcmb.gov.tr/kurlar/today.xml";

    XmlDocument dokuman = new XmlDocument();
    dokuman.Load(dovizUrl);


    //ParaBirimiBusiness pbBusiness = new ParaBirimiBusiness();
    //ParaBirimiFilter pbFilter = new ParaBirimiFilter();
    //pbFilter.Active = true;
    //IList<ParaBirimiSimple> paraBirimiKodListesi = pbBusiness.GetAll(pbFilter, "Id", true);

    #region döviz kuru günlük verilerin parabirimlerine göre kaydedilmesi
    //her işlemde önce sistemde aktif olan döviz bilgileri pasife çekilmelidir
    //sonrasında sistemde kayıtlı tüm para birimlerine göre kur tablosuna kayıt atılmalıdır

    //pasife çekilme işlemi yapılıyor.....
    //DovizKuru.UpdateBy(DovizKuruColumns.Active, false, new DovizKuruFilter() { Active = true });
    //pasife çekilme işlemi yapılıyor.....

    //yeni değerler ekleniyor.....
    decimal USDValue = 0;
    decimal EURValue = 0;
    decimal donusum;
    string caprazKur, digerCaprazKur;

    IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();


    //para birimi olarak TL gelmesi durumunda TCMB verilerinde USD alış bilgisi kullanılmalıdır 
    USDValue = decimal.Parse(dokuman.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/ForexBuying").InnerXml.Replace('.', ','), NumberStyles.Currency);

        //para birimi olarak TL gelmesi durumunda TCMB verilerinde USD alış bilgisi kullanılmalıdır 
        EURValue = decimal.Parse(dokuman.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/ForexBuying").InnerXml.Replace('.', ','), NumberStyles.Currency);

        string connString = configuration.GetConnectionString("HTSDatabase");

        using (var conn = new NpgsqlConnection(connString))
        {
            conn.Open();

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO public.\"ExchangeRateInformations\"( \"CurrencyId\", \"ExchangeRate\", \"CreationTime\")\r\n\tVALUES (@value1, @value2, @value3);";
                cmd.Parameters.AddWithValue("value1", 2); // Değerleri değiştirin
                cmd.Parameters.AddWithValue("value2", USDValue); // Değerleri değiştirin
                cmd.Parameters.AddWithValue("value3", DateTime.Now); // Değerleri değiştirin
            
            cmd.ExecuteNonQuery();

      
            }


        using (var cmd2 = new NpgsqlCommand())
        {
            cmd2.Connection = conn;
            cmd2.CommandText = "INSERT INTO public.\"ExchangeRateInformations\"( \"CurrencyId\", \"ExchangeRate\", \"CreationTime\")\r\n\tVALUES (@value1, @value2, @value3);";
            cmd2.Parameters.AddWithValue("value1", 3); // Değerleri değiştirin
            cmd2.Parameters.AddWithValue("value2", EURValue); // Değerleri değiştirin
            cmd2.Parameters.AddWithValue("value3", DateTime.Now); // Değerleri değiştirin

            cmd2.ExecuteNonQuery();
        }

        conn.Close();
    }

    
    //yeni değerler ekleniyor.....
    #endregion
}
catch (Exception ex)
{
}
finally
{
}
