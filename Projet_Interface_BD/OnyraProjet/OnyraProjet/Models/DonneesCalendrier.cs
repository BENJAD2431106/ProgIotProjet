﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnyraProjet.Models;

[Keyless]
public partial class DonneesCalendrier
{
    [Column("noUtilisateur")]
    public int NoUtilisateur { get; set; }

    public double ValTemperature { get; set; }

    [Column("valSon")]
    public double ValSon { get; set; }

    [Column("dateHeure", TypeName = "datetime")]
    public DateTime DateHeure { get; set; }

    [Column("ressenti")]
    public int Ressenti { get; set; }

    [Column("heureCoucher", TypeName = "datetime")]
    public DateTime HeureCoucher { get; set; }

    [Column("heureLever", TypeName = "datetime")]
    public DateTime HeureLever { get; set; }

    [Column("dates")]
    public DateOnly Dates { get; set; }
}
