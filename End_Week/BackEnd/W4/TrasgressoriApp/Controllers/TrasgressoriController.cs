using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

public class TrasgressoriController : Controller
{
    private readonly string _connectionString;

    public TrasgressoriController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IActionResult Index()
    {
        var trasgressori = new List<Trasgressore>();

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Trasgressori", con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                trasgressori.Add(new Trasgressore
                {
                    Id = (int)reader["Id"],
                    Nome = reader["Nome"].ToString(),
                    Cognome = reader["Cognome"].ToString(),
                    DataNascita = (DateTime)reader["DataNascita"]
                });
            }
        }

        return View(trasgressori);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Trasgressore trasgressore)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Trasgressori (Nome, Cognome, DataNascita) VALUES (@Nome, @Cognome, @DataNascita)", con);
            cmd.Parameters.AddWithValue("@Nome", trasgressore.Nome);
            cmd.Parameters.AddWithValue("@Cognome", trasgressore.Cognome);
            cmd.Parameters.AddWithValue("@DataNascita", trasgressore.DataNascita);
            cmd.ExecuteNonQuery();
        }

        return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        Trasgressore trasgressore = null;

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Trasgressori WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                trasgressore = new Trasgressore
                {
                    Id = (int)reader["Id"],
                    Nome = reader["Nome"].ToString(),
                    Cognome = reader["Cognome"].ToString(),
                    DataNascita = (DateTime)reader["DataNascita"]
                };
            }
        }

        if (trasgressore == null)
        {
            return NotFound();
        }

        return View(trasgressore);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Trasgressore trasgressore = null;

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Trasgressori WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                trasgressore = new Trasgressore
                {
                    Id = (int)reader["Id"],
                    Nome = reader["Nome"].ToString(),
                    Cognome = reader["Cognome"].ToString(),
                    DataNascita = (DateTime)reader["DataNascita"]
                };
            }
        }

        if (trasgressore == null)
        {
            return NotFound();
        }

        return View(trasgressore);
    }

    [HttpPost]
    public IActionResult Edit(Trasgressore trasgressore)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Trasgressori SET Nome = @Nome, Cognome = @Cognome, DataNascita = @DataNascita WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", trasgressore.Id);
            cmd.Parameters.AddWithValue("@Nome", trasgressore.Nome);
            cmd.Parameters.AddWithValue("@Cognome", trasgressore.Cognome);
            cmd.Parameters.AddWithValue("@DataNascita", trasgressore.DataNascita);
            cmd.ExecuteNonQuery();
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        Trasgressore trasgressore = null;

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Trasgressori WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                trasgressore = new Trasgressore
                {
                    Id = (int)reader["Id"],
                    Nome = reader["Nome"].ToString(),
                    Cognome = reader["Cognome"].ToString(),
                    DataNascita = (DateTime)reader["DataNascita"]
                };
            }
        }

        if (trasgressore == null)
        {
            return NotFound();
        }

        return View(trasgressore);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"
                DELETE FROM Verbali WHERE ViolazioneId IN (SELECT Id FROM Violazioni WHERE TrasgressoreId = @Id);
                DELETE FROM Violazioni WHERE TrasgressoreId = @Id;
                DELETE FROM Trasgressori WHERE Id = @Id;", con);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        return RedirectToAction("Index");
    }
}
