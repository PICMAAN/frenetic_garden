// Hereda toda la logica de ComensalBase (pedido, globo, paciencia,
// entrega, sprites) y solo define su propio TipoComensal.
public class ComensalCerdo : ComensalBase
{
    protected override void ConfigurarTipo()
    {
        tipo = Tipocomensal.Cerdo;
    }
}