// Los 6 comensales finales del juego. Cada uno hereda toda la logica
// de ComensalBase (pedido, globo, paciencia, entrega) y solo define
// su propio TipoComensal.

public class ComensalPollo : ComensalBase
{
    protected override void ConfigurarTipo()
    {
        tipo = TipoComensal.Pollo;
    }
}

public class ComensalVaca : ComensalBase
{
    protected override void ConfigurarTipo()
    {
        tipo = TipoComensal.Vaca;
    }
}

public class ComensalCerdo : ComensalBase
{
    protected override void ConfigurarTipo()
    {
        tipo = TipoComensal.Cerdo;
    }
}

public class ComensalTortuga : ComensalBase
{
    protected override void ConfigurarTipo()
    {
        tipo = TipoComensal.Tortuga;
    }
}

public class ComensalRana : ComensalBase
{
    protected override void ConfigurarTipo()
    {
        tipo = TipoComensal.Rana;
    }
}

public class ComensalOveja : ComensalBase
{
    protected override void ConfigurarTipo()
    {
        tipo = TipoComensal.Oveja;
    }
}