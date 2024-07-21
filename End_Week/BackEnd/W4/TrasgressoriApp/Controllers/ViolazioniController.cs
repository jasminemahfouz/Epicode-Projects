using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

public class ViolazioniController : Controller
{
    private readonly string _connectionString;

    public ViolazioniController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IActionResult Index(int trasgressoreId)
    {
        var violazioni = new List<Violazione>();

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Violazioni WHERE TrasgressoreId = @TrasgressoreId", con);
            cmd.Parameters.AddWithValue("@TrasgressoreId", trasgressoreId);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                violazioni.Add(new Violazione
                {
                    Id = (int)reader["Id"],
                    TrasgressoreId = (int)reader["TrasgressoreId"],
                    Descrizione = reader["Descrizione"].ToString(),
                    DataViolazione = (DateTime)reader["DataViolazione"],
                    Importo = reader["Importo"] != DBNull.Value ? (decimal)reader["Importo"] : 0,
                    PuntiDecurtati = reader["PuntiDecurtati"] != DBNull.Value ? (int)reader["PuntiDecurtati"] : 0
                });
            }
        }

        ViewBag.TrasgressoreId = trasgressoreId;
        return View(violazioni);
    }

    [HttpGet]
    public IActionResult Create(int trasgressoreId)
    {
        ViewBag.TrasgressoreId = trasgressoreId;
        return View();
    }

    [HttpPost]
    public IActionResult Create(Violazione violazione)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Violazioni (TrasgressoreId, Descrizione, DataViolazione, Importo, PuntiDecurtati) VALUES (@TrasgressoreId, @Descrizione, @DataViolazione, @Importo, @PuntiDecurtati)", con);
            cmd.Parameters.AddWithValue("@TrasgressoreId", violazione.TrasgressoreId);
            cmd.Parameters.AddWithValue("@Descrizione", violazione.Descrizione);
            cmd.Parameters.AddWithValue("@DataViolazione", violazione.DataViolazione);
            cmd.Parameters.AddWithValue("@Importo", violazione.Importo);
            cmd.Parameters.AddWithValue("@PuntiDecurtati", violazione.PuntiDecurtati);
            cmd.ExecuteNonQuery();
        }

        return RedirectToAction("Index", new { trasgressoreId = violazione.TrasgressoreId });
    }

    public IActionResult Details(int id)
    {
        Violazione violazione = null;

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Violazioni WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                violazione = new Violazione
                {
                    Id = (int)reader["Id"],
                    TrasgressoreId = (int)reader["TrasgressoreId"],
                    Descrizione = reader["Descrizione"].ToString(),
                    DataViolazione = (DateTime)reader["DataViolazione"],
                    Importo = reader["Importo"] != DBNull.Value ? (decimal)reader["Importo"] : 0,
                    PuntiDecurtati = reader["PuntiDecurtati"] != DBNull.Value ? (int)reader["PuntiDecurtati"] : 0
                };
            }
        }

        if (violazione == null)
        {
            return NotFound();
        }

        return View(violazione);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Violazione violazione = null;

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Violazioni WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                violazione = new Violazione
                {
                    Id = (int)reader["Id"],
                    TrasgressoreId = (int)reader["TrasgressoreId"],
                    Descrizione = reader["Descrizione"].ToString(),
                    DataViolazione = (DateTime)reader["DataViolazione"],
                    Importo = reader["Importo"] != DBNull.Value ? (decimal)reader["Importo"] : 0,
                    PuntiDecurtati = reader["PuntiDecurtati"] != DBNull.Value ? (int)reader["PuntiDecurtati"] : 0
                };
            }
        }

        if (violazione == null)
        {
            return NotFound();
        }

        ViewBag.TrasgressoreId = violazione.TrasgressoreId;
        return View(violazione);
    }

    [HttpPost]
    public IActionResult Edit(Violazione violazione)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();

            // Elimina i verbali associati alla violazione
            SqlCommand deleteCmd = new SqlCommand("DELETE FROM Verbali WHERE ViolazioneId = @ViolazioneId", con);
            deleteCmd.Parameters.AddWithValue("@ViolazioneId", violazione.Id);
            deleteCmd.ExecuteNonQuery();

            // Aggiorna la violazione
            SqlCommand updateCmd = new SqlCommand("UPDATE Violazioni SET Descrizione = @Descrizione, DataViolazione = @DataViolazione, Importo = @Importo, PuntiDecurtati = @PuntiDecurtati WHERE Id = @Id", con);
            updateCmd.Parameters.AddWithValue("@Id", violazione.Id);
            updateCmd.Parameters.AddWithValue("@Descrizione", violazione.Descrizione);
            updateCmd.Parameters.AddWithValue("@DataViolazione", violazione.DataViolazione);
            updateCmd.Parameters.AddWithValue("@Importo", violazione.Importo);
            updateCmd.Parameters.AddWithValue("@PuntiDecurtati", violazione.PuntiDecurtati);
            updateCmd.ExecuteNonQuery();
        }

        return RedirectToAction("Index", new { trasgressoreId = violazione.TrasgressoreId });
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        Violazione violazione = null;

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Violazioni WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                violazione = new Violazione
                {
                    Id = (int)reader["Id"],
                    TrasgressoreId = (int)reader["TrasgressoreId"],
                    Descrizione = reader["Descrizione"].ToString(),
                    DataViolazione = (DateTime)reader["DataViolazione"],
                    Importo = reader["Importo"] != DBNull.Value ? (decimal)reader["Importo"] : 0,
                    PuntiDecurtati = reader["PuntiDecurtati"] != DBNull.Value ? (int)reader["PuntiDecurtati"] : 0
                };
            }
        }

        if (violazione == null)
        {
            return NotFound();
        }

        return View(violazione);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        int trasgressoreId;

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT TrasgressoreId FROM Violazioni WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            trasgressoreId = (int)cmd.ExecuteScalar();
        }

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"
                DELETE FROM Verbali WHERE ViolazioneId = @Id;
                DELETE FROM Violazioni WHERE Id = @Id;", con);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        return RedirectToAction("Index", new { trasgressoreId });
    }
}
