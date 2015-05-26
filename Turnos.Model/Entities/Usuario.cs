using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using Turnos.Model.DTO;
using AutoMapper;

namespace Turnos.Model.Entities
{
    public class Usuario
    {
        public Usuario()
        {
            Secuencias = new HashSet<UsuarioSecuencia>();
        }

        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }

        public int UsuarioId { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<UsuarioSecuencia> Secuencias { get; set; }
    }
}