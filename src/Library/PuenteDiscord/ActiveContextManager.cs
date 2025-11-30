using Library.PuenteDiscord;

/// <summary>
/// Puede gestionar los contextos activos de comandos por usuario,
/// permitiendo verificar, registrar y finalizar la ejecución
/// de un comando asociado a un usuario específico.
/// </summary>
public static class ActiveContextManager
{
    /// <summary>
    /// Es un diccionario interno que almacena los contextos activos
    /// de cada usuario, identificados por su ID único (como el ulong).
    /// </summary>
    private static readonly Dictionary<ulong, DiscordMessageContext> _contextos =
        new Dictionary<ulong, DiscordMessageContext>();

    /// <summary>
    /// Intenta obtener el contexto activo asociado a un usuario.
    /// </summary>
    /// <param name="userId">ID del usuario de Discord.</param>
    /// <param name="ctx">Devuelve el contexto encontrado, si existe.</param>
    /// <returns>
    /// true si el usuario tiene un contexto activo; 
    /// false en caso contrario.
    /// </returns>
    public static bool TryGetContext(ulong userId, out DiscordMessageContext ctx)
    {
        return _contextos.TryGetValue(userId, out ctx);
    }

    /// <summary>
    /// Registra o reemplaza el contexto activo de un usuario.
    /// </summary>
    /// <param name="userId">ID del usuario de Discord.</param>
    /// <param name="ctx">Contexto a asociar al usuario.</param>
    public static void Registrar(ulong userId, DiscordMessageContext ctx)
    {
        _contextos[userId] = ctx;
    }

    /// <summary>
    /// Elimina el contexto activo de un usuario, si existe.
    /// </summary>
    /// <param name="userId">ID del usuario de Discord.</param>
    public static void Finalizar(ulong userId)
    {
        if (_contextos.ContainsKey(userId))
            _contextos.Remove(userId);
    }
}
