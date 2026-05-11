namespace AdminCore.Services
{
    public interface IRucValidator
    {
        bool EsValido(string? ruc, out string mensajeError);
    }
}
    