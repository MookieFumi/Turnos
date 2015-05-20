﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Turnos.Model.Entities
{
    public class Usuario
    {
        public Usuario()
        {
            Turnos = new HashSet<UsuarioTurno>();
        }

        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }

        public int UsuarioId { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<UsuarioTurno> Turnos { get; set; }

        public void AddTurno(DateTime fechaDesde, int numeroSemana, string nombre)
        {
            if (Turnos.Any(p => p.FechaDesde == fechaDesde && p.NumeroSemana == numeroSemana))
            {
                throw new ArgumentException("Ya existe el número de semana para la fecha seleccionada");
            }

            var usuarioTurno = new UsuarioTurno()
            {
                FechaDesde = fechaDesde,
                NumeroSemana = numeroSemana,
                Nombre = nombre
            };
            for (int dia = 1; dia <= 7; dia++)
            {
                usuarioTurno.Dias.Add(new UsuarioTurnoDia()
                {
                    Dia = dia,
                    Turno = Turno.Mañana,
                    Trabaja = true
                });
            }

            Turnos.Add(usuarioTurno);
        }
    }
}