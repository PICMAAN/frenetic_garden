// Hereda toda la logica de ComensalBase (pedido, globo, paciencia,
// entrega, sprites) y solo define su propio TipoComensal.
public class ComensalVaca : ComensalBase
{
    protected override void ConfigurarTipo()
    {
        tipo = Tipocomensal.Vaca;
    }
}