// Los 6 comensales finales del juego. Cada uno hereda toda la logica
// de ComensalBase (pedido, globo, paciencia, entrega) y solo define
// su propio TipoComensal.

public class ComensalPollo : Comensalbase
{
    protected override void ConfigurarTipo()
    {
        tipo = Tipocomensal.Pollo;
    }
}

public class ComensalVaca : Comensalbase
{
    protected override void ConfigurarTipo()
    {
        tipo = Tipocomensal.Vaca;
    }
}

public class ComensalCerdo : Comensalbase
{
    protected override void ConfigurarTipo()
    {
        tipo = Tipocomensal.Cerdo;
    }
}

public class ComensalTortuga : Comensalbase
{
    protected override void ConfigurarTipo()
    {
        tipo = Tipocomensal.Tortuga;
    }
}

public class ComensalRana : Comensalbase
{
    protected override void ConfigurarTipo()
    {
        tipo = Tipocomensal.Rana;
    }
}

public class ComensalOveja : Comensalbase
{
    protected override void ConfigurarTipo()
    {
        tipo = Tipocomensal.Oveja;
    }
}