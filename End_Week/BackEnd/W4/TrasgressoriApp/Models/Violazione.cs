public class Violazione
{
    public int Id { get; set; }
    public int TrasgressoreId { get; set; }
    public string Descrizione { get; set; }
    public DateTime DataViolazione { get; set; }
    public decimal Importo { get; set; }  // Assicurati che questa proprietà esista
    public int PuntiDecurtati { get; set; }
}
