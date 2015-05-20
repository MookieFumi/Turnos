##Diferentes maneras de trabajar con el contexto de Entity Framework.

Con dos m�todos cuyo resultado final es el mismo (turnos de usuario) devuelven datos de diferente forma, uno utilizando entidades del dominio(GetUsuario) y aprovech�ndonos de Lazy Loading (ejecuta n consultas contra la bbdd) y el otro(GetUsuarioDTO) mediante DTO con la ayuda de AutoMapper (ejecuta una �nica consulta contra la bbdd).

	private Usuario GetUsuario(DPContext context)
	{
		return context.Usuarios.First();
	}

	private TurnoDeUsuarioDTO GetUsuarioDTO(DPContext context)
	{
		return context.Usuarios
			.Project()
			.To<TurnoDeUsuarioDTO>()
			.First();
	}


