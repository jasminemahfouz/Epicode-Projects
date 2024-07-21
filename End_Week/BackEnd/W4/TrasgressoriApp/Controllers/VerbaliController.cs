using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

public class VerbaliController : Controller
{
    private readonly string _connectionString;

    public VerbaliController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IActionResult Index(int violazioneId, int trasgressoreId)
    {
        var verbali = new List<Verbale>();

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Verbali WHERE ViolazioneId = @ViolazioneId", con);
            cmd.Parameters.AddWithValue("@ViolazioneId", violazioneId);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                verbali.Add(new Verbale
                {
                    Id = (int)reader["Id"],
                    ViolazioneId = (int)reader["ViolazioneId"],
                    NumeroVerbale = reader["NumeroVerbale"].ToString(),
                    DataVerbale = (DateTime)reader["DataVerbale"]
                });
            }
        }

        ViewBag.ViolazioneId = violazioneId;
        ViewBag.TrasgressoreId = trasgressoreId;
        return View(verbali);
    }

    [HttpGet]
    public IActionResult Create(int violazioneId, int trasgressoreId)
    {
        ViewBag.ViolazioneId = violazioneId;
        ViewBag.TrasgressoreId = trasgressoreId;
        return View();
    }

    [HttpPost]
    public IActionResult Create(Verbale verbale)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Verbali (ViolazioneId, NumeroVerbale, DataVerbale) VALUES (@ViolazioneId, @NumeroVerbale, @DataVerbale)", con);
            cmd.Parameters.AddWithValue("@ViolazioneId", verbale.ViolazioneId);
            cmd.Parameters.AddWithValue("@NumeroVerbale", verbale.NumeroVerbale);
            cmd.Parameters.AddWithValue("@DataVerbale", verbale.DataVerbale);
            cmd.ExecuteNonQuery();
        }

        return RedirectToAction("Index", new { violazioneId = verbale.ViolazioneId, trasgressoreId = ViewBag.TrasgressoreId });
    }

    public IActionResult Details(int id)
    {
        Verbale verbale = null;

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Verbali WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                verbale = new Verbale
                {
                    Id = (int)reader["Id"],
                    ViolazioneId = (int)reader["ViolazioneId"],
                    NumeroVerbale = reader["NumeroVerbale"].ToString(),
                    DataVerbale = (DateTime)reader["DataVerbale"]
                };
            }
        }

        if (verbale == null)
        {
            return NotFound();
        }

        ViewBag.TrasgressoreId = verbale.ViolazioneId;
        return View(verbale);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Verbale verbale = null;

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Verbali WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                verbale = new Verbale
                {
                    Id = (int)reader["Id"],
                    ViolazioneId = (int)reader["ViolazioneId"],
                    NumeroVerbale = reader["NumeroVerbale"].ToString(),
                    DataVerbale = (DateTime)reader["DataVerbale"]
                };
            }
        }

        if (verbale == null)
        {
            return NotFound();
        }

        ViewBag.ViolazioneId = verbale.ViolazioneId;
        ViewBag.TrasgressoreId = verbale.ViolazioneId;
        return View(verbale);
    }

    [HttpPost]
    public IActionResult Edit(Verbale verbale)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Verbali SET NumeroVerbale = @NumeroVerbale, DataVerbale = @DataVerbale WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", verbale.Id);
            cmd.Parameters.AddWithValue("@NumeroVerbale", verbale.NumeroVerbale);
            cmd.Parameters.AddWithValue("@DataVerbale", verbale.DataVerbale);
            cmd.ExecuteNonQuery();
        }

        return RedirectToAction("Index", new { violazioneId = verbale.ViolazioneId, trasgressoreId = ViewBag.TrasgressoreId });
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        Verbale verbale = null;

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Verbali WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                verbale = new Verbale
                {
                    Id = (int)reader["Id"],
                    ViolazioneId = (int)reader["ViolazioneId"],
                    NumeroVerbale = reader["NumeroVerbale"].ToString(),
                    DataVerbale = (DateTime)reader["DataVerbale"]
                };
            }
        }

        if (verbale == null)
        {
            return NotFound();
        }

        ViewBag.ViolazioneId = verbale.ViolazioneId;
        ViewBag.TrasgressoreId = verbale.ViolazioneId;
        return View(verbale);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        int violazioneId;
        int trasgressoreId;

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT ViolazioneId FROM Verbali WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            violazioneId = (int)cmd.ExecuteScalar();

            cmd = new SqlCommand("SELECT TrasgressoreId FROM Violazioni WHERE Id = @ViolazioneId", con);
            cmd.Parameters.AddWithValue("@ViolazioneId", violazioneId);
            trasgressoreId = (int)cmd.ExecuteScalar();
        }

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Verbali WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        return RedirectToAction("Index", "Violazioni", new { trasgressoreId });
    }
}
