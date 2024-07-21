using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

public class ReportsController : Controller
{
    private readonly string _connectionString;

    public ReportsController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IActionResult Index()
    {
        return View();  // Assicuriamoci che questa vista sia nella cartella Views/Reports/Index.cshtml
    }

    public IActionResult TotalVerbaliByTrasgressore()
    {
        var result = new List<TotalVerbaliViewModel>();

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"
                SELECT T.Nome, T.Cognome, COUNT(Ve.Id) AS TotaleVerbali
                FROM Trasgressori T
                JOIN Violazioni Vi ON T.Id = Vi.TrasgressoreId
                JOIN Verbali Ve ON Vi.Id = Ve.ViolazioneId
                GROUP BY T.Nome, T.Cognome", con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new TotalVerbaliViewModel
                {
                    Nome = reader["Nome"].ToString(),
                    Cognome = reader["Cognome"].ToString(),
                    TotaleVerbali = reader["TotaleVerbali"] != DBNull.Value ? (int)reader["TotaleVerbali"] : 0
                });
            }
        }

        return View(result);
    }

    public IActionResult TotalPointsByTrasgressore()
    {
        var result = new List<TotalPointsViewModel>();

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"
                SELECT T.Nome, T.Cognome, SUM(Vi.PuntiDecurtati) AS TotalePunti
                FROM Trasgressori T
                JOIN Violazioni Vi ON T.Id = Vi.TrasgressoreId
                GROUP BY T.Nome, T.Cognome", con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new TotalPointsViewModel
                {
                    Nome = reader["Nome"].ToString(),
                    Cognome = reader["Cognome"].ToString(),
                    TotalePunti = reader["TotalePunti"] != DBNull.Value ? (int)reader["TotalePunti"] : 0
                });
            }
        }

        return View(result);
    }

    public IActionResult ViolazioniOverTenPoints()
    {
        var result = new List<ViolazioniOverTenPointsViewModel>();

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"
                SELECT T.Nome, T.Cognome, Vi.Importo, Vi.DataViolazione, Vi.PuntiDecurtati
                FROM Trasgressori T
                JOIN Violazioni Vi ON T.Id = Vi.TrasgressoreId
                WHERE Vi.PuntiDecurtati > 10", con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new ViolazioniOverTenPointsViewModel
                {
                    Nome = reader["Nome"].ToString(),
                    Cognome = reader["Cognome"].ToString(),
                    Importo = reader["Importo"] != DBNull.Value ? (decimal)reader["Importo"] : 0,
                    DataViolazione = (DateTime)reader["DataViolazione"],
                    PuntiDecurtati = reader["PuntiDecurtati"] != DBNull.Value ? (int)reader["PuntiDecurtati"] : 0
                });
            }
        }

        return View(result);
    }

    public IActionResult ViolazioniOverFourHundredEuro()
    {
        var result = new List<ViolazioniOverFourHundredEuroViewModel>();

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"
                SELECT T.Nome, T.Cognome, Vi.Importo, Vi.DataViolazione, Vi.PuntiDecurtati
                FROM Trasgressori T
                JOIN Violazioni Vi ON T.Id = Vi.TrasgressoreId
                WHERE Vi.Importo > 400", con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new ViolazioniOverFourHundredEuroViewModel
                {
                    Nome = reader["Nome"].ToString(),
                    Cognome = reader["Cognome"].ToString(),
                    Importo = reader["Importo"] != DBNull.Value ? (decimal)reader["Importo"] : 0,
                    DataViolazione = (DateTime)reader["DataViolazione"],
                    PuntiDecurtati = reader["PuntiDecurtati"] != DBNull.Value ? (int)reader["PuntiDecurtati"] : 0
                });
            }
        }

        return View(result);
    }
}

public class TotalVerbaliViewModel
{
    public string Nome { get; set; }
    public string Cognome { get; set; }
    public int TotaleVerbali { get; set; }
}

public class TotalPointsViewModel
{
    public string Nome { get; set; }
    public string Cognome { get; set; }
    public int TotalePunti { get; set; }
}

public class ViolazioniOverTenPointsViewModel
{
    public string Nome { get; set; }
    public string Cognome { get; set; }
    public decimal Importo { get; set; }
    public DateTime DataViolazione { get; set; }
    public int PuntiDecurtati { get; set; }
}

public class ViolazioniOverFourHundredEuroViewModel
{
    public string Nome { get; set; }
    public string Cognome { get; set; }
    public decimal Importo { get; set; }
    public DateTime DataViolazione { get; set; }
    public int PuntiDecurtati { get; set; }
}
