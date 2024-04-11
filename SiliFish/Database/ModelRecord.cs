﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiliFish.Database
{
    [PrimaryKey(nameof(Id))]
    public class ModelRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string Stats { get; set; }
        public string JsonFile { get; set; }

        public ModelRecord() { }//required for SQLite
        public ModelRecord(string name, DateTime dateTime, string stats, string jsonFile)
        {
            Name = name;
            DateTime = dateTime;
            Stats = stats;
            JsonFile = jsonFile;
        }
    }
}
