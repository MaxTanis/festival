﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SchoolTemplate.Database;
using SchoolTemplate.Models;

namespace SchoolTemplate.Controllers
{
    public class HomeController : Controller
    {
        // zorg ervoor dat je hier je gebruikersnaam (leerlingnummer) en wachtwoord invult
        string connectionString = "Server=172.16.160.21;Port=3306;Database=110157;Uid=110157;Pwd=crOLeran;";

        public IActionResult Index()
        { 
            return View();
        }

        private List<Festival> GetFestivals()
        {
            List<Festival> festivals = new List<Festival>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from festival", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["id"]);
                        string naam = reader["naam"].ToString();
                        string plaats = reader["plaats"].ToString();

                        DateTime start_dt;
                        DateTime.TryParse(reader["start_dt"] as string, out start_dt);

                        Festival f = new Festival
                        {
                            id = id,
                            naam = naam,
                            plaats = plaats,
                            start_dt = start_dt
                        };
                        festivals.Add(f);
                    }
                }
            }

            return festivals;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Route("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [Route("contact")]
        [HttpPost]
        public IActionResult Contact(PersonModel model)
        {
            return View(model);
        }

        [Route("informatie")]
        public IActionResult Informatie()
        {
            List<Festival> products = new List<Festival>();
            products = GetFestivals();

            return View(products);
        }

    }
}
