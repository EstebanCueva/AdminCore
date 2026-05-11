namespace AdminCore.Services
{
    public class RucValidator : IRucValidator
    {
        public bool EsValido(string? ruc, out string mensajeError)
        {
            mensajeError = string.Empty;

            if (string.IsNullOrWhiteSpace(ruc))
            {
                mensajeError = "El RUC es obligatorio.";
                return false;
            }

            ruc = ruc.Trim();

            if (ruc.Length != 13)
            {
                mensajeError = "El RUC debe tener 13 dígitos.";
                return false;
            }

            if (!ruc.All(char.IsDigit))
            {
                mensajeError = "El RUC solo debe contener números.";
                return false;
            }

            if (ruc.Substring(10, 3) == "000")
            {
                mensajeError = "Los últimos 3 dígitos del RUC no pueden ser 000.";
                return false;
            }

            return true;
        }
    }
}
