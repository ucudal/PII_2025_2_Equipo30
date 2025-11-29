using Library.PuenteDiscord;

public static class ActiveContextManager
{
    // Contextos activos por usuario
    private static readonly Dictionary<ulong, DiscordMessageContext> _contextos =
        new Dictionary<ulong, DiscordMessageContext>();

    // Verifica si un usuario ya está ejecutando un comando
    public static bool TryGetContext(ulong userId, out DiscordMessageContext ctx)
    {
        return _contextos.TryGetValue(userId, out ctx);
    }

    // Registrar contexto
    public static void Registrar(ulong userId, DiscordMessageContext ctx)
    {
        _contextos[userId] = ctx;
    }

    // Finalizar contexto
    public static void Finalizar(ulong userId)
    {
        if (_contextos.ContainsKey(userId))
            _contextos.Remove(userId);
    }
}