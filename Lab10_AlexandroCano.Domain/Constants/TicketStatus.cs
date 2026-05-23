namespace Lab10_AlexandroCano.Domain.Constants;

public static class TicketStatus
{
    public const string Abierto = "abierto";
    public const string EnProceso = "en_proceso";
    public const string Cerrado = "cerrado";

    public static bool IsValid(string status)
    {
        return status == Abierto ||
               status == EnProceso ||
               status == Cerrado;
    }
}